# Product Requirements Document — Frontend Design Specification

## University & Professor Rating Platform

---

**Prepared By:** Osama Jankoot (22110020) · Anas Ibraiwish (2111099) · Saif Hamadeh (22110155)
**Supervised By:** Eng. Fadia Alaeddin
**Program:** Bachelor of Science in Cyber Security
**Version:** 1.0 — April 2026

---

## 1. Document Overview

This Product Requirements Document (PRD) defines the complete frontend design specification for the University & Professor Rating Platform. It covers all user-facing pages, components, interactions, UI states, and responsive behavior. This document is intended to be used as the single source of truth when implementing the frontend in Stitch and coding with HTML, CSS (Bootstrap), and .NET backend integration.

> **Design Tool:** Stitch will be used for all UI/UX wireframing and high-fidelity mockups before development. All designs should be exported and reviewed before frontend coding begins.

---

## 2. Design System & Tokens

### 2.1 Color Palette

| Token Name | Hex Value | Usage | Notes |
|---|---|---|---|
| Primary | `#1B4F72` | Headers, CTA buttons, navigation | Dark navy blue |
| Secondary | `#2E86C1` | Links, accents, badges | Medium blue |
| Success | `#27AE60` | Positive sentiment, high ratings | Green |
| Warning | `#F39C12` | Neutral sentiment, medium ratings | Amber |
| Danger | `#E74C3C` | Negative sentiment, errors, low ratings | Red |
| Background | `#F8F9FA` | Page background | Light gray |
| Surface | `#FFFFFF` | Cards, modals, inputs | White |
| Text Primary | `#2C3E50` | Body text | Dark charcoal |
| Text Secondary | `#7F8C8D` | Captions, hints, metadata | Muted gray |
| Star Fill | `#F1C40F` | Filled rating stars | Gold |
| Star Empty | `#D5D8DC` | Empty rating stars | Light gray |

### 2.2 Typography

| Element | Font | Size | Weight | Line Height |
|---|---|---|---|---|
| H1 — Page Title | Inter / Poppins | 32px | 700 (Bold) | 1.3 |
| H2 — Section Title | Inter / Poppins | 24px | 600 (Semi) | 1.3 |
| H3 — Card Title | Inter / Poppins | 20px | 600 | 1.4 |
| Body | Inter | 16px | 400 | 1.6 |
| Caption / Meta | Inter | 14px | 400 | 1.5 |
| Button Text | Inter | 16px | 600 | 1.0 |

### 2.3 Spacing & Grid

- **Base spacing unit:** 8px
- **Grid system:** Bootstrap 12-column grid
- **Container max-width:** 1200px (centered)
- **Card border-radius:** 12px
- **Button border-radius:** 8px
- **Shadow (cards):** `0 2px 8px rgba(0,0,0,0.08)`
- **Shadow (hover):** `0 4px 16px rgba(0,0,0,0.12)`

### 2.4 Responsive Breakpoints

| Breakpoint | Width | Layout |
|---|---|---|
| Mobile | < 576px | Single column, hamburger nav, stacked cards |
| Tablet | 576px – 992px | Two columns, collapsible sidebar |
| Desktop | > 992px | Full layout, side-by-side comparison |

---

## 3. Global Components

### 3.1 Navigation Bar

Sticky top navigation bar present on all pages. Contains the platform logo on the left, centered search bar, and right-aligned user actions.

- **Logo:** Platform logo/wordmark linking to home page
- **Search Bar:** Unified search input with placeholder "Search universities or professors…". Shows autocomplete dropdown with results grouped by type (University / Professor)
- **Nav Links:** Home, Universities, Professors, Compare
- **Auth Buttons:** Login / Sign Up buttons when logged out; user avatar dropdown when logged in (Profile, My Reviews, Logout)
- **Mobile:** Collapse to hamburger menu; search icon expands search bar overlay

### 3.2 Footer

Present on all pages. Dark background (`#1B4F72`) with white text.

- **Column 1 — About Us:** Brief platform description
- **Column 2 — Quick Links:** Home, Universities, Professors, FAQ
- **Column 3 — Contact:** Email, social media icons
- **Column 4 — Legal:** Privacy Policy, Terms of Service

### 3.3 Rating Stars Component

Reusable star rating display and input component used throughout the platform.

- **Display Mode:** Shows filled/half/empty stars (1–5 scale) with numeric value beside it
- **Input Mode:** Interactive hover-to-select stars with tooltip showing label (e.g., "4 – Very Good")
- **Colors:** Gold (`#F1C40F`) for filled, light gray (`#D5D8DC`) for empty

