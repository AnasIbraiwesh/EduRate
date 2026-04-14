/* ============================================================
   EduRate — Mock Data Store
   Replace fetch() calls here when .NET API is ready.
   ============================================================ */

// ── Universities ──────────────────────────────────────────────
const UNIVERSITIES = [
  {
    id: 1,
    name: "University of Jordan",
    shortName: "UJ",
    city: "Amman",
    country: "Jordan",
    website: "http://www.ju.edu.jo",
    founded: 1962,
    description: "The University of Jordan is a prestigious public research university located in Amman, Jordan. It is one of the oldest and largest universities in the country.",
    totalReviews: 148,
    ratings: {
      location: 4.3,
      reputation: 4.5,
      opportunities: 3.9,
      happiness: 3.7,
      internetQuality: 3.2,
      facilities: 4.0,
      clubsActivities: 3.5,
      socialLife: 3.8,
      foodCafeteria: 3.3,
      safety: 4.4
    },
    overallRating: 3.86,
    professors: [1, 2, 5]
  },
  {
    id: 2,
    name: "Jordan University of Science and Technology",
    shortName: "JUST",
    city: "Irbid",
    country: "Jordan",
    website: "http://www.just.edu.jo",
    founded: 1986,
    description: "JUST is a Jordanian public university known for engineering, medicine, and applied sciences programs.",
    totalReviews: 112,
    ratings: {
      location: 3.6,
      reputation: 4.4,
      opportunities: 4.2,
      happiness: 3.9,
      internetQuality: 3.8,
      facilities: 4.3,
      clubsActivities: 3.6,
      socialLife: 3.5,
      foodCafeteria: 3.4,
      safety: 4.2
    },
    overallRating: 3.89,
    professors: [3, 6]
  },
  {
    id: 3,
    name: "Princess Sumaya University for Technology",
    shortName: "PSUT",
    city: "Amman",
    country: "Jordan",
    website: "http://www.psut.edu.jo",
    founded: 1991,
    description: "PSUT is a private Jordanian university specializing in technology and ICT, part of the Royal Scientific Society.",
    totalReviews: 87,
    ratings: {
      location: 4.1,
      reputation: 4.2,
      opportunities: 4.5,
      happiness: 4.2,
      internetQuality: 4.6,
      facilities: 4.4,
      clubsActivities: 4.0,
      socialLife: 4.1,
      foodCafeteria: 3.7,
      safety: 4.5
    },
    overallRating: 4.23,
    professors: [4, 7]
  },
  {
    id: 4,
    name: "German Jordanian University",
    shortName: "GJU",
    city: "Madaba",
    country: "Jordan",
    website: "http://www.gju.edu.jo",
    founded: 2005,
    description: "GJU is a public university that combines Jordanian academic standards with German dual-education principles.",
    totalReviews: 64,
    ratings: {
      location: 3.4,
      reputation: 4.0,
      opportunities: 4.6,
      happiness: 4.1,
      internetQuality: 4.0,
      facilities: 4.1,
      clubsActivities: 3.8,
      socialLife: 3.9,
      foodCafeteria: 3.6,
      safety: 4.0
    },
    overallRating: 3.95,
    professors: [8]
  },
  {
    id: 5,
    name: "Yarmouk University",
    shortName: "YU",
    city: "Irbid",
    country: "Jordan",
    website: "http://www.yu.edu.jo",
    founded: 1976,
    description: "Yarmouk University is a public Jordanian university known for its arts, humanities, and science programs.",
    totalReviews: 93,
    ratings: {
      location: 3.7,
      reputation: 3.8,
      opportunities: 3.4,
      happiness: 3.9,
      internetQuality: 2.9,
      facilities: 3.6,
      clubsActivities: 4.0,
      socialLife: 4.2,
      foodCafeteria: 3.5,
      safety: 4.1
    },
    overallRating: 3.71,
    professors: [2, 5]
  },
  {
    id: 6,
    name: "Al-Balqa Applied University",
    shortName: "BAU",
    city: "Salt",
    country: "Jordan",
    website: "http://www.bau.edu.jo",
    founded: 1997,
    description: "Al-Balqa Applied University is a public Jordanian university with a focus on applied and technical education.",
    totalReviews: 75,
    ratings: {
      location: 3.3,
      reputation: 3.5,
      opportunities: 3.3,
      happiness: 3.6,
      internetQuality: 2.8,
      facilities: 3.4,
      clubsActivities: 3.2,
      socialLife: 3.7,
      foodCafeteria: 3.1,
      safety: 3.9
    },
    overallRating: 3.38,
    professors: [6]
  }
];

