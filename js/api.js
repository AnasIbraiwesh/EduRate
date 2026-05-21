const BASE_URL = 'https://edurate-vd6d.onrender.com';

async function apiFetch(path, options = {}) {
  const res = await fetch(BASE_URL + path, {
    credentials: 'include',
    headers: { 'Content-Type': 'application/json' },
    ...options,
  });
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

  postUniversityReview: (uniId, rating, comment) =>
    apiFetch('/api/universityreviews', { method: 'POST', body: JSON.stringify({ universityId: uniId, rating, comment }) }),
  postProfessorReview: (profId, rating, comment) =>
    apiFetch('/api/professorreviews',  { method: 'POST', body: JSON.stringify({ professorId: profId, rating, comment }) }),
};

function normalizeUniversity(u) {
  return {
    id: u.universityId,
    name: u.name,
    city: u.location,
    country: '',
    description: u.description,
    website: u.websiteUrl,
    image: null,
    overallRating: 0,
    totalReviews: 0,
    ratings: {},
  };
}

function normalizeProfessor(p) {
  return {
    id: p.professorId,
    name: p.fullName,
    department: p.department,
    universityId: p.universityId,
    universityName: p.university?.name || '',
    overallRating: p.rating || 0,
    totalReviews: 0,
    wouldTakeAgain: 0,
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
    sentiment: 'neutral',
  };
}

function normalizeProfReview(r) {
  return {
    id: r.professorReviewId,
    reviewer: 'Verified Student',
    date: r.createdAt,
    comment: r.comment,
    ratings: { overall: r.rating },
    wouldTakeAgain: null,
    course: '',
    grade: null,
  };
}