### 3.4 Sentiment Badge

Small colored badge displayed on each review indicating the sentiment analysis result.

- **Positive:** Green badge with thumbs-up icon
- **Neutral:** Amber badge with dash icon
- **Negative:** Red badge with thumbs-down icon

### 3.5 Toast & Alert System

- **Success Toast:** Green, auto-dismiss after 4s — "Review submitted successfully"
- **Error Toast:** Red, persistent until dismissed — validation or server errors
- **Info Toast:** Blue, auto-dismiss — informational messages

---

## 4. Page Specifications

### 4.1 Authentication Pages

#### 4.1.1 Sign Up Page

Clean centered card layout on a light background.

| Field | Type | Validation |
|---|---|---|
| Full Name | Text input | Required, min 2 characters |
| University Email | Email input | Required, must be `.edu` or university domain |
| Password | Password input with show/hide toggle | Min 8 chars, 1 uppercase, 1 number |
| Confirm Password | Password input | Must match password |
| University | Searchable dropdown | Required, select from database |
| Role | Radio buttons: Student / Professor | Required |

- **CTA Button:** "Create Account" — Primary color, full-width
- Below CTA: "Already have an account? Log in" link
- Email verification flow: show confirmation screen after submission

#### 4.1.2 Login Page

Same centered card layout.

- Email input field
- Password input with show/hide toggle
- "Remember Me" checkbox
- "Forgot Password?" link
- **CTA Button:** "Log In"
- Below CTA: "Don't have an account? Sign up" link

---

### 4.2 Home Page

The landing page and primary entry point for all users.

#### 4.2.1 Hero Section

- **Layout:** Full-width banner with gradient overlay on background image
- **Headline:** Large bold text — "Find the Right University. Choose the Best Professor."
- **Subheadline:** "Real reviews. Real data. Real decisions."
- **Search Bar:** Large centered search input with "Search universities or professors…" placeholder and search icon button
- **Quick Action Buttons:** Two pill buttons below search — "Browse Universities" and "Browse Professors"

#### 4.2.2 How It Works Section

Three-step horizontal card layout with icons:

1. **Search** — Find your university or professor
2. **Read & Compare** — View ratings, reviews, and comparisons
3. **Decide** — Make informed decisions with confidence

#### 4.2.3 Top Rated Section

Two side-by-side carousels:

- **Top Universities:** Horizontal scroll of university cards showing name, overall rating, and number of reviews
- **Top Professors:** Horizontal scroll of professor cards showing name, department, rating, and "Would Take Again" percentage

#### 4.2.4 Statistics Bar

Animated counter section showing platform stats: Total Universities Rated, Total Professors Rated, Total Reviews Submitted, Active Student Users.

---

### 4.3 University Module

#### 4.3.1 University Listing Page

- **Search Bar:** Filter universities by name
- **Filters Sidebar:** City/Location dropdown, rating range slider (1–5), sort by (Highest Rated, Most Reviewed, Alphabetical)
- **Results Grid:** Cards in 3-column grid (desktop) / 2-column (tablet) / 1-column (mobile)

Each university card contains: university name and city, overall average rating (stars + numeric), total number of reviews, mini bar chart showing top 3 category ratings, and a "View Details" button.

#### 4.3.2 University Profile Page

**Header Banner:**
- University name (H1), city, and country
- Overall average rating prominently displayed
- Total review count
- "Write a Review" CTA button (visible only to logged-in students)
- "Compare" button

**Rating Categories Grid:**

A visual grid or horizontal bar chart showing average scores (1–5) for each category:

| Category | Display |
|---|---|
| Location | Star rating + progress bar |
| Reputation | Star rating + progress bar |
| Opportunities | Star rating + progress bar |
| Happiness | Star rating + progress bar |
| Internet Quality | Star rating + progress bar |
| Facilities | Star rating + progress bar |
| Clubs & Activities | Star rating + progress bar |
| Social Life | Star rating + progress bar |
| Food & Cafeteria | Star rating + progress bar |
| Safety | Star rating + progress bar |

**National Averages Chart:**
A radar/spider chart or grouped bar chart comparing this university's scores against the national average across all categories.

**Reviews Section:**
- Sort by: Most Recent, Highest Rated, Lowest Rated
- Each review card: anonymous username label ("Verified Student"), date, star ratings per category, written comment, sentiment badge (Positive/Neutral/Negative)
- Pagination: 10 reviews per page with load more button

**Professors at this University:**
A linked section listing professors associated with this university, each with name, department, and rating. Clicking navigates to the professor profile page.

#### 4.3.3 University Review Submission Modal