const UNIVERSITY_CATEGORIES = [
  { key: "location",       label: "Location" },
  { key: "reputation",     label: "Reputation" },
  { key: "opportunities",  label: "Opportunities" },
  { key: "happiness",      label: "Happiness" },
  { key: "internetQuality",label: "Internet Quality" },
  { key: "facilities",     label: "Facilities" },
  { key: "clubsActivities",label: "Clubs & Activities" },
  { key: "socialLife",     label: "Social Life" },
  { key: "foodCafeteria",  label: "Food & Cafeteria" },
  { key: "safety",         label: "Safety" }
];

// ── Professors ────────────────────────────────────────────────
const PROFESSORS = [
  {
    id: 1,
    name: "Dr. Ahmad Khalil",
    department: "Computer Science",
    university: 1,
    universityName: "University of Jordan",
    courses: ["Data Structures", "Algorithms", "Programming Fundamentals"],
    totalReviews: 72,
    isVerified: true,
    ratings: {
      difficulty:    3.8,
      workload:      4.0,
      friendliness:  4.2
    },
    wouldTakeAgain: 78,
    overallRating: 4.0
  },
  {
    id: 2,
    name: "Dr. Maha Saleh",
    department: "Mathematics",
    university: 1,
    universityName: "University of Jordan",
    courses: ["Calculus I", "Calculus II", "Linear Algebra", "Discrete Math"],
    totalReviews: 58,
    isVerified: false,
    ratings: {
      difficulty:    4.1,
      workload:      4.3,
      friendliness:  3.6
    },
    wouldTakeAgain: 55,
    overallRating: 3.5
  },
  {
    id: 3,
    name: "Dr. Omar Hamdan",
    department: "Electrical Engineering",
    university: 2,
    universityName: "Jordan University of Science and Technology",
    courses: ["Circuit Analysis", "Digital Electronics", "Signal Processing"],
    totalReviews: 44,
    isVerified: true,
    ratings: {
      difficulty:    4.5,
      workload:      4.4,
      friendliness:  3.4
    },
    wouldTakeAgain: 48,
    overallRating: 3.4
  },
  {
    id: 4,
    name: "Dr. Rania Nasser",
    department: "Cybersecurity",
    university: 3,
    universityName: "Princess Sumaya University for Technology",
    courses: ["Network Security", "Cryptography", "Ethical Hacking", "Cyber Forensics"],
    totalReviews: 61,
    isVerified: true,
    ratings: {
      difficulty:    3.5,
      workload:      3.6,
      friendliness:  4.8
    },
    wouldTakeAgain: 91,
    overallRating: 4.6
  },
  {
    id: 5,
    name: "Dr. Khalid Abu-Baker",
    department: "Business Administration",
    university: 1,
    universityName: "University of Jordan",
    courses: ["Principles of Management", "Marketing", "Business Ethics"],
    totalReviews: 38,
    isVerified: false,
    ratings: {
      difficulty:    2.8,
      workload:      2.9,
      friendliness:  4.5
    },
    wouldTakeAgain: 82,
    overallRating: 4.1
  },
  {
    id: 6,
    name: "Dr. Samia Ibrahim",
    department: "Artificial Intelligence",
    university: 2,
    universityName: "Jordan University of Science and Technology",
    courses: ["Machine Learning", "Deep Learning", "Computer Vision", "NLP Basics"],
    totalReviews: 53,
    isVerified: true,
    ratings: {
      difficulty:    4.2,
      workload:      4.0,
      friendliness:  4.6
    },
    wouldTakeAgain: 87,
    overallRating: 4.4
  },
  {
    id: 7,
    name: "Dr. Hassan Al-Zoubi",
    department: "Software Engineering",
    university: 3,
    universityName: "Princess Sumaya University for Technology",
    courses: ["Software Design Patterns", "Agile Development", "System Analysis"],
    totalReviews: 47,
    isVerified: false,
    ratings: {
      difficulty:    3.2,
      workload:      3.4,
      friendliness:  4.3
    },
    wouldTakeAgain: 76,
    overallRating: 4.0
  },
  {
    id: 8,
    name: "Dr. Lina Mustafa",
    department: "Industrial Engineering",
    university: 4,
    universityName: "German Jordanian University",
    courses: ["Operations Research", "Quality Management", "Project Management"],
    totalReviews: 29,
    isVerified: true,
    ratings: {
      difficulty:    3.6,
      workload:      3.8,
      friendliness:  4.7
    },
    wouldTakeAgain: 85,
    overallRating: 4.3
  }
];

