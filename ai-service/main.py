from fastapi import FastAPI
from pydantic import BaseModel
from transformers import pipeline
import numpy as np
import skfuzzy as fuzz
import os
import json

app = FastAPI()

MODEL_DIR = os.environ.get("MODEL_DIR", os.path.dirname(os.path.dirname(os.path.abspath(__file__))))
SENTIMENT_PATH = os.path.join(MODEL_DIR, "sentiment_bert_model")
FILTER_PATH = os.path.join(MODEL_DIR, "filtering_bert_model")

sentiment_pipe = pipeline("text-classification", model=SENTIMENT_PATH)
filter_pipe = pipeline("text-classification", model=FILTER_PATH)

SENTIMENT_MAP = {"LABEL_0": "negative", "LABEL_1": "neutral", "LABEL_2": "positive"}


# --- Input models ---

class TextInput(BaseModel):
    text: str

class UniRecommendInput(BaseModel):
    universities: list
    city: str
    preferred_major: str
    level_of_study: str
    distance_sensitivity: str
    budget_sensitivity: str
    preferred_ranking: int

class ProfRecommendInput(BaseModel):
    professors: list
    preferred_department: str
    course_interest: str
    teaching_style_preference: str
    preferred_rating: float


# --- Sentiment endpoint ---

@app.post("/sentiment")
def sentiment(body: TextInput):
    result = sentiment_pipe(body.text[:512])[0]
    label = SENTIMENT_MAP.get(result["label"], "neutral")
    return {"label": label}


# --- Filter endpoint ---

@app.post("/filter")
def filter_review(body: TextInput):
    result = filter_pipe(body.text[:512])[0]
    approved = result["label"] == "appropriate"
    return {"approved": approved}


# --- University recommendation helpers (from notebook) ---

MAJOR_ALIASES = {
    "technology & it": ["computer science", "it", "information technology", "computing sciences", "data science", "software engineering", "cybersecurity", "technology & it"],
    "arts & humanities": ["arts", "humanities", "languages", "arts & humanities", "english language and literature"],
}

def location_match_score(user_city, university_location, distance_sensitivity):
    same_location = user_city.lower() in university_location.lower()
    if distance_sensitivity == "Low":
        return 10 if same_location else 7
    elif distance_sensitivity == "Medium":
        return 10 if same_location else 4
    elif distance_sensitivity == "High":
        return 10 if same_location else 1
    return 0

def major_match_score(preferred_major, university_majors):
    preferred = preferred_major.lower()
    check_values = MAJOR_ALIASES.get(preferred, [preferred])
    for major in university_majors:
        if major.strip().lower() in check_values:
            return 10
    return 0

def level_match_score(level_of_study, university_levels):
    level_of_study = level_of_study.lower()
    for level in university_levels:
        if level_of_study == level.lower():
            return 10
    return 0

def budget_match_score(user_budget_sensitivity, university_budget_level):
    if user_budget_sensitivity == "Low":
        return 10
    if user_budget_sensitivity == "Medium":
        return max(0, 10 - abs(university_budget_level - 5))
    if user_budget_sensitivity == "High":
        return max(0, 10 - university_budget_level)
    return 0

def ranking_match_score(preferred_ranking, university_ranking):
    difference = abs(preferred_ranking - university_ranking)
    return max(0, 10 - difference * 2.5)