A modal overlay triggered by "Write a Review" button. Only accessible to verified, logged-in students.

| Field | Type | Required? |
|---|---|---|
| Location (1–5) | Star input | Yes |
| Reputation (1–5) | Star input | Yes |
| Opportunities (1–5) | Star input | Yes |
| Happiness (1–5) | Star input | Yes |
| Internet Quality (1–5) | Star input | Yes |
| Facilities (1–5) | Star input | Yes |
| Clubs & Activities (1–5) | Star input | Yes |
| Social Life (1–5) | Star input | Yes |
| Food & Cafeteria (1–5) | Star input | Yes |
| Safety (1–5) | Star input | Yes |
| Written Review | Textarea (min 50 chars) | Yes |

- Submit button with loading spinner
- Confirmation toast on success
- Abusive language filter: show inline warning if detected

#### 4.3.4 University Comparison Page

Side-by-side comparison of two selected universities.

- **Selection:** Two searchable dropdowns at the top to pick universities
- **Layout:** Two-column vertical layout (desktop) / stacked cards (mobile)
- **Content:** For each category, display both universities' ratings with colored bar charts for visual contrast
- **Winner Highlight:** The higher-rated category cell gets a subtle green highlight
- **Summary:** Bottom section shows overall winner and category-by-category breakdown

---

### 4.4 Professor Module

#### 4.4.1 Professor Listing Page

- **Search Bar:** Search by professor name
- **Filter Sidebar:** Department filter dropdown (CS, AI, Business, Engineering, etc.), University filter, Rating range
- **Results Grid:** Cards in 3-column grid with professor name, department, university, overall rating, "Would Take Again" percentage

#### 4.4.2 Professor Profile Page

**Profile Header:**
- Professor name (H1)
- Department and university affiliation
- List of courses taught (as tags/chips)
- Overall average rating (large star display)
- "Would Take Again" percentage prominently shown
- Verified Professor badge (if applicable)
- "Rate This Professor" CTA button
- "Compare" button

**Rating Summary:**

| Metric | Display |
|---|---|
| Difficulty (1–5) | Progress bar + numeric value |
| Workload (1–5) | Progress bar + numeric value |
| Friendliness / Communication (1–5) | Progress bar + numeric value |
| Would Take Again | Percentage display with donut chart |

**Reviews Section:**
- **Course Filter:** Dropdown to filter reviews by specific course (e.g., "Physics 101", "Math 201")
- **Sort Options:** Most Recent, Highest, Lowest
- **Review Card:** Displays "Verified Student" label, course name tag, date, difficulty/workload/friendliness ratings, grade received, written comment, sentiment badge
- Pagination: 10 per page

**Professor Reply (Premium):**
If the professor has verified their profile, their replies appear indented below the student review with a "Professor Response" label and verified badge.

#### 4.4.3 Professor Review Submission Modal

| Field | Type | Required? |
|---|---|---|
| Course | Searchable dropdown | Yes |
| Would Take Again | Yes / No toggle | Yes |
| Difficulty (1–5) | Star input | Yes |
| Workload (1–5) | Star input | Yes |
| Friendliness / Communication (1–5) | Star input | Yes |
| Grade Received | Dropdown (A, B, C, D, F, Prefer not to say) | Optional |
| Written Review | Textarea (min 50 chars) | Yes |

#### 4.4.4 Professor Comparison Page

Same pattern as university comparison but with professor-specific metrics.

- **Selection:** Two searchable dropdowns, optional course filter to compare on the same course
- **Metrics Compared:** Overall rating, difficulty, workload, friendliness, "Would Take Again" percentage
- **Charts:** Side-by-side bar charts or radar chart

---

### 4.5 Comparison Hub Page

A dedicated page accessible from the main nav where users can initiate any comparison.

- **Toggle:** Switch between "Compare Universities" and "Compare Professors" modes
- **Flow:** Select first item from dropdown → select second item → click "Compare" to load side-by-side view

---

### 4.6 Recommendations Page

Personalized recommendation section available to logged-in users.

- **University Recommendations:** Based on user preferences, browsing history, and rating patterns. Shows cards with match percentage
- **Professor Recommendations:** Suggested professors based on courses of interest and preferred teaching styles
- **How It Works:** Brief explainer card — "Our system analyzes your preferences and ratings to suggest the best matches."

---

### 4.7 FAQ Page

Accordion-style expandable question/answer list.

- Grouped by topic: General, Ratings, Reviews, Comparison, Account
- Each item has a question as the clickable header and answer that expands below
- Search bar at the top to filter FAQs
- Contact support link at the bottom

---