const PROFESSOR_CATEGORIES = [
  { key: "difficulty",   label: "Difficulty" },
  { key: "workload",     label: "Workload" },
  { key: "friendliness", label: "Friendliness / Communication" }
];

// ── University Reviews ────────────────────────────────────────
const REVIEWS_UNIVERSITY = {
  1: [
    { id: 101, reviewer: "Student_2841", date: "2025-12-10", ratings: { location: 5, reputation: 5, opportunities: 4, happiness: 4, internetQuality: 3, facilities: 4, clubsActivities: 3, socialLife: 4, foodCafeteria: 3, safety: 5 }, comment: "The University of Jordan has an amazing reputation and the campus is beautiful. The location in Amman is very convenient. However, the internet connection could definitely use improvement. The professors are generally knowledgeable and dedicated.", sentiment: "positive" },
    { id: 102, reviewer: "Student_5503", date: "2025-11-22", ratings: { location: 4, reputation: 4, opportunities: 3, happiness: 3, internetQuality: 2, facilities: 4, clubsActivities: 4, socialLife: 4, foodCafeteria: 3, safety: 4 }, comment: "Good university overall with lots of clubs to join. The cafeteria food is mediocre at best. Internet is the biggest issue — it drops constantly during exams. Reputation is solid for employers though.", sentiment: "neutral" },
    { id: 103, reviewer: "Student_7721", date: "2025-10-05", ratings: { location: 4, reputation: 5, opportunities: 4, happiness: 4, internetQuality: 3, facilities: 4, clubsActivities: 3, socialLife: 3, foodCafeteria: 2, safety: 5 }, comment: "Strong academic programs especially in science and engineering. The safety on campus is excellent. Food options are very limited though, and the cafeteria gets extremely crowded.", sentiment: "positive" },
    { id: 104, reviewer: "Student_1190", date: "2025-09-14", ratings: { location: 5, reputation: 4, opportunities: 4, happiness: 2, internetQuality: 2, facilities: 3, clubsActivities: 2, socialLife: 3, foodCafeteria: 2, safety: 4 }, comment: "The location is great and the university has a good name, but I'm very disappointed with the student support services. Happiness is low because the administration doesn't listen to students. Needs major improvement.", sentiment: "negative" },
    { id: 105, reviewer: "Student_3366", date: "2025-08-30", ratings: { location: 4, reputation: 5, opportunities: 5, happiness: 5, internetQuality: 4, facilities: 5, clubsActivities: 5, socialLife: 5, foodCafeteria: 4, safety: 5 }, comment: "Absolutely love this university! Best decision I ever made. The opportunities here are incredible — internships, research programs, and partnerships with major companies. The facilities were recently renovated and look amazing.", sentiment: "positive" }
  ],
  2: [
    { id: 201, reviewer: "Student_8812", date: "2025-12-01", ratings: { location: 3, reputation: 5, opportunities: 5, happiness: 4, internetQuality: 4, facilities: 5, clubsActivities: 4, socialLife: 3, foodCafeteria: 3, safety: 4 }, comment: "JUST has world-class facilities and the engineering programs are top-notch. Very demanding academically but worth it. The location in Irbid is a bit far from Amman but the campus infrastructure makes up for it.", sentiment: "positive" },
    { id: 202, reviewer: "Student_2244", date: "2025-11-07", ratings: { location: 4, reputation: 4, opportunities: 4, happiness: 4, internetQuality: 4, facilities: 4, clubsActivities: 3, socialLife: 4, foodCafeteria: 4, safety: 4 }, comment: "Solid university with good programs. The reputation of JUST graduates is respected in the job market. The workload is heavy but manageable. Decent food and campus life.", sentiment: "neutral" }
  ],
  3: [
    { id: 301, reviewer: "Student_9901", date: "2025-12-20", ratings: { location: 4, reputation: 4, opportunities: 5, happiness: 4, internetQuality: 5, facilities: 5, clubsActivities: 4, socialLife: 4, foodCafeteria: 4, safety: 5 }, comment: "PSUT is the best technology university in Jordan. The internet speed is amazing (fiber optic campus-wide), labs are state of the art, and the industry connections bring real job opportunities. Highly recommended for tech students.", sentiment: "positive" },
    { id: 302, reviewer: "Student_4417", date: "2025-10-15", ratings: { location: 4, reputation: 4, opportunities: 4, happiness: 5, internetQuality: 5, facilities: 4, clubsActivities: 4, socialLife: 4, foodCafeteria: 3, safety: 4 }, comment: "The community at PSUT is very friendly and supportive. The professors are approachable and the class sizes are small which means you get individual attention. The cafeteria could be improved but everything else is great.", sentiment: "positive" }
  ],
  4: [
    { id: 401, reviewer: "Student_6635", date: "2025-11-18", ratings: { location: 3, reputation: 4, opportunities: 5, happiness: 4, internetQuality: 4, facilities: 4, clubsActivities: 4, socialLife: 4, foodCafeteria: 4, safety: 4 }, comment: "GJU's dual education system is genuinely unique in Jordan. The semester abroad program in Germany is an incredible experience. The campus is a bit isolated in Madaba but the education quality is exceptional.", sentiment: "positive" }
  ],
  5: [
    { id: 501, reviewer: "Student_3358", date: "2025-12-05", ratings: { location: 3, reputation: 4, opportunities: 3, happiness: 4, internetQuality: 2, facilities: 3, clubsActivities: 5, socialLife: 5, foodCafeteria: 3, safety: 4 }, comment: "Yarmouk has the best social life out of any university in Jordan. The student clubs are amazing and there are events every week. Internet is terrible but you come here for the experience. Academics are decent but not outstanding.", sentiment: "neutral" }
  ],
  6: [
    { id: 601, reviewer: "Student_7792", date: "2025-10-22", ratings: { location: 3, reputation: 3, opportunities: 3, happiness: 3, internetQuality: 2, facilities: 3, clubsActivities: 3, socialLife: 4, foodCafeteria: 3, safety: 4 }, comment: "BAU is an average university. It serves its purpose for practical and technical education but don't expect world-class facilities or top-tier research. For the price though, it's reasonable value.", sentiment: "neutral" }
  ]
};

