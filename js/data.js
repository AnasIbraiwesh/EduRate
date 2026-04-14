/**
 * EduRate — Mock Data
 * Replace fetch() calls with .NET API endpoints when backend is ready.
 */

const UNIVERSITIES = [
  {
    id: 1,
    name: "University of Jordan",
    shortName: "UJ",
    city: "Amman",
    location: "Amman, Jordan",
    established: 1962,
    website: "#",
    description: "The University of Jordan is the oldest and largest public university in Jordan, offering a wide range of undergraduate and graduate programs across multiple colleges.",
    reviewCount: 284,
    overallRating: 4.2,
    ratings: {
      location:      4.3,
      reputation:    4.5,
      safety:        4.1,
      food:          3.4,
      facilities:    4.0,
      clubs:         3.7,
      happiness:     4.2,
      internet:      3.5,
      opportunities: 4.1,
      socialLife:    3.8
    },
    sentiment: { positive: 68, neutral: 22, negative: 10 }
  },
  {
    id: 2,
    name: "Jordan University of Science & Technology",
    shortName: "JUST",
    city: "Irbid",
    location: "Irbid, Jordan",
    established: 1986,
    website: "#",
    description: "JUST is one of Jordan's leading universities in science, technology, engineering, and medicine, with a strong research culture and modern facilities.",
    reviewCount: 196,
    overallRating: 4.5,
    ratings: {
      location:      3.8,
      reputation:    4.7,
      safety:        4.3,
      food:          3.6,
      facilities:    4.5,
      clubs:         4.0,
      happiness:     4.4,
      internet:      4.1,
      opportunities: 4.6,
      socialLife:    4.0
    },
    sentiment: { positive: 75, neutral: 18, negative: 7 }
  },
  {
    id: 3,
    name: "Yarmouk University",
    shortName: "YU",
    city: "Irbid",
    location: "Irbid, Jordan",
    established: 1976,
    website: "#",
    description: "Yarmouk University is a public university located in Irbid, known for its strong humanities, arts, and social sciences programs.",
    reviewCount: 141,
    overallRating: 3.8,
    ratings: {
      location:      4.0,
      reputation:    3.9,
      safety:        4.2,
      food:          3.2,
      facilities:    3.5,
      clubs:         3.8,
      happiness:     3.9,
      internet:      3.0,
      opportunities: 3.6,
      socialLife:    4.1
    },
    sentiment: { positive: 58, neutral: 27, negative: 15 }
  },
  {
    id: 4,
    name: "German Jordanian University",
    shortName: "GJU",
    city: "Amman",
    location: "Amman, Jordan",
    established: 2005,
    website: "#",
    description: "GJU applies the German dual-education model combining academic study with practical training, offering top engineering, business, and applied science programs.",
    reviewCount: 98,
    overallRating: 4.4,
    ratings: {
      location:      4.1,
      reputation:    4.4,
      safety:        4.5,
      food:          3.8,
      facilities:    4.6,
      clubs:         3.9,
      happiness:     4.3,
      internet:      4.4,
      opportunities: 4.7,
      socialLife:    3.7
    },
    sentiment: { positive: 72, neutral: 20, negative: 8 }
  },
  {
    id: 5,
    name: "Hashemite University",
    shortName: "HU",
    city: "Zarqa",
    location: "Zarqa, Jordan",
    established: 1995,
    website: "#",
    description: "Hashemite University is a public institution in Zarqa offering programs in medicine, engineering, information technology, and arts & sciences.",
    reviewCount: 87,
    overallRating: 3.6,
    ratings: {
      location:      3.3,
      reputation:    3.7,
      safety:        4.0,
      food:          3.1,
      facilities:    3.4,
      clubs:         3.2,
      happiness:     3.6,
      internet:      2.9,
      opportunities: 3.5,
      socialLife:    3.4
    },
    sentiment: { positive: 50, neutral: 30, negative: 20 }
  },
  {
    id: 6,
    name: "American University of Madaba",
    shortName: "AUM",
    city: "Madaba",
    location: "Madaba, Jordan",
    established: 2011,
    website: "#",
    description: "AUM is a private liberal arts university offering an American-style education emphasizing critical thinking, interdisciplinary learning, and community engagement.",
    reviewCount: 53,
    overallRating: 4.0,
    ratings: {
      location:      3.5,
      reputation:    4.0,
      safety:        4.4,
      food:          3.9,
      facilities:    4.1,
      clubs:         4.2,
      happiness:     4.3,
      internet:      3.8,
      opportunities: 3.9,
      socialLife:    4.4
    },
    sentiment: { positive: 64, neutral: 25, negative: 11 }
  }
];

