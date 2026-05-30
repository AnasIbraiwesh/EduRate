const BASE_URL = (location.hostname === 'localhost' || location.hostname === '127.0.0.1')
  ? 'https://edurate-vd6d.onrender.com'
  : '';

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
  register: (fullName, email, password, universityId) => apiFetch('/api/auth/register', { method: 'POST', body: JSON.stringify({ fullName, email, password, universityId }) }),
  logout:   ()                          => apiFetch('/api/auth/logout',   { method: 'POST' }),
  me:       ()                          => apiFetch('/api/auth/me'),
  updateUniversity: (universityId)      => apiFetch('/api/auth/me/university', { method: 'PUT', body: JSON.stringify({ universityId }) }),

  getUniversities: ()   => apiFetch('/api/universities'),
  getUniversity:   (id) => apiFetch(`/api/universities/${id}`),
  getProfessors:   ()   => apiFetch('/api/professors'),
  getProfessor:    (id) => apiFetch(`/api/professors/${id}`),

  getUniversityReviews:    (uniId)  => apiFetch('/api/universityreviews' + (uniId  ? '?universityId=' + uniId  : '')),
  getProfessorReviews:     (profId) => apiFetch('/api/professorreviews'  + (profId ? '?professorId='  + profId : '')),
  getCourses:              (profId) => apiFetch('/api/courses?professorId=' + profId),
  getProfessorsByUniversity: (uniId) => apiFetch('/api/professors?universityId=' + uniId),

  createUniversity: (data) => apiFetch('/api/universities', { method: 'POST', body: JSON.stringify(data) }),
  deleteUniversity: (id)   => apiFetch(`/api/universities/${id}`, { method: 'DELETE' }),
  createProfessor:  (data) => apiFetch('/api/professors',  { method: 'POST', body: JSON.stringify(data) }),
  deleteProfessor:  (id)   => apiFetch(`/api/professors/${id}`,  { method: 'DELETE' }),

  postUniversityReview: (uniId, rating, categories, comment) =>
    apiFetch('/api/universityreviews', { method: 'POST', body: JSON.stringify({
      universityId: uniId, rating,
      categoriesJson: JSON.stringify(categories),
      comment
    }) }),
  postProfessorReview: (profId, rating, comment, wouldTakeAgain) =>
    apiFetch('/api/professorreviews',  { method: 'POST', body: JSON.stringify({ professorId: profId, rating, comment, wouldTakeAgain }) }),

  getUniRecommendations: (city, major, level, distanceSensitivity, budgetSensitivity, ranking) =>
    apiFetch(`/api/recommendations/universities?city=${encodeURIComponent(city)}&major=${encodeURIComponent(major)}&level=${encodeURIComponent(level)}&distanceSensitivity=${encodeURIComponent(distanceSensitivity)}&budgetSensitivity=${encodeURIComponent(budgetSensitivity)}&ranking=${ranking}`),
  getProfRecommendations: (department, course, teachingStyle, rating) =>
    apiFetch(`/api/recommendations/professors?department=${encodeURIComponent(department)}&course=${encodeURIComponent(course)}&teachingStyle=${encodeURIComponent(teachingStyle)}&rating=${rating}`),
};

function normalizeUniversity(u) {
  const locParts = (u.location || '').split(',');
  const normName = s => s.toLowerCase().replace(/^the\s+/, '').replace(/[-\s]+/g, ' ').trim();
  const local = (typeof UNIVERSITIES !== 'undefined')
    ? UNIVERSITIES.find(lu => normName(lu.name) === normName(u.name))
    : null;
  const r = u.overallRating ? +u.overallRating.toFixed(2) : (local ? local.overallRating : 0);
  return {
    id: u.universityId,
    name: u.name,
    city: locParts[0].trim(),
    country: locParts[1] ? locParts[1].trim() : 'Jordan',
    description: u.description,
    website: u.websiteUrl,
    image: (local && local.image) ? local.image : u.imageUrl,
    overallRating: r,
    totalReviews: u.totalReviews || (local ? local.totalReviews : 0),
    ranking: u.ranking || 0,
    ratings: {
      location: r, reputation: r, opportunities: r, happiness: r,
      internetQuality: r, facilities: r, clubsActivities: r,
      socialLife: r, foodCafeteria: r, safety: r
    },
  };
}

function normalizeProfessor(p) {
  let courses = [];
  try { courses = JSON.parse(p.coursesJson || '[]'); } catch (_) {}
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
    courses,
    ratings: { difficulty: p.overallRating || 0, workload: p.overallRating || 0, friendliness: p.overallRating || 0 },
  };
}

function normalizeUniReview(r) {
  let cats = {};
  try { cats = r.categoriesJson ? JSON.parse(r.categoriesJson) : {}; } catch(_) {}
  return {
    id: r.universityReviewId,
    reviewer: 'Verified Student',
    date: r.createdAt,
    comment: r.comment,
    ratings: { overall: r.rating, ...cats },
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
    wouldTakeAgain: r.wouldTakeAgain ?? null,
    course: '',
    grade: null,
  };
}