def fuzzy_university_score(major_score, level_score, location_score, budget_score, ranking_score):
    x_input = np.arange(0, 11, 1)
    x_recommendation = np.arange(0, 101, 1)
    low = fuzz.trimf(x_input, [0, 0, 5])
    medium = fuzz.trimf(x_input, [0, 5, 10])
    high = fuzz.trimf(x_input, [5, 10, 10])
    poor = fuzz.trimf(x_recommendation, [0, 0, 50])
    good = fuzz.trimf(x_recommendation, [25, 50, 75])
    excellent = fuzz.trimf(x_recommendation, [50, 100, 100])

    major_high = fuzz.interp_membership(x_input, high, major_score)
    major_low = fuzz.interp_membership(x_input, low, major_score)
    level_high = fuzz.interp_membership(x_input, high, level_score)
    location_high = fuzz.interp_membership(x_input, high, location_score)
    location_medium = fuzz.interp_membership(x_input, medium, location_score)
    location_low = fuzz.interp_membership(x_input, low, location_score)
    budget_high = fuzz.interp_membership(x_input, high, budget_score)
    budget_medium = fuzz.interp_membership(x_input, medium, budget_score)
    budget_low = fuzz.interp_membership(x_input, low, budget_score)
    ranking_high = fuzz.interp_membership(x_input, high, ranking_score)
    ranking_medium = fuzz.interp_membership(x_input, medium, ranking_score)
    ranking_low = fuzz.interp_membership(x_input, low, ranking_score)

    rule1 = np.fmin(np.fmin(major_high, level_high), ranking_high)
    activation_excellent_1 = np.fmin(rule1, excellent)
    rule2 = np.fmin(np.fmin(major_high, budget_high), location_high)
    activation_excellent_2 = np.fmin(rule2, excellent)
    rule3 = np.fmin(major_high, ranking_medium)
    activation_good_1 = np.fmin(rule3, good)
    rule4 = np.fmin(np.fmin(major_high, location_medium), budget_medium)
    activation_good_2 = np.fmin(rule4, good)
    rule5 = major_low
    activation_poor_1 = np.fmin(rule5, poor)
    rule6 = np.fmin(np.fmin(location_low, budget_low), ranking_low)
    activation_poor_2 = np.fmin(rule6, poor)

    aggregated = np.fmax(activation_poor_1, np.fmax(activation_poor_2, np.fmax(
        activation_good_1, np.fmax(activation_good_2, np.fmax(
            activation_excellent_1, activation_excellent_2)))))

    if aggregated.sum() == 0:
        return 0.0
    recommendation_score = fuzz.defuzz(x_recommendation, aggregated, "centroid")
    return float(round(recommendation_score, 2))


# --- Professor recommendation helpers (from notebook) ---

def department_match_score(preferred_department, professor_department):
    return 10 if preferred_department.lower() == professor_department.lower() else 0

def course_match_score(course_interest, professor_courses):
    course_interest = course_interest.lower()
    for course in professor_courses:
        if course_interest == course.lower():
            return 10
    return 0

def teaching_style_match_score(preferred_style, professor_style):
    return 10 if preferred_style.lower() == professor_style.lower() else 0

def rating_match_score(user_preferred_rating, professor_rating):
    difference = abs(user_preferred_rating - professor_rating)
    return max(0, 10 - difference * 2.5)

def fuzzy_professor_score(department_score, course_score, style_score, rating_score):
    x_input = np.arange(0, 11, 1)
    x_recommendation = np.arange(0, 101, 1)
    low = fuzz.trimf(x_input, [0, 0, 5])
    medium = fuzz.trimf(x_input, [0, 5, 10])
    high = fuzz.trimf(x_input, [5, 10, 10])
    poor = fuzz.trimf(x_recommendation, [0, 0, 50])
    good = fuzz.trimf(x_recommendation, [25, 50, 75])
    excellent = fuzz.trimf(x_recommendation, [50, 100, 100])

    department_high = fuzz.interp_membership(x_input, high, department_score)
    department_low = fuzz.interp_membership(x_input, low, department_score)
    course_high = fuzz.interp_membership(x_input, high, course_score)
    course_low = fuzz.interp_membership(x_input, low, course_score)
    style_high = fuzz.interp_membership(x_input, high, style_score)
    style_medium = fuzz.interp_membership(x_input, medium, style_score)
    style_low = fuzz.interp_membership(x_input, low, style_score)
    rating_high = fuzz.interp_membership(x_input, high, rating_score)
    rating_medium = fuzz.interp_membership(x_input, medium, rating_score)
    rating_low = fuzz.interp_membership(x_input, low, rating_score)

    rule1 = np.fmin(np.fmin(course_high, department_high), style_high)
    activation_excellent_1 = np.fmin(rule1, excellent)
    rule2 = np.fmin(np.fmin(course_high, style_high), rating_high)
    activation_excellent_2 = np.fmin(rule2, excellent)
    rule3 = np.fmin(course_high, rating_medium)
    activation_good_1 = np.fmin(rule3, good)
    rule4 = np.fmin(department_high, style_medium)
    activation_good_2 = np.fmin(rule4, good)
    rule5 = course_low
    activation_poor_1 = np.fmin(rule5, poor)
    rule6 = np.fmin(department_low, rating_low)
    activation_poor_2 = np.fmin(rule6, poor)
    rule7 = np.fmin(style_low, rating_low)
    activation_poor_3 = np.fmin(rule7, poor)

    aggregated = np.fmax(activation_poor_1, np.fmax(activation_poor_2, np.fmax(
        activation_poor_3, np.fmax(activation_good_1, np.fmax(
            activation_good_2, np.fmax(activation_excellent_1, activation_excellent_2))))))

    if aggregated.sum() == 0:
        return 0.0
    recommendation_score = fuzz.defuzz(x_recommendation, aggregated, "centroid")
    return float(round(recommendation_score, 2))