const PROFESSORS = [
  {
    id: 1,
    name: "Dr. Ahmad Khalil",
    firstName: "Ahmad",
    lastName: "Khalil",
    department: "Computer Science",
    universityId: 1,
    universityName: "University of Jordan",
    title: "Associate Professor",
    courses: ["CS101 — Intro to Programming", "CS201 — Data Structures", "CS401 — Algorithms"],
    reviewCount: 72,
    overallRating: 4.5,
    wouldTakeAgain: 88,
    ratings: {
      difficulty:    3.8,
      workload:      3.5,
      communication: 4.7,
      friendliness:  4.6,
      clarity:       4.4
    },
    gradeDistribution: { A: 28, B: 35, C: 22, D: 10, F: 5 },
    sentiment: { positive: 80, neutral: 14, negative: 6 }
  },
  {
    id: 2,
    name: "Dr. Lina Mansour",
    firstName: "Lina",
    lastName: "Mansour",
    department: "Business Administration",
    universityId: 1,
    universityName: "University of Jordan",
    title: "Professor",
    courses: ["BA201 — Marketing", "BA301 — Business Strategy", "BA401 — International Business"],
    reviewCount: 58,
    overallRating: 4.2,
    wouldTakeAgain: 80,
    ratings: {
      difficulty:    4.2,
      workload:      4.0,
      communication: 4.3,
      friendliness:  4.2,
      clarity:       4.1
    },
    gradeDistribution: { A: 20, B: 38, C: 28, D: 10, F: 4 },
    sentiment: { positive: 70, neutral: 20, negative: 10 }
  },
  {
    id: 3,
    name: "Dr. Omar Saleh",
    firstName: "Omar",
    lastName: "Saleh",
    department: "Electrical Engineering",
    universityId: 2,
    universityName: "JUST",
    title: "Professor",
    courses: ["EE201 — Circuit Theory", "EE301 — Digital Systems", "EE401 — Embedded Systems"],
    reviewCount: 45,
    overallRating: 3.8,
    wouldTakeAgain: 62,
    ratings: {
      difficulty:    4.6,
      workload:      4.5,
      communication: 3.5,
      friendliness:  3.6,
      clarity:       3.7
    },
    gradeDistribution: { A: 12, B: 28, C: 32, D: 18, F: 10 },
    sentiment: { positive: 48, neutral: 30, negative: 22 }
  },
  {
    id: 4,
    name: "Dr. Sara Nabulsi",
    firstName: "Sara",
    lastName: "Nabulsi",
    department: "Medicine",
    universityId: 2,
    universityName: "JUST",
    title: "Assistant Professor",
    courses: ["MED201 — Anatomy", "MED301 — Physiology", "MED401 — Pathology"],
    reviewCount: 61,
    overallRating: 4.7,
    wouldTakeAgain: 94,
    ratings: {
      difficulty:    3.9,
      workload:      4.1,
      communication: 4.9,
      friendliness:  4.8,
      clarity:       4.7
    },
    gradeDistribution: { A: 35, B: 40, C: 18, D: 5, F: 2 },
    sentiment: { positive: 90, neutral: 7, negative: 3 }
  },
  {
    id: 5,
    name: "Dr. Khalid Hamdan",
    firstName: "Khalid",
    lastName: "Hamdan",
    department: "Mathematics",
    universityId: 3,
    universityName: "Yarmouk University",
    title: "Associate Professor",
    courses: ["MATH101 — Calculus I", "MATH201 — Linear Algebra", "MATH301 — Differential Equations"],
    reviewCount: 39,
    overallRating: 3.5,
    wouldTakeAgain: 55,
    ratings: {
      difficulty:    4.7,
      workload:      4.4,
      communication: 3.2,
      friendliness:  3.3,
      clarity:       3.4
    },
    gradeDistribution: { A: 8, B: 22, C: 34, D: 24, F: 12 },
    sentiment: { positive: 42, neutral: 28, negative: 30 }
  },
  {
    id: 6,
    name: "Dr. Nour Al-Rashid",
    firstName: "Nour",
    lastName: "Al-Rashid",
    department: "Architecture",
    universityId: 4,
    universityName: "GJU",
    title: "Associate Professor",
    courses: ["ARCH201 — Design Studio I", "ARCH301 — Urban Planning", "ARCH401 — Sustainable Design"],
    reviewCount: 34,
    overallRating: 4.6,
    wouldTakeAgain: 91,
    ratings: {
      difficulty:    3.5,
      workload:      3.8,
      communication: 4.8,
      friendliness:  4.7,
      clarity:       4.6
    },
    gradeDistribution: { A: 38, B: 36, C: 18, D: 6, F: 2 },
    sentiment: { positive: 85, neutral: 12, negative: 3 }
  },
  {
    id: 7,
    name: "Dr. Rami Awad",
    firstName: "Rami",
    lastName: "Awad",
    department: "Physics",
    universityId: 3,
    universityName: "Yarmouk University",
    title: "Professor",
    courses: ["PHYS101 — General Physics I", "PHYS201 — Modern Physics", "PHYS301 — Quantum Mechanics"],
    reviewCount: 28,
    overallRating: 4.1,
    wouldTakeAgain: 75,
    ratings: {
      difficulty:    4.2,
      workload:      4.0,
      communication: 4.2,
      friendliness:  4.0,
      clarity:       4.1
    },
    gradeDistribution: { A: 18, B: 32, C: 30, D: 14, F: 6 },
    sentiment: { positive: 65, neutral: 22, negative: 13 }
  },
  {
    id: 8,
    name: "Dr. Maya Barakat",
    firstName: "Maya",
    lastName: "Barakat",
    department: "Information Technology",
    universityId: 1,
    universityName: "University of Jordan",
    title: "Assistant Professor",
    courses: ["IT201 — Web Development", "IT301 — Database Systems", "IT401 — Cloud Computing"],
    reviewCount: 47,
    overallRating: 4.3,
    wouldTakeAgain: 84,
    ratings: {
      difficulty:    3.6,
      workload:      3.4,
      communication: 4.5,
      friendliness:  4.4,
      clarity:       4.3
    },
    gradeDistribution: { A: 30, B: 38, C: 22, D: 7, F: 3 },
    sentiment: { positive: 76, neutral: 17, negative: 7 }
  }
];