// ── Professor Reviews ─────────────────────────────────────────
const REVIEWS_PROFESSOR = {
  1: [
    { id: 1001, reviewer: "Student_4421", date: "2025-12-08", course: "Data Structures", wouldTakeAgain: true, ratings: { difficulty: 4, workload: 4, friendliness: 4 }, grade: "A", comment: "Dr. Ahmad is an outstanding professor. His explanations of complex algorithms are very clear. He uses real-world examples that make the material much easier to understand. Highly recommend taking any course with him.", sentiment: "positive", profReply: "Thank you for the kind feedback! It's always rewarding to see students connect theory to practice." },
    { id: 1002, reviewer: "Student_7733", date: "2025-11-20", course: "Algorithms", wouldTakeAgain: true, ratings: { difficulty: 4, workload: 4, friendliness: 4 }, grade: "B", comment: "Challenging course but very fair grading. Dr. Ahmad is available during office hours and genuinely wants students to succeed. The course is hard but you learn a lot.", sentiment: "positive", profReply: null },
    { id: 1003, reviewer: "Student_1892", date: "2025-10-12", course: "Programming Fundamentals", wouldTakeAgain: false, ratings: { difficulty: 4, workload: 4, friendliness: 4 }, grade: "C", comment: "The workload is very heavy compared to other courses. I didn't feel adequately supported when struggling. Office hours are helpful but 2 hours per week is not enough for such a dense course.", sentiment: "negative", profReply: null }
  ],
  2: [
    { id: 2001, reviewer: "Student_5578", date: "2025-12-01", course: "Calculus I", wouldTakeAgain: true, ratings: { difficulty: 4, workload: 4, friendliness: 4 }, grade: "B", comment: "Dr. Maha knows her subject extremely well but can be intimidating in class. If you do the work and attend all lectures, you will pass. The exams are very challenging.", sentiment: "neutral", profReply: null },
    { id: 2002, reviewer: "Student_3341", date: "2025-09-18", course: "Linear Algebra", wouldTakeAgain: false, ratings: { difficulty: 5, workload: 5, friendliness: 3 }, grade: "D", comment: "Incredibly difficult course. The material was too advanced for the course level and she doesn't slow down for struggling students. Would not recommend unless you already have a strong math background.", sentiment: "negative", profReply: null }
  ],
  4: [
    { id: 4001, reviewer: "Student_9921", date: "2025-12-15", course: "Network Security", wouldTakeAgain: true, ratings: { difficulty: 3, workload: 4, friendliness: 5 }, grade: "A", comment: "Dr. Rania is the best professor I have had in my entire academic career. She is incredibly passionate about cybersecurity and that energy is infectious. Lectures are engaging, material is relevant, and she genuinely cares about her students.", sentiment: "positive", profReply: "It means so much to hear this! Security is my passion and I love sharing that enthusiasm." },
    { id: 4002, reviewer: "Student_6612", date: "2025-11-30", course: "Ethical Hacking", wouldTakeAgain: true, ratings: { difficulty: 4, workload: 3, friendliness: 5 }, grade: "A", comment: "Ethical Hacking with Dr. Rania was the highlight of my academic year. Hands-on labs, CTF competitions, real penetration testing scenarios — this course prepared me better for the real world than anything else.", sentiment: "positive", profReply: null },
    { id: 4003, reviewer: "Student_2289", date: "2025-10-05", course: "Cryptography", wouldTakeAgain: true, ratings: { difficulty: 3, workload: 3, friendliness: 5 }, grade: "B", comment: "Cryptography was dry in other courses but Dr. Rania made it fascinating. She explains the math behind RSA and elliptic curves in a way that actually makes sense. Excellent professor.", sentiment: "positive", profReply: null }
  ],
  6: [
    { id: 6001, reviewer: "Student_8830", date: "2025-12-10", course: "Machine Learning", wouldTakeAgain: true, ratings: { difficulty: 4, workload: 4, friendliness: 5 }, grade: "A", comment: "Dr. Samia is a gem. She explains ML concepts with incredible clarity and always makes sure everyone is keeping up. The course project was challenging but she guided us every step of the way.", sentiment: "positive", profReply: null },
    { id: 6002, reviewer: "Student_5547", date: "2025-11-15", course: "Deep Learning", wouldTakeAgain: true, ratings: { difficulty: 5, workload: 4, friendliness: 4 }, grade: "B", comment: "Very demanding course with extensive reading and project work, but well worth it. Dr. Samia is clearly at the top of her field and brings guest speakers from industry which adds real value.", sentiment: "positive", profReply: "Thank you! Industry connections are so important in AI — keep building your network." }
  ],
  3: [
    { id: 3001, reviewer: "Student_1177", date: "2025-11-28", course: "Circuit Analysis", wouldTakeAgain: false, ratings: { difficulty: 5, workload: 5, friendliness: 3 }, grade: "C", comment: "Dr. Omar's Circuit Analysis course is brutal. The exams are extremely hard and there is very little support outside class. You need to form study groups to survive.", sentiment: "negative", profReply: null },
    { id: 3002, reviewer: "Student_9944", date: "2025-10-10", course: "Digital Electronics", wouldTakeAgain: true, ratings: { difficulty: 4, workload: 4, friendliness: 4 }, grade: "B", comment: "Hard but fair. Dr. Omar really knows electrical engineering inside out. If you put in the effort, you'll learn a ton. Not the most approachable professor but not bad either.", sentiment: "neutral", profReply: null }
  ],
  5: [
    { id: 5001, reviewer: "Student_7712", date: "2025-12-05", course: "Principles of Management", wouldTakeAgain: true, ratings: { difficulty: 2, workload: 3, friendliness: 5 }, grade: "A", comment: "Dr. Khalid is incredibly warm and approachable. The course is not too demanding and he makes every lecture interesting with case studies and discussions. Great professor for a business elective.", sentiment: "positive", profReply: null }
  ],
  7: [
    { id: 7001, reviewer: "Student_3389", date: "2025-12-01", course: "Software Design Patterns", wouldTakeAgain: true, ratings: { difficulty: 3, workload: 3, friendliness: 4 }, grade: "A", comment: "Dr. Hassan explains design patterns in a very practical way. He uses lots of real code examples which makes the concepts stick. Good balance between theory and practice.", sentiment: "positive", profReply: null }
  ],
  8: [
    { id: 8001, reviewer: "Student_6601", date: "2025-11-25", course: "Operations Research", wouldTakeAgain: true, ratings: { difficulty: 4, workload: 4, friendliness: 5 }, grade: "A", comment: "Dr. Lina is an amazing educator and a genuinely kind person. She goes out of her way to ensure every student understands the material. Her office hours are very productive.", sentiment: "positive", profReply: "Thank you so much! I love seeing students succeed." }
  ]
};