# --- Recommendation endpoints ---

@app.post("/recommend/universities")
def recommend_universities(body: UniRecommendInput):
    results = []
    for uni in body.universities:
        if uni.get("IsDeleted"):
            continue
        majors = uni.get("Majors", [])
        if isinstance(majors, str):
            majors = [m.strip() for m in majors.split(",")]
        levels = uni.get("Levels", [])
        if isinstance(levels, str):
            levels = [l.strip() for l in levels.split(",")]

        major_score = major_match_score(body.preferred_major, majors)
        level_score = level_match_score(body.level_of_study, levels)
        location_score = location_match_score(body.city, uni.get("Location", ""), body.distance_sensitivity)
        budget_score = budget_match_score(body.budget_sensitivity, uni.get("BudgetLevel", 5))
        ranking_score = ranking_match_score(body.preferred_ranking, uni.get("Ranking", 3))
        final_score = fuzzy_university_score(major_score, level_score, location_score, budget_score, ranking_score)

        results.append({
            "universityId": uni["UniversityId"],
            "name": uni["Name"],
            "location": uni.get("Location", ""),
            "description": uni.get("Description", ""),
            "websiteUrl": uni.get("WebsiteUrl", ""),
            "imageUrl": uni.get("ImageUrl"),
            "overallRating": uni.get("OverallRating", 0),
            "totalReviews": uni.get("TotalReviews", 0),
            "recommendationScore": final_score,
        })

    results.sort(key=lambda x: x["recommendationScore"], reverse=True)
    return results


@app.post("/recommend/professors")
def recommend_professors(body: ProfRecommendInput):
    results = []
    for prof in body.professors:
        if prof.get("IsDeleted"):
            continue
        courses = prof.get("Courses", [])
        if isinstance(courses, str):
            try:
                courses = json.loads(courses)
            except Exception:
                courses = [c.strip() for c in courses.split(",")]

        dept_score = department_match_score(body.preferred_department, prof.get("Department", ""))
        course_score = course_match_score(body.course_interest, courses)
        style_score = teaching_style_match_score(body.teaching_style_preference, prof.get("TeachingStyle", ""))
        rat_score = rating_match_score(body.preferred_rating, prof.get("Rating", 0))
        final_score = fuzzy_professor_score(dept_score, course_score, style_score, rat_score)

        results.append({
            "professorId": prof["ProfessorId"],
            "fullName": prof.get("FullName", ""),
            "universityId": prof.get("UniversityId"),
            "department": prof.get("Department", ""),
            "specialization": prof.get("Specialization", ""),
            "teachingStyle": prof.get("TeachingStyle", ""),
            "rating": prof.get("Rating", 0),
            "overallRating": prof.get("OverallRating", 0),
            "totalReviews": prof.get("TotalReviews", 0),
            "recommendationScore": final_score,
        })

    results.sort(key=lambda x: x["recommendationScore"], reverse=True)
    return results