const REVIEWS = {
  universities: {
    1: [
      { id: 1, stars: 5, sentiment: "positive", comment: "The University of Jordan has an amazing campus and the faculty are genuinely supportive. I especially loved the variety of student clubs and the vibrant social scene. Career services helped me land an internship before graduation!", date: "2 weeks ago", verified: true, helpful: 24 },
      { id: 2, stars: 4, sentiment: "positive", comment: "Great reputation and the professors in the engineering faculty are highly qualified. Campus facilities are modern and well-maintained. The only downside is the WiFi in some older buildings.", date: "1 month ago", verified: true, helpful: 17 },
      { id: 3, stars: 3, sentiment: "neutral", comment: "Decent university overall. The academic quality is good but the administrative processes can be slow and frustrating at times. Food options on campus are quite limited.", date: "2 months ago", verified: true, helpful: 9 },
      { id: 4, stars: 5, sentiment: "positive", comment: "I studied Business here and the connections I made with faculty and peers were invaluable. The library resources are excellent and the research environment is top-notch.", date: "3 months ago", verified: true, helpful: 31 },
      { id: 5, stars: 2, sentiment: "negative", comment: "Parking is a nightmare and the cafeteria food is subpar. The internet connection is inconsistent which makes online submissions stressful during peak times.", date: "3 months ago", verified: true, helpful: 6 },
      { id: 6, stars: 4, sentiment: "positive", comment: "Strong academic programs, especially in medicine and engineering. The campus is beautiful and well-kept. Would definitely recommend to prospective students.", date: "4 months ago", verified: false, helpful: 13 },
      { id: 7, stars: 3, sentiment: "neutral", comment: "The academic level is high which is good but some professors need to improve their teaching methods. Social life is great though and there are many activities to participate in.", date: "5 months ago", verified: true, helpful: 8 }
    ],
    2: [
      { id: 8, stars: 5, sentiment: "positive", comment: "JUST has world-class facilities for engineering and medicine. The international partnerships opened so many doors for me. Highly competitive admission for a reason!", date: "1 week ago", verified: true, helpful: 19 },
      { id: 9, stars: 5, sentiment: "positive", comment: "The research opportunities here are incredible. I co-authored a paper as an undergrad — that rarely happens elsewhere. Facilities are state of the art.", date: "3 weeks ago", verified: true, helpful: 27 },
      { id: 10, stars: 4, sentiment: "positive", comment: "Excellent university, especially for STEM. Professors are highly qualified and teaching quality is consistently high. Campus is modern and safe.", date: "2 months ago", verified: true, helpful: 14 },
      { id: 11, stars: 3, sentiment: "neutral", comment: "Great university academically. Location in Irbid is a bit far from Amman but the campus life makes up for it. Transportation could be better.", date: "3 months ago", verified: true, helpful: 7 },
      { id: 12, stars: 4, sentiment: "positive", comment: "The med school is one of the best in Jordan. Hands-on clinical training starts early which is a huge advantage for students aiming for competitive residencies.", date: "4 months ago", verified: true, helpful: 22 }
    ],
    3: [
      { id: 13, stars: 4, sentiment: "positive", comment: "Yarmouk has a great humanities and arts program. The campus atmosphere is friendly and the professors in literature are genuinely passionate about their subjects.", date: "2 weeks ago", verified: true, helpful: 11 },
      { id: 14, stars: 3, sentiment: "neutral", comment: "Good university with a long history. Some departments are better than others. IT infrastructure needs significant upgrading — the WiFi is really unreliable.", date: "1 month ago", verified: true, helpful: 8 },
      { id: 15, stars: 2, sentiment: "negative", comment: "The facilities are showing their age and need renovation. Academic quality varies widely by department. However the social life is one of the best I have experienced.", date: "2 months ago", verified: true, helpful: 5 }
    ],
    4: [
      { id: 16, stars: 5, sentiment: "positive", comment: "GJU's dual education model is exceptional. I did my semester abroad in Germany which was life-changing. The engineering faculty are internationally trained and the facilities are top tier.", date: "1 week ago", verified: true, helpful: 28 },
      { id: 17, stars: 5, sentiment: "positive", comment: "Very professional environment. The German system really prepares you for the real world. Industry connections are unmatched — companies actively recruit from GJU.", date: "1 month ago", verified: true, helpful: 21 },
      { id: 18, stars: 4, sentiment: "positive", comment: "Great place to study if you want a rigorous education. The workload is heavy but the quality of learning is worth it. Facilities are modern and well-equipped.", date: "2 months ago", verified: true, helpful: 15 }
    ],
    5: [
      { id: 19, stars: 3, sentiment: "neutral", comment: "Hashemite University is an okay option, especially if you live in Zarqa. Academic programs are decent but campus life could be more engaging.", date: "3 weeks ago", verified: true, helpful: 6 },
      { id: 20, stars: 4, sentiment: "positive", comment: "I studied IT here and found the program to be solid. Professors are approachable and willing to help. The campus is quiet which I personally like for studying.", date: "2 months ago", verified: true, helpful: 9 }
    ],
    6: [
      { id: 21, stars: 4, sentiment: "positive", comment: "AUM has a very unique atmosphere that encourages discussion and critical thinking. Small class sizes mean you actually get to interact with your professors.", date: "2 weeks ago", verified: true, helpful: 12 },
      { id: 22, stars: 5, sentiment: "positive", comment: "The liberal arts approach was exactly what I needed. The campus community is tight-knit and the social life is excellent. Would choose AUM again without hesitation.", date: "1 month ago", verified: true, helpful: 18 }
    ]
  },
  professors: {
    1: [
      { id: 101, stars: 5, sentiment: "positive", course: "CS101", comment: "Dr. Khalil is one of the best CS professors I have had. He explains complex concepts clearly and is always available for office hours. His assignments are challenging but fair.", date: "1 week ago", verified: true, helpful: 18 },
      { id: 102, stars: 5, sentiment: "positive", course: "CS201", comment: "Data Structures with Dr. Khalil was genuinely enjoyable. He uses real-world examples and encourages problem-solving over memorization.", date: "3 weeks ago", verified: true, helpful: 14 },
      { id: 103, stars: 4, sentiment: "positive", course: "CS401", comment: "Very knowledgeable professor. The algorithms course is tough but he scaffolds the content well. Exams are fair if you attend lectures.", date: "1 month ago", verified: true, helpful: 10 },
      { id: 104, stars: 3, sentiment: "neutral", course: "CS101", comment: "Good professor overall but sometimes goes too fast through material. His slides could be better organized. Office hours are helpful.", date: "2 months ago", verified: true, helpful: 5 },
      { id: 105, stars: 5, sentiment: "positive", course: "CS201", comment: "Best professor in the CS department. Makes you fall in love with programming. The course is hard work but you learn so much.", date: "3 months ago", verified: true, helpful: 22 }
    ],
    2: [
      { id: 106, stars: 4, sentiment: "positive", course: "BA201", comment: "Dr. Mansour knows marketing inside-out. Brings industry case studies to every lecture. Exams are based on understanding not memorization.", date: "2 weeks ago", verified: true, helpful: 12 },
      { id: 107, stars: 5, sentiment: "positive", course: "BA301", comment: "Business Strategy was the most practical course I took at university. Dr. Mansour's industry experience really shines through.", date: "1 month ago", verified: true, helpful: 16 },
      { id: 108, stars: 3, sentiment: "neutral", course: "BA201", comment: "Average course, the professor knows her stuff but the class is very large and it is hard to get individual attention.", date: "2 months ago", verified: true, helpful: 4 }
    ],
    3: [
      { id: 109, stars: 3, sentiment: "neutral", course: "EE201", comment: "Very knowledgeable but not the best communicator. You need to read the textbook alongside lectures to really understand the material.", date: "1 month ago", verified: true, helpful: 8 },
      { id: 110, stars: 2, sentiment: "negative", course: "EE301", comment: "Dr. Saleh is clearly an expert but his teaching style is not accessible for beginners. Assignments are extremely difficult without enough guidance.", date: "2 months ago", verified: true, helpful: 11 },
      { id: 111, stars: 4, sentiment: "positive", course: "EE401", comment: "Once you get used to his style, the course becomes manageable. Very demanding but you learn a lot if you put in the effort.", date: "3 months ago", verified: true, helpful: 7 }
    ],
    4: [
      { id: 112, stars: 5, sentiment: "positive", course: "MED201", comment: "Dr. Nabulsi is an extraordinary professor. Her anatomy lectures are the clearest I have attended. She genuinely cares about student understanding.", date: "1 week ago", verified: true, helpful: 31 },
      { id: 113, stars: 5, sentiment: "positive", course: "MED301", comment: "Makes physiology easy to understand and remember. Incredible teacher who goes beyond the curriculum to give clinical context.", date: "2 weeks ago", verified: true, helpful: 28 },
      { id: 114, stars: 5, sentiment: "positive", course: "MED201", comment: "Best professor in the medical college hands down. Her passion for the subject is infectious and it motivates you to study harder.", date: "1 month ago", verified: true, helpful: 24 }
    ],
    5: [
      { id: 115, stars: 2, sentiment: "negative", course: "MATH101", comment: "Very difficult professor. The course is extremely hard and he does not make effort to help struggling students. Exams are brutal.", date: "1 month ago", verified: true, helpful: 15 },
      { id: 116, stars: 3, sentiment: "neutral", course: "MATH201", comment: "Knows math very well but presentation is lacking. Prefers proofs over intuition which makes it hard for applied students.", date: "2 months ago", verified: true, helpful: 9 },
      { id: 117, stars: 4, sentiment: "positive", course: "MATH301", comment: "Challenging course but if you come prepared and attend every lecture you will be fine. His problem sets really develop your thinking.", date: "3 months ago", verified: true, helpful: 6 }
    ],
    6: [
      { id: 118, stars: 5, sentiment: "positive", course: "ARCH201", comment: "Dr. Al-Rashid is an inspiring mentor. Her design critiques are insightful and push you to think differently. Best professor at GJU.", date: "2 weeks ago", verified: true, helpful: 20 },
      { id: 119, stars: 5, sentiment: "positive", course: "ARCH301", comment: "Urban Planning with Dr. Al-Rashid completely changed how I see cities. Her real-world project approach makes the course unforgettable.", date: "1 month ago", verified: true, helpful: 17 }
    ],
    7: [
      { id: 120, stars: 4, sentiment: "positive", course: "PHYS101", comment: "Dr. Awad is a solid physics professor. He explains derivations step by step and makes time for questions. Lab sessions are well organized.", date: "3 weeks ago", verified: true, helpful: 9 },
      { id: 121, stars: 4, sentiment: "positive", course: "PHYS201", comment: "Modern Physics was genuinely fascinating. Dr. Awad's enthusiasm for quantum topics is contagious.", date: "2 months ago", verified: true, helpful: 7 }
    ],
    8: [
      { id: 122, stars: 5, sentiment: "positive", course: "IT201", comment: "Dr. Barakat teaches web development with hands-on projects from day one. I built a complete full-stack app by the end of the course!", date: "1 week ago", verified: true, helpful: 19 },
      { id: 123, stars: 4, sentiment: "positive", course: "IT301", comment: "Database course is well structured and practical. Dr. Barakat is always willing to help and her feedback on projects is constructive.", date: "2 weeks ago", verified: true, helpful: 14 },
      { id: 124, stars: 5, sentiment: "positive", course: "IT401", comment: "Excellent professor. Cloud Computing course is very relevant to the industry. She brought in a guest speaker from AWS which was amazing.", date: "1 month ago", verified: true, helpful: 21 }
    ]
  }
};