// ── FAQ Data ──────────────────────────────────────────────────
const FAQ_DATA = [
  {
    category: "General",
    items: [
      { q: "What is EduRate?", a: "EduRate is a student-driven platform for rating and reviewing universities and professors in Jordan. Our goal is to help students make informed decisions about their education." },
      { q: "Is EduRate free to use?", a: "Yes! EduRate is completely free for students. You can browse all university and professor profiles, read reviews, and use the comparison tool without any subscription." },
      { q: "Is EduRate affiliated with any university?", a: "No. EduRate is an independent platform and is not affiliated with or endorsed by any university. All ratings and reviews are submitted by students." }
    ]
  },
  {
    category: "Ratings",
    items: [
      { q: "How are the overall ratings calculated?", a: "Overall ratings are calculated as the average of all individual category ratings across all submitted reviews. For universities, 10 categories are averaged. For professors, 3 categories are averaged." },
      { q: "What does the 'Would Take Again' percentage mean?", a: "The 'Would Take Again' percentage shows what proportion of students who reviewed a professor said they would enroll in another course taught by that professor." },
      { q: "How is sentiment analysis determined?", a: "Sentiment analysis is performed on the written review text using natural language processing. Reviews are classified as Positive, Neutral, or Negative based on the overall tone and language used." }
    ]
  },
  {
    category: "Reviews",
    items: [
      { q: "Who can submit reviews?", a: "Only verified students with a valid university email address can submit reviews. This ensures all reviews come from real students with genuine experience." },
      { q: "Can I edit or delete my review after submitting?", a: "Once a review is submitted it cannot be edited. You may contact our support team to request removal of a review if it contains incorrect information." },
      { q: "Are reviews anonymous?", a: "Yes. Your real name is never shown on the platform. Reviews are displayed under a random anonymous student identifier." },
      { q: "How does the abusive language filter work?", a: "Our system automatically scans review text for offensive, discriminatory, or abusive language. Flagged content is held for manual review before being published." }
    ]
  },
  {
    category: "Comparison",
    items: [
      { q: "How many items can I compare at once?", a: "Currently EduRate supports side-by-side comparison of two universities or two professors at a time. Multi-item comparison is planned for a future update." },
      { q: "Can I compare a professor from one university with one from another?", a: "Yes! You can compare any two professors regardless of which university they teach at." }
    ]
  },
  {
    category: "Account",
    items: [
      { q: "How do I create an account?", a: "Click 'Sign Up' and fill in your details. You must use a valid university email address. After submission, check your email for a verification link." },
      { q: "I forgot my password. What should I do?", a: "Click the 'Forgot Password?' link on the login page and enter your email address. You'll receive a password reset link within a few minutes." },
      { q: "How can professors claim their profiles?", a: "Professors can sign up with their institutional email and select the 'Professor' role. Verified profiles display a special badge and unlock additional features like replying to reviews." }
    ]
  }
];

