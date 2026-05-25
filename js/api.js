const BASE_URL = 'https://edurate-vd6d.onrender.com';

async function apiFetch(path, options = {}) {
  const token = (typeof AUTH !== 'undefined') ? AUTH.get()?.token : null;
  const headers = { 'Content-Type': 'application/json' };
  if (token) headers['Authorization'] = `Bearer ${token}`;
  const res = await fetch(BASE_URL + path, { ...options, headers });
  if (!res.ok) throw { status: res.status };
  return res.status === 204 ? null : res.json();
}

const API = {
  login:    (email, password)           => apiFetch('/api/auth/login',    { method: 'POST', body: JSON.stringify({ email, password }) }),
  register: (fullName, email, password) => apiFetch('/api/auth/register', { method: 'POST', body: JSON.stringify({ fullName, email, password }) }),
  logout:   ()                          => apiFetch('/api/auth/logout',   { method: 'POST' }),
  me:       ()                          => apiFetch('/api/auth/me'),

  getUniversities: ()   => apiFetch('/api/universities'),
  getUniversity:   (id) => apiFetch(`/api/universities/${id}`),
  getProfessors:   ()   => apiFetch('/api/professors'),
  getProfessor:    (id) => apiFetch(`/api/professors/${id}`),

  getUniversityReviews:    (uniId)  => apiFetch('/api/universityreviews' + (uniId  ? '?universityId=' + uniId  : '')),
  getProfessorReviews:     (profId) => apiFetch('/api/professorreviews'  + (profId ? '?professorId='  + profId : '')),
  getProfessorsByUniversity: (uniId) => apiFetch('/api/professors?universityId=' + uniId),

  createUniversity: (data) => apiFetch('/api/universities', { method: 'POST', body: JSON.stringify(data) }),
  deleteUniversity: (id)   => apiFetch(`/api/universities/${id}`, { method: 'DELETE' }),
  createProfessor:  (data) => apiFetch('/api/professors',  { method: 'POST', body: JSON.stringify(data) }),
  deleteProfessor:  (id)   => apiFetch(`/api/professors/${id}`,  { method: 'DELETE' }),

  postUniversityReview: (uniId, rating, comment) =>
    apiFetch('/api/universityreviews', { method: 'POST', body: JSON.stringify({ universityId: uniId, rating, comment }) }),
  postProfessorReview: (profId, rating, comment) =>
    apiFetch('/api/professorreviews',  { method: 'POST', body: JSON.stringify({ professorId: profId, rating, comment }) }),

  getUniRecommendations: (city, major, level, distanceSensitivity, budgetSensitivity, ranking) =>
    apiFetch(`/api/recommendations/universities?city=${encodeURIComponent(city)}&major=${encodeURIComponent(major)}&level=${encodeURIComponent(level)}&distanceSensitivity=${encodeURIComponent(distanceSensitivity)}&budgetSensitivity=${encodeURIComponent(budgetSensitivity)}&ranking=${ranking}`),
  getProfRecommendations: (department, course, teachingStyle, rating) =>
    apiFetch(`/api/recommendations/professors?department=${encodeURIComponent(department)}&course=${encodeURIComponent(course)}&teachingStyle=${encodeURIComponent(teachingStyle)}&rating=${rating}`),
};

function normalizeUniversity(u) {
  return {
    id: u.universityId,
    name: u.name,
    city: u.location,
    country: '',
    description: u.description,
    website: u.websiteUrl,
    image: u.imageUrl || null,
    overallRating: u.overallRating || 0,
    totalReviews: u.totalReviews || 0,
    ranking: u.ranking || 0,
    ratings: {
      location: 0, reputation: 0, opportunities: 0, happiness: 0,
      internetQuality: 0, facilities: 0, clubsActivities: 0,
      socialLife: 0, foodCafeteria: 0, safety: 0
    },
  };
}

function normalizeProfessor(p) {
  return {
    id: p.professorId,
    name: p.fullName,
    department: p.department,
    universityId: p.universityId,
    universityName: p.universityName || '',
    overallRating: p.overallRating || 0,
    totalReviews: p.totalReviews || 0,
    wouldTakeAgain: 0,
    isVerified: false,
    ratings: { difficulty: 0, workload: 0, friendliness: 0 },
  };
}

function normalizeUniReview(r) {
  return {
    id: r.universityReviewId,
    reviewer: 'Verified Student',
    date: r.createdAt,
    comment: r.comment,
    ratings: { overall: r.rating },
    sentiment: r.sentiment || 'neutral',
  };
}

function normalizeProfReview(r) {
  return {
    id: r.professorReviewId,
    reviewer: 'Verified Student',
    date: r.createdAt,
    comment: r.comment,
    ratings: { overall: r.rating },
    sentiment: r.sentiment || null,
    wouldTakeAgain: null,
    course: '',
    grade: null,
  };
}