### 4.8 Professor Dashboard (Logged-in Professor View)

Accessible only to users with the "Professor" role.

- **Overview Tab:** Overall rating, total reviews, "Would Take Again" %, sentiment breakdown pie chart
- **Reviews Tab:** List of all reviews with course filter and sentiment filter. Read-only — no edit or delete controls
- **Trends Tab:** Line chart showing rating trends over semesters
- **Premium — Reply:** If verified, reply button appears on each review
- **Premium — CV Upload:** Upload and display CV/teaching philosophy

---

## 5. UI States & Interactions

### 5.1 Loading States

- Skeleton loaders for cards, profiles, and review lists
- Spinner overlay for form submissions
- Progress bar for comparison data loading

### 5.2 Empty States

- **No reviews yet:** Illustration + "Be the first to review!" CTA
- **No search results:** "No results found. Try a different search."
- **No recommendations:** "Rate more universities and professors to get personalized suggestions."

### 5.3 Error States

- **Form validation:** Inline red error messages below each field
- **Server error:** Full-page error card with retry button
- **Network error:** Banner at top — "You appear to be offline"

### 5.4 Hover & Active States

- **Cards:** Subtle shadow elevation on hover, slight scale (`1.01`)
- **Buttons:** Darken 10% on hover, `scale(0.98)` on active
- **Stars:** Glow effect on hover in input mode
- **Links:** Underline on hover, color shift to secondary

---

## 6. Accessibility Requirements

- WCAG 2.1 AA compliance minimum
- All interactive elements must be keyboard navigable (Tab, Enter, Escape)
- Color contrast ratio: minimum 4.5:1 for body text, 3:1 for large text
- ARIA labels on all icons, star ratings, and interactive components
- Screen reader support for sentiment badges and chart data
- Focus indicators visible on all focusable elements
- Alt text for all images; decorative images marked `aria-hidden`

---

## 7. Frontend Technology Stack

| Technology | Purpose | Notes |
|---|---|---|
| HTML5 | Markup & structure | Semantic elements throughout |
| CSS3 | Styling | Custom properties for theme tokens |
| Bootstrap 5 | Responsive grid & components | Customized theme with project colors |
| Chart.js or Recharts | Data visualization | Bar, radar, donut, line charts |
| Stitch | UI/UX design & wireframing | Design-to-code workflow |
| .NET (Backend) | Server-side rendering & APIs | Frontend consumes REST APIs |

---

## 8. Complete Page Map

| # | Page | Route | Access |
|---|---|---|---|
| 1 | Home | `/` | Public |
| 2 | Sign Up | `/register` | Public (redirects if logged in) |
| 3 | Login | `/login` | Public (redirects if logged in) |
| 4 | University Listing | `/universities` | Public |
| 5 | University Profile | `/universities/{id}` | Public |
| 6 | University Comparison | `/compare/universities` | Public |
| 7 | Professor Listing | `/professors` | Public |
| 8 | Professor Profile | `/professors/{id}` | Public |
| 9 | Professor Comparison | `/compare/professors` | Public |
| 10 | Comparison Hub | `/compare` | Public |
| 11 | Recommendations | `/recommendations` | Authenticated (Student) |
| 12 | Professor Dashboard | `/dashboard` | Authenticated (Professor) |
| 13 | FAQ | `/faq` | Public |

---

## 9. Design Deliverables Checklist

The following deliverables should be completed in Stitch before frontend development begins:

| # | Deliverable | Priority | Status |
|---|---|---|---|
| 1 | Design System (colors, typography, spacing, components) | P0 — Critical | Pending |
| 2 | Home Page wireframe + high-fidelity | P0 — Critical | Pending |
| 3 | Authentication pages (Login, Sign Up) | P0 — Critical | Pending |
| 4 | University Listing + Profile pages | P0 — Critical | Pending |
| 5 | Professor Listing + Profile pages | P0 — Critical | Pending |
| 6 | Review submission modals (University + Professor) | P0 — Critical | Pending |
| 7 | Comparison pages (University + Professor) | P1 — High | Pending |
| 8 | Recommendations page | P1 — High | Pending |
| 9 | Professor Dashboard | P1 — High | Pending |
| 10 | FAQ page | P2 — Medium | Pending |
| 11 | Mobile responsive variants for all pages | P1 — High | Pending |
| 12 | Loading, empty, and error state designs | P2 — Medium | Pending |

---

> **Next Step:** Begin wireframing in Stitch following the specifications in this document. Start with P0 (Critical) deliverables: Design System, Home Page, Auth Pages, University Module, and Professor Module. Refer to Section 2 for all design tokens.