// ── National Averages (computed from UNIVERSITIES) ────────────
function getNationalAverages() {
  const cats = UNIVERSITY_CATEGORIES.map(c => c.key);
  const avgs = {};
  cats.forEach(cat => {
    const sum = UNIVERSITIES.reduce((s, u) => s + u.ratings[cat], 0);
    avgs[cat] = +(sum / UNIVERSITIES.length).toFixed(2);
  });
  return avgs;
}

// ── Helper Functions ──────────────────────────────────────────
function getUniversityById(id) {
  return UNIVERSITIES.find(u => u.id === Number(id)) || null;
}

function getProfessorById(id) {
  return PROFESSORS.find(p => p.id === Number(id)) || null;
}

function getUniversityReviews(uniId) {
  return REVIEWS_UNIVERSITY[Number(uniId)] || [];
}

function getProfessorReviews(profId) {
  return REVIEWS_PROFESSOR[Number(profId)] || [];
}

function getProfessorsByUniversity(uniId) {
  const uni = getUniversityById(uniId);
  if (!uni) return [];
  return uni.professors.map(id => getProfessorById(id)).filter(Boolean);
}

function getTopUniversities(n = 4) {
  return [...UNIVERSITIES].sort((a, b) => b.overallRating - a.overallRating).slice(0, n);
}