const FAQ_DATA = [
  {
    id: 1,
    category: "general",
    question: "What is EduRate?",
    answer: "EduRate is a platform where university students in Jordan can rate and review their universities and professors. Our mission is to help prospective students make informed decisions about their academic future based on honest, verified peer reviews."
  },
  {
    id: 2,
    category: "general",
    question: "Is EduRate free to use?",
    answer: "Yes, EduRate is completely free for students. You can browse all university and professor ratings without creating an account. To submit a review or access personalized features, you will need a free account."
  },
  {
    id: 3,
    category: "general",
    question: "Which universities are listed on EduRate?",
    answer: "EduRate currently covers all major public and private universities in Jordan. We are continuously expanding our database. If your university is not listed, you can request it by contacting us through the feedback form."
  },
  {
    id: 4,
    category: "reviews",
    question: "How are ratings calculated?",
    answer: "Ratings are calculated as the weighted average of all verified student submissions in each category. We use a Bayesian averaging method that accounts for the number of reviews, ensuring universities and professors with more reviews are rated more reliably. New entities start with a neutral baseline that adjusts as reviews come in."
  },
  {
    id: 5,
    category: "reviews",
    question: "Are reviews really anonymous?",
    answer: "Yes. Your personal identity is never shown on any review. We only display whether the reviewer is a verified student. Your email and account details are never visible to other users, to the university, or to professors. However, we retain this information internally to prevent abuse."
  },
  {
    id: 6,
    category: "reviews",
    question: "How do I submit a review?",
    answer: "Navigate to the university or professor profile you want to review and click 'Write a Review'. You will need to be logged in with a verified student account. Fill in the category ratings, write your comment (minimum 20 characters), and submit. Your review will be published after a quick automated moderation check."
  },
  {
    id: 7,
    category: "reviews",
    question: "Can I edit or delete my review after submitting?",
    answer: "Yes, you can edit or delete your own reviews at any time from your account profile page. Changes to ratings will immediately update the overall averages for the university or professor."
  },
  {
    id: 8,
    category: "reviews",
    question: "What makes a review verified?",
    answer: "A review is marked as verified when the reviewer's account was created using an official university email address (e.g., @ju.edu.jo, @just.edu.jo). This ensures that reviews come from real current or former students."
  },
  {
    id: 9,
    category: "comparing",
    question: "How do I compare two universities?",
    answer: "Go to any university profile and click the 'Compare' button. This pre-fills that university as Entity A on the comparison page. Then select a second university from the dropdown to see a side-by-side comparison of all 10 rating categories, a radar chart overlay, and overall score comparison."
  },
  {
    id: 10,
    category: "comparing",
    question: "Can I compare professors too?",
    answer: "Yes! The same comparison feature exists for professors. Navigate to a professor's profile, click 'Compare', and then select a second professor. The comparison shows all five rating categories side-by-side."
  },
  {
    id: 11,
    category: "comparing",
    question: "Can I share a comparison with someone?",
    answer: "Yes. On the comparison page, click the 'Share Comparison' button to copy a direct link to the comparison. Anyone with the link can view the same comparison without needing an account."
  },
  {
    id: 12,
    category: "account",
    question: "How do I create an account?",
    answer: "Click 'Sign Up' in the navigation bar. Enter your university email address, create a password, and select your role (Student or Professor). We will send a verification email to confirm your address. Once verified, your account is active."
  },
  {
    id: 13,
    category: "account",
    question: "I forgot my password. What do I do?",
    answer: "Click 'Log In' and then 'Forgot Password?'. Enter your registered email address and we will send you a secure password reset link valid for 24 hours."
  },
  {
    id: 14,
    category: "account",
    question: "Can professors create accounts?",
    answer: "Yes. Professors can create accounts using their institutional email. Professor accounts can view their own ratings and reviews, respond to reviews, and flag inaccurate information. Professors cannot edit or delete student reviews."
  },
  {
    id: 15,
    category: "privacy",
    question: "How does EduRate protect my privacy?",
    answer: "We take privacy seriously. Your personal information is never displayed publicly. Reviews are anonymous. We do not sell your data to third parties. All data is stored securely and access is strictly controlled. Please read our full Privacy Policy for details."
  },
  {
    id: 16,
    category: "privacy",
    question: "Can universities or professors see who wrote a review about them?",
    answer: "No. The reviewer's identity is completely anonymous. Universities and professors can only see the review content, the overall rating, and a verified/unverified badge. Personal details are never disclosed."
  },
  {
    id: 17,
    category: "privacy",
    question: "How do I report an inappropriate review?",
    answer: "Each review has a 'Report' option accessible through the three-dot menu. Reports are reviewed by our moderation team within 48 hours. Reviews that violate our community guidelines (hate speech, personal attacks, spam) will be removed."
  }
];