function getTopProfessors(n = 4) {
  return [...PROFESSORS].sort((a, b) => b.overallRating - a.overallRating).slice(0, n);
}

function searchUniversities(query) {
  const q = query.toLowerCase();
  return UNIVERSITIES.filter(u =>
    u.name.toLowerCase().includes(q) ||
    u.shortName.toLowerCase().includes(q) ||
    u.city.toLowerCase().includes(q)
  );
}

function searchProfessors(query) {
  const q = query.toLowerCase();
  return PROFESSORS.filter(p =>
    p.name.toLowerCase().includes(q) ||
    p.department.toLowerCase().includes(q) ||
    p.universityName.toLowerCase().includes(q)
  );
}

function getUniqueUniversityDepts() {
  return [...new Set(PROFESSORS.map(p => p.department))].sort();
}

function getStats() {
  return {
    universities: UNIVERSITIES.length,
    professors: PROFESSORS.length,
    reviews: Object.values(REVIEWS_UNIVERSITY).flat().length + Object.values(REVIEWS_PROFESSOR).flat().length,
    students: 1240
  };
}

// ── Abusive Words Filter (simple demo) ───────────────────────
const ABUSIVE_WORDS = ["stupid", "idiot", "hate", "dumb", "ugly", "loser"];
function containsAbusiveLanguage(text) {
  const t = text.toLowerCase();
  return ABUSIVE_WORDS.some(w => t.includes(w));
}

// ── Mock Auth State ───────────────────────────────────────────
const AUTH = {
  get() {
    try { return JSON.parse(sessionStorage.getItem('edurate_user')) || null; }
    catch { return null; }
  },
  set(user) { sessionStorage.setItem('edurate_user', JSON.stringify(user)); },
  clear()   { sessionStorage.removeItem('edurate_user'); },
  isLoggedIn() { return !!this.get(); },
  isStudent()  { return this.get()?.role === 'student'; },
  isProfessor(){ return this.get()?.role === 'professor'; }
};