// ── Helper functions ──────────────────────────────────────────

function getUniversityById(id) {
  return UNIVERSITIES.find(u => u.id === parseInt(id)) || null;
}

function getProfessorById(id) {
  return PROFESSORS.find(p => p.id === parseInt(id)) || null;
}

function getUniversityReviews(uniId) {
  return REVIEWS.universities[uniId] || [];
}

function getProfessorReviews(profId) {
  return REVIEWS.professors[profId] || [];
}

function searchAll(query) {
  const q = query.toLowerCase();
  const unis = UNIVERSITIES
    .filter(u => u.name.toLowerCase().includes(q) || u.city.toLowerCase().includes(q))
    .slice(0, 3)
    .map(u => ({ type: 'university', id: u.id, name: u.name, sub: u.city }));
  const profs = PROFESSORS
    .filter(p => p.name.toLowerCase().includes(q) || p.department.toLowerCase().includes(q))
    .slice(0, 2)
    .map(p => ({ type: 'professor', id: p.id, name: p.name, sub: p.department }));
  return [...unis, ...profs];
}

function starsHTML(rating, size = 16) {
  const full  = Math.floor(rating);
  const half  = rating % 1 >= 0.5;
  const empty = 5 - full - (half ? 1 : 0);
  let html = '<span class="star-display">';
  for (let i = 0; i < full;  i++) html += `<svg class="star" width="${size}" height="${size}" viewBox="0 0 24 24" fill="currentColor"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"/></svg>`;
  if (half)                         html += `<svg class="star" width="${size}" height="${size}" viewBox="0 0 24 24" fill="currentColor" style="clip-path:inset(0 50% 0 0)"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"/></svg>`;
  for (let i = 0; i < empty; i++) html += `<svg class="star empty" width="${size}" height="${size}" viewBox="0 0 24 24" fill="currentColor"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"/></svg>`;
  html += '</span>';
  return html;
}

function sentimentBadgeHTML(sentiment) {
  const map = {
    positive: { label: 'Positive', emoji: '😊' },
    neutral:  { label: 'Neutral',  emoji: '😐' },
    negative: { label: 'Negative', emoji: '😞' }
  };
  const s = map[sentiment] || map.neutral;
  return `<span class="sentiment-badge ${sentiment}">${s.emoji} ${s.label}</span>`;
}

function ratingBarColor(score) {
  if (score >= 4.0) return 'high';
  if (score >= 3.0) return 'medium';
  return 'low';
}

function formatDate(str) { return str; }
