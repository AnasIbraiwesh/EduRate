using eduRateSystem.Data;
using eduRateSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace eduRateSystem.Seed
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var db = services.GetRequiredService<ApplicationDbContext>();

            if (await db.Universities.AnyAsync(u => !u.IsDeleted)) return;

            // ── Universities ────────────────────────────────────────────────────────────

            var universities = new List<University>
            {
                new University
                {
                    Name        = "The University of Jordan",
                    Location    = "Amman, Jordan",
                    Description = "Jordan's oldest and largest public university, established by Royal Decree in 1962. Offers 302+ programs across 26 schools spanning medicine, engineering, humanities, and sciences.",
                    WebsiteUrl  = "https://ju.edu.jo",
                    Ranking     = 2,
                    BudgetLevel = 5,
                    Majors      = "Engineering, Medicine, Computer Science, Business, Pharmacy, Law, Science, Arts, Languages",
                    Levels      = "Bachelor, Master, PhD",
                    ImageUrl    = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/36/University_of_Jordan_Logo.svg/250px-University_of_Jordan_Logo.svg.png",
                    IsDeleted   = false,
                    CreatedAt   = DateTime.UtcNow
                },
                new University
                {
                    Name        = "Jordan University of Science and Technology",
                    Location    = "Irbid, Jordan",
                    Description = "A leading public technological university in northern Jordan, home to King Abdullah University Hospital. Comprises 12 faculties with a strong focus on science, engineering, and health.",
                    WebsiteUrl  = "https://www.just.edu.jo",
                    Ranking     = 4,
                    BudgetLevel = 5,
                    Majors      = "Medicine, Engineering, Pharmacy, Computer Science, Dentistry, Applied Medical Sciences, Nursing",
                    Levels      = "Bachelor, Master, PhD",
                    ImageUrl    = "https://upload.wikimedia.org/wikipedia/en/thumb/a/ab/JUST_logo2.png/250px-JUST_logo2.png",
                    IsDeleted   = false,
                    CreatedAt   = DateTime.UtcNow
                },
                new University
                {
                    Name        = "Princess Sumaya University for Technology",
                    Location    = "Amman, Jordan",
                    Description = "A non-profit private university specializing in IT, communications, and electronics, owned by the Royal Scientific Society and inaugurated by King Hussein I in 1991.",
                    WebsiteUrl  = "https://www.psut.edu.jo",
                    Ranking     = 3,
                    BudgetLevel = 4,
                    Majors      = "Computer Science, Software Engineering, Cybersecurity, Electronics Engineering, Business Technology",
                    Levels      = "Bachelor, Master",
                    ImageUrl    = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/11/PSUT_Logo.png/250px-PSUT_Logo.png",
                    IsDeleted   = false,
                    CreatedAt   = DateTime.UtcNow
                },
                new University
                {
                    Name        = "Yarmouk University",
                    Location    = "Irbid, Jordan",
                    Description = "A major public university in northern Jordan offering programs across 15 faculties, with 52 bachelor's, 64 master's, and 18 PhD programs in science, arts, and engineering.",
                    WebsiteUrl  = "https://www.yu.edu.jo",
                    Ranking     = 5,
                    BudgetLevel = 4,
                    Majors      = "Engineering Technology, IT, Computer Science, Science, Arts, Business, Mass Communication, Archaeology",
                    Levels      = "Bachelor, Master, PhD",
                    ImageUrl    = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ae/Yarmouk_University_logo.png/250px-Yarmouk_University_logo.png",
                    IsDeleted   = false,
                    CreatedAt   = DateTime.UtcNow
                },
                new University
                {
                    Name        = "German Jordanian University",
                    Location    = "Amman, Jordan",
                    Description = "Founded through a Jordanian-German bilateral agreement in 2005, GJU offers an applied-sciences model combining Jordan-based study with mandatory internships in Germany across 10 schools.",
                    WebsiteUrl  = "https://www.gju.edu.jo",
                    Ranking     = 6,
                    BudgetLevel = 4,
                    Majors      = "Applied Technical Sciences, Electrical Engineering, Mechanical Engineering, Biomedical Engineering, Management and Logistics, Architecture",
                    Levels      = "Bachelor, Master",
                    ImageUrl    = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/73/GJU_logo.svg/250px-GJU_logo.svg.png",
                    IsDeleted   = false,
                    CreatedAt   = DateTime.UtcNow
                },
                new University
                {
                    Name        = "American University of Madaba",
                    Location    = "Madaba, Jordan",
                    Description = "A not-for-profit university founded by the Latin Patriarchate of Jerusalem, offering an American-style liberal arts education across 7 faculties with 19 bachelor's and 2 graduate programs.",
                    WebsiteUrl  = "https://www.aum.edu.jo",
                    Ranking     = 7,
                    BudgetLevel = 3,
                    Majors      = "Engineering, Business, Information Technology, Architecture, Health Sciences, Science, Languages",
                    Levels      = "Bachelor, Master",
                    ImageUrl    = "https://upload.wikimedia.org/wikipedia/en/f/f9/AUMadaba.png",
                    IsDeleted   = false,
                    CreatedAt   = DateTime.UtcNow
                },
                new University
                {
                    Name        = "Applied Science Private University",
                    Location    = "Amman, Jordan",
                    Description = "The largest private university in Jordan by campus area, offering 59 specializations across 12 faculties to over 5,900 students from 50+ countries.",
                    WebsiteUrl  = "http://www.asu.edu.jo",
                    Ranking     = 8,
                    BudgetLevel = 3,
                    Majors      = "Pharmacy, Engineering, Information Technology, Business, Law, Nursing, Art and Design",
                    Levels      = "Bachelor, Master",
                    ImageUrl    = null,
                    IsDeleted   = false,
                    CreatedAt   = DateTime.UtcNow
                },
                new University
                {
                    Name        = "Al-Ahliyya Amman University",
                    Location    = "Amman, Jordan",
                    Description = "Jordan's first private university, established in 1990. Houses 12 colleges offering undergraduate and postgraduate programs in law, engineering, business, IT, pharmacy, and nursing.",
                    WebsiteUrl  = "https://www.ammanu.edu.jo",
                    Ranking     = 9,
                    BudgetLevel = 3,
                    Majors      = "Business, Information Technology, Engineering, Law, Pharmacy, Nursing, Medical Engineering",
                    Levels      = "Bachelor, Master",
                    ImageUrl    = "https://www.ammanu.edu.jo/media/1bgdv5he/aau-logo.png",
                    IsDeleted   = false,
                    CreatedAt   = DateTime.UtcNow
                },
                new University
                {
                    Name        = "Al-Balqa Applied University",
                    Location    = "Salt, Jordan",
                    Description = "A government-supported public university founded by Royal Decree in 1996, distinguished by applied engineering and technical education with 22 colleges across 13 campus sites throughout Jordan.",
                    WebsiteUrl  = "https://www.bau.edu.jo",
                    Ranking     = 10,
                    BudgetLevel = 3,
                    Majors      = "Engineering Technology, Computer Sciences, Medicine, Architecture, Agriculture, Business, Nursing",
                    Levels      = "Bachelor, Master, PhD",
                    ImageUrl    = "https://www.bau.edu.jo/content/Logo/baulogoar.png",
                    IsDeleted   = false,
                    CreatedAt   = DateTime.UtcNow
                },
                new University
                {
                    Name        = "Al Hussein Technical University",
                    Location    = "Amman, Jordan",
                    Description = "A pioneering applied-sciences institution founded by the Crown Prince Foundation in 2016, achieving a consistent 100% graduate employment rate through STEM programs aligned with industry needs.",
                    WebsiteUrl  = "https://htu.edu.jo",
                    Ranking     = 1,
                    BudgetLevel = 4,
                    Majors      = "Electrical Engineering, Mechanical Engineering, Data Science and AI, Cybersecurity, Civil Engineering, Energy Engineering, Mechatronics",
                    Levels      = "Bachelor",
                    ImageUrl    = "https://www.htu.edu.jo/_next/static/media/HTU%20Logo.9bbd05d3.svg",
                    IsDeleted   = false,
                    CreatedAt   = DateTime.UtcNow
                }
            };

            db.Universities.AddRange(universities);
            await db.SaveChangesAsync();

            // IDs are populated by EF Core after save
            var uj   = universities[0];
            var just = universities[1];
            var psut = universities[2];
            var yu   = universities[3];
            var gju  = universities[4];
            var aum  = universities[5];
            var asu  = universities[6];
            var aau  = universities[7];
            var bau  = universities[8];
            var htu  = universities[9];

            // ── Professors ──────────────────────────────────────────────────────────────

            var professors = new List<Professor>
            {
                // ── The University of Jordan ──────────────────────────────────────────
                new Professor
                {
                    FullName      = "Shaher Momani",
                    UniversityId  = uj.UniversityId,
                    Department    = "Mathematics",
                    Specialization = "Fractional calculus, differential equations, mathematical modeling",
                    CoursesJson   = """["Real Analysis","Differential Equations","Mathematical Modeling","Numerical Analysis"]""",
                    TeachingStyle = "Theoretical",
                    Rating        = 4.8,
                    ImageUrl      = "https://upload.wikimedia.org/wikipedia/commons/8/8a/%D8%B4%D8%A7%D9%87%D8%B1_%D8%A7%D9%84%D9%85%D9%88%D9%85%D9%86%D9%8A.jpg",
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Mohammad S. Obaidat",
                    UniversityId  = uj.UniversityId,
                    Department    = "Computer Science",
                    Specialization = "Cybersecurity, IoT, distributed systems, cloud security",
                    CoursesJson   = """["Cybersecurity","Internet of Things","Distributed Systems","Cloud Computing Security"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.7,
                    ImageUrl      = "https://upload.wikimedia.org/wikipedia/commons/b/bd/Mohammad_S._Obaidat.jpg",
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Mohammed Haj-Ahmed",
                    UniversityId  = uj.UniversityId,
                    Department    = "Electrical Engineering",
                    Specialization = "Power systems, electrical machines, circuit theory",
                    CoursesJson   = """["Electric Power Systems","Electrical Machines","Circuit Theory","Power Electronics"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.9,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Areej Abuhammad",
                    UniversityId  = uj.UniversityId,
                    Department    = "Pharmaceutical Sciences",
                    Specialization = "Drug design, pharmaceutical sciences, medicinal chemistry",
                    CoursesJson   = """["Drug Design","Medicinal Chemistry","Pharmacology","Pharmaceutical Biochemistry"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.2,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Wassel Al-Bodour",
                    UniversityId  = uj.UniversityId,
                    Department    = "Civil Engineering",
                    Specialization = "Geotechnical engineering, pavement materials, soil mechanics",
                    CoursesJson   = """["Soil Mechanics","Foundation Engineering","Pavement Design","Geotechnical Engineering"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.8,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Anas Aloudat",
                    UniversityId  = uj.UniversityId,
                    Department    = "Management Information Systems",
                    Specialization = "Mobile computing, location-based services, information systems",
                    CoursesJson   = """["Management Information Systems","Mobile Computing","E-Business","Location-Based Services"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.0,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Marwan Jarrah",
                    UniversityId  = uj.UniversityId,
                    Department    = "English Language and Literature",
                    Specialization = "Theoretical linguistics, English literature, syntax",
                    CoursesJson   = """["Introduction to Linguistics","Syntactic Theory","English Literature","Academic Writing"]""",
                    TeachingStyle = "Theoretical",
                    Rating        = 3.7,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },

                // ── Jordan University of Science and Technology ───────────────────
                new Professor
                {
                    FullName      = "Rami Haddad",
                    UniversityId  = just.UniversityId,
                    Department    = "Civil Engineering",
                    Specialization = "Structural engineering, concrete materials, construction",
                    CoursesJson   = """["Structural Analysis","Reinforced Concrete Design","Construction Materials","Structural Dynamics"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.3,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Mohamed Barbarawi",
                    UniversityId  = just.UniversityId,
                    Department    = "Neurosurgery",
                    Specialization = "Brain tumors, spinal surgery, neurosurgical techniques",
                    CoursesJson   = """["Neurosurgery","Neuroanatomy","Brain Tumor Management","Spinal Surgery"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.5,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Fadia Mayyas",
                    UniversityId  = just.UniversityId,
                    Department    = "Clinical Pharmacy",
                    Specialization = "Clinical pharmacy, pharmacology, drug therapy management",
                    CoursesJson   = """["Clinical Pharmacology","Drug Therapy Management","Pharmacokinetics","Patient Counseling"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.1,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Mohammad Alyahya",
                    UniversityId  = just.UniversityId,
                    Department    = "Health Management and Policy",
                    Specialization = "Health services management, public health, healthcare systems",
                    CoursesJson   = """["Health Systems Management","Public Health","Healthcare Policy","Epidemiology"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.9,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "M-Ali Al-Akhras",
                    UniversityId  = just.UniversityId,
                    Department    = "Physics",
                    Specialization = "Biomedical physics, radiation physics, medical imaging",
                    CoursesJson   = """["Radiation Physics","Biomedical Physics","Medical Imaging","Nuclear Medicine"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.6,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Hani Abu Qdais",
                    UniversityId  = just.UniversityId,
                    Department    = "Environmental Engineering",
                    Specialization = "Waste management, environmental engineering, solid waste systems",
                    CoursesJson   = """["Environmental Engineering","Solid Waste Management","Wastewater Treatment","Environmental Impact Assessment"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.4,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Omar Qasaimeh",
                    UniversityId  = just.UniversityId,
                    Department    = "Electrical Engineering",
                    Specialization = "Semiconductors, optoelectronics, photonic devices",
                    CoursesJson   = """["Semiconductor Devices","Optoelectronics","Photonics","Electronic Circuits"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.2,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },

                // ── Princess Sumaya University for Technology ─────────────────────
                new Professor
                {
                    FullName      = "Mohammad Azzeh",
                    UniversityId  = psut.UniversityId,
                    Department    = "Data Science",
                    Specialization = "Software cost estimation, machine learning, data science",
                    CoursesJson   = """["Machine Learning","Data Science","Software Engineering","Software Cost Estimation"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.6,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Mouhammd Alkasassbeh",
                    UniversityId  = psut.UniversityId,
                    Department    = "Computer Science",
                    Specialization = "Network traffic analysis, network security, machine learning in networking",
                    CoursesJson   = """["Network Security","Machine Learning","Intrusion Detection Systems","Computer Networks"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.0,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Saleh Abu-Soud",
                    UniversityId  = psut.UniversityId,
                    Department    = "Computing Sciences",
                    Specialization = "Artificial intelligence, fuzzy logic, machine learning theory",
                    CoursesJson   = """["Artificial Intelligence","Fuzzy Logic","Knowledge Representation","Machine Learning"]""",
                    TeachingStyle = "Theoretical",
                    Rating        = 4.3,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Daoud Daoud",
                    UniversityId  = psut.UniversityId,
                    Department    = "Computer Science",
                    Specialization = "Algorithms, software engineering, computational complexity",
                    CoursesJson   = """["Algorithms","Data Structures","Software Engineering","Computational Complexity"]""",
                    TeachingStyle = "Theoretical",
                    Rating        = 3.5,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Osama Al-Haj Hassan",
                    UniversityId  = psut.UniversityId,
                    Department    = "Computing Sciences",
                    Specialization = "Embedded systems, digital design, microcontrollers",
                    CoursesJson   = """["Embedded Systems","Digital Design","Microcontrollers","Computer Architecture"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.8,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Mohammad Alnabhan",
                    UniversityId  = psut.UniversityId,
                    Department    = "Computing Sciences",
                    Specialization = "Information systems, software engineering, database systems",
                    CoursesJson   = """["Information Systems","Software Engineering","Database Management","Systems Analysis"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.1,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },

                // ── Yarmouk University ────────────────────────────────────────────
                new Professor
                {
                    FullName      = "Mostafa Hayajneh",
                    UniversityId  = yu.UniversityId,
                    Department    = "Mathematics",
                    Specialization = "Pure mathematics, mathematical analysis, topology",
                    CoursesJson   = """["Real Analysis","Abstract Algebra","Topology","Complex Analysis"]""",
                    TeachingStyle = "Theoretical",
                    Rating        = 4.2,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Yousef Bader",
                    UniversityId  = yu.UniversityId,
                    Department    = "English Language and Literature",
                    Specialization = "English literature, linguistics, language teaching methodology",
                    CoursesJson   = """["English Literature","Applied Linguistics","Language Teaching Methods","Academic Writing"]""",
                    TeachingStyle = "Theoretical",
                    Rating        = 4.0,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Mahmoud Al-Qudah",
                    UniversityId  = yu.UniversityId,
                    Department    = "Chemistry",
                    Specialization = "Analytical chemistry, chemical synthesis, spectroscopy",
                    CoursesJson   = """["Analytical Chemistry","Organic Chemistry","Spectroscopic Methods","Chemical Synthesis"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.9,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Mohammed Habes",
                    UniversityId  = yu.UniversityId,
                    Department    = "Mass Communication",
                    Specialization = "Social media, digital journalism, mass communication theory",
                    CoursesJson   = """["Mass Communication Theory","Digital Journalism","Social Media Analytics","Media Research Methods"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.7,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Hussein Alzoubi",
                    UniversityId  = yu.UniversityId,
                    Department    = "Computer Engineering",
                    Specialization = "Computer networks, distributed computing, network protocols",
                    CoursesJson   = """["Computer Networks","Distributed Computing","Network Protocols","Operating Systems"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.5,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Wassef Sekhaneh",
                    UniversityId  = yu.UniversityId,
                    Department    = "Conservation of Cultural Resources",
                    Specialization = "Archaeology, cultural heritage conservation, historical sites",
                    CoursesJson   = """["Archaeological Theory","Cultural Heritage Conservation","Field Archaeology","Heritage Management"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.4,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Sahem Nawafleh",
                    UniversityId  = yu.UniversityId,
                    Department    = "Public Administration",
                    Specialization = "Public administration, governance, public policy",
                    CoursesJson   = """["Public Administration","Governance and Policy","Public Sector Management","Administrative Law"]""",
                    TeachingStyle = "Theoretical",
                    Rating        = 2.9,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },

                // ── German Jordanian University ───────────────────────────────────
                new Professor
                {
                    FullName      = "Raed Mesleh",
                    UniversityId  = gju.UniversityId,
                    Department    = "Electrical Engineering",
                    Specialization = "Wireless communications, MIMO systems, spatial modulation",
                    CoursesJson   = """["Wireless Communications","MIMO Systems","Digital Communications","Signal Processing"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.9,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Ziyad Masoud",
                    UniversityId  = gju.UniversityId,
                    Department    = "Mechanical Engineering",
                    Specialization = "Vibrations, control systems, mechanical dynamics",
                    CoursesJson   = """["Control Systems","Mechanical Vibrations","Dynamics","Applied Mechanics"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.3,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Osamah Badarneh",
                    UniversityId  = gju.UniversityId,
                    Department    = "Communications Engineering",
                    Specialization = "Wireless networks, signal processing, fading channels",
                    CoursesJson   = """["Communications Engineering","Wireless Networks","Signal Processing","Information Theory"]""",
                    TeachingStyle = "Theoretical",
                    Rating        = 4.1,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Nizar Abu-Jaber",
                    UniversityId  = gju.UniversityId,
                    Department    = "Civil and Environmental Engineering",
                    Specialization = "Geology, water resources, environmental engineering",
                    CoursesJson   = """["Hydrogeology","Water Resources Engineering","Environmental Engineering","Geology"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.7,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Samer Al-Gharabli",
                    UniversityId  = gju.UniversityId,
                    Department    = "Pharmaceutical and Chemical Engineering",
                    Specialization = "Chemical engineering, membrane technology, separation processes",
                    CoursesJson   = """["Chemical Engineering","Membrane Technology","Separation Processes","Transport Phenomena"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.9,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Eyad Hamad",
                    UniversityId  = gju.UniversityId,
                    Department    = "Biomedical Engineering",
                    Specialization = "Biomedical engineering, biosensors, medical devices",
                    CoursesJson   = """["Biomedical Engineering","Biosensors","Medical Devices","Bioelectronics"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.2,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Abdelrahman Zuraik",
                    UniversityId  = gju.UniversityId,
                    Department    = "Management Sciences",
                    Specialization = "Innovation leadership, management, entrepreneurship",
                    CoursesJson   = """["Innovation Management","Entrepreneurship","Business Strategy","Leadership"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.6,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },

                // ── American University of Madaba ─────────────────────────────────
                new Professor
                {
                    FullName      = "Wajdy Awaida",
                    UniversityId  = aum.UniversityId,
                    Department    = "Biotechnology",
                    Specialization = "Molecular biology, biotechnology, biochemistry",
                    CoursesJson   = """["Molecular Biology","Biotechnology","Biochemistry","Cell Biology"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.0,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Wesam Ammari",
                    UniversityId  = aum.UniversityId,
                    Department    = "Health Sciences",
                    Specialization = "Clinical pharmacy, pharmaceutical sciences, patient care",
                    CoursesJson   = """["Clinical Pharmacy","Pharmaceutical Sciences","Drug Therapy","Patient Counseling"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.8,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Naif Haddad",
                    UniversityId  = aum.UniversityId,
                    Department    = "Architecture",
                    Specialization = "Architecture, urban planning, architectural design",
                    CoursesJson   = """["Architectural Design","Urban Planning","History of Architecture","Building Technology"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.5,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Wafa Alkhadra",
                    UniversityId  = aum.UniversityId,
                    Department    = "English Language and Literature",
                    Specialization = "English literature, linguistics, translation studies",
                    CoursesJson   = """["English Literature","Translation Studies","Applied Linguistics","Academic English"]""",
                    TeachingStyle = "Theoretical",
                    Rating        = 4.1,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Mohammad Alawamleh",
                    UniversityId  = aum.UniversityId,
                    Department    = "Business",
                    Specialization = "Industrial engineering, business management, operations research",
                    CoursesJson   = """["Operations Management","Business Statistics","Industrial Engineering","Supply Chain Management"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.9,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Islam Hamad",
                    UniversityId  = aum.UniversityId,
                    Department    = "Pharmacy",
                    Specialization = "Drug delivery, pharmaceutical sciences, formulation",
                    CoursesJson   = """["Drug Delivery Systems","Pharmaceutical Formulation","Biopharmaceutics","Pharmacokinetics"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.7,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Jamal Shamieh",
                    UniversityId  = aum.UniversityId,
                    Department    = "Risk Management",
                    Specialization = "Risk management, finance, business analytics",
                    CoursesJson   = """["Risk Management","Financial Analysis","Business Analytics","Corporate Finance"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.4,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },

                // ── Applied Science Private University ────────────────────────────
                new Professor
                {
                    FullName      = "Mwaffaq Abu Alhija",
                    UniversityId  = asu.UniversityId,
                    Department    = "Cybersecurity and Cloud Computing",
                    Specialization = "Cybersecurity, cloud computing, network security",
                    CoursesJson   = """["Cybersecurity","Cloud Computing","Network Security","Ethical Hacking"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.0,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Ahmad Hanandeh",
                    UniversityId  = asu.UniversityId,
                    Department    = "Intelligent Systems",
                    Specialization = "Machine learning, intelligent systems, technology management",
                    CoursesJson   = """["Machine Learning","Intelligent Systems","Artificial Intelligence","Technology Management"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.3,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Ghadeer Al Dweik",
                    UniversityId  = asu.UniversityId,
                    Department    = "Nursing",
                    Specialization = "Critical care nursing, clinical nursing, nursing science",
                    CoursesJson   = """["Critical Care Nursing","Clinical Nursing","Nursing Research","Patient Safety"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.8,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Mahmoud Alghizzawi",
                    UniversityId  = asu.UniversityId,
                    Department    = "Marketing",
                    Specialization = "Digital marketing, e-marketing, social media analytics",
                    CoursesJson   = """["Digital Marketing","Social Media Marketing","E-Commerce","Consumer Behavior"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.2,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Ahmed Otoom",
                    UniversityId  = asu.UniversityId,
                    Department    = "Information Technology",
                    Specialization = "Healthcare informatics, machine learning, clinical data systems",
                    CoursesJson   = """["Healthcare Informatics","Machine Learning","Data Mining","Clinical Information Systems"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.6,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Nagham Hendi",
                    UniversityId  = asu.UniversityId,
                    Department    = "Pharmacy",
                    Specialization = "Genomics, pharmaceutical chemistry, drug discovery",
                    CoursesJson   = """["Pharmaceutical Chemistry","Genomics","Drug Discovery","Medicinal Chemistry"]""",
                    TeachingStyle = "Practical",
                    Rating        = 2.8,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },

                // ── Al-Ahliyya Amman University ───────────────────────────────────
                new Professor
                {
                    FullName      = "Ahmad Samed Al-Adwan",
                    UniversityId  = aau.UniversityId,
                    Department    = "Business Technology",
                    Specialization = "E-learning, technology adoption, business technology",
                    CoursesJson   = """["E-Learning Systems","Technology Adoption","Management Information Systems","Digital Transformation"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.4,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Jamal Al-Nabulsi",
                    UniversityId  = aau.UniversityId,
                    Department    = "Medical Engineering",
                    Specialization = "Biomedical engineering, medical devices, healthcare technology",
                    CoursesJson   = """["Biomedical Engineering","Medical Devices","Healthcare Technology","Biomechanics"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.0,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Asim El-Sheikh",
                    UniversityId  = aau.UniversityId,
                    Department    = "Management Information Systems",
                    Specialization = "IT governance, e-business, information systems management",
                    CoursesJson   = """["IT Governance","E-Business","MIS","Enterprise Systems"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.9,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Khaled Halteh",
                    UniversityId  = aau.UniversityId,
                    Department    = "Financial Technology",
                    Specialization = "Financial technology, digital finance, banking innovation",
                    CoursesJson   = """["Financial Technology","Digital Banking","Blockchain","FinTech Regulation"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.5,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Musleh Ahmad Al-Tarawneh",
                    UniversityId  = aau.UniversityId,
                    Department    = "Law",
                    Specialization = "Public law, administrative law, constitutional law",
                    CoursesJson   = """["Constitutional Law","Administrative Law","Public International Law","Legal Research Methods"]""",
                    TeachingStyle = "Theoretical",
                    Rating        = 3.3,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Fayez Saleem Haddad",
                    UniversityId  = aau.UniversityId,
                    Department    = "Business",
                    Specialization = "Business management, hospitality management, entrepreneurship",
                    CoursesJson   = """["Business Management","Hospitality Management","Entrepreneurship","Strategic Management"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.7,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },

                // ── Al-Balqa Applied University ───────────────────────────────────
                new Professor
                {
                    FullName      = "Ali Al-Shawabkeh",
                    UniversityId  = bau.UniversityId,
                    Department    = "Scientific Basic Sciences",
                    Specialization = "Industrial chemistry, materials science, chemical analysis",
                    CoursesJson   = """["Industrial Chemistry","Materials Science","Chemical Analysis","Engineering Chemistry"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.8,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Noor M. Al-Kharabsheh",
                    UniversityId  = bau.UniversityId,
                    Department    = "Water Resources",
                    Specialization = "Hydrology, water resources engineering, environmental management",
                    CoursesJson   = """["Hydrology","Water Resources Engineering","Environmental Management","Irrigation Engineering"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.5,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "A. Alzoubaidi",
                    UniversityId  = bau.UniversityId,
                    Department    = "Computer Engineering",
                    Specialization = "Network engineering, digital systems, computer architecture",
                    CoursesJson   = """["Computer Networks","Digital Systems","Computer Architecture","Network Engineering"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.7,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Mohammad Al-Qudah",
                    UniversityId  = bau.UniversityId,
                    Department    = "Computer Science",
                    Specialization = "Information systems, database design, software development",
                    CoursesJson   = """["Information Systems","Database Design","Software Development","Programming Fundamentals"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.3,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Mohammad Bani Yaseen",
                    UniversityId  = bau.UniversityId,
                    Department    = "Applied Sciences",
                    Specialization = "Applied sciences, physics, engineering mathematics",
                    CoursesJson   = """["Applied Physics","Engineering Mathematics","Applied Sciences","General Physics"]""",
                    TeachingStyle = "Theoretical",
                    Rating        = 3.1,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Tarek Mazzawi",
                    UniversityId  = bau.UniversityId,
                    Department    = "Internal Medicine",
                    Specialization = "Gastroenterology, internal medicine, hepatology",
                    CoursesJson   = """["Internal Medicine","Gastroenterology","Clinical Diagnosis","Medical Ethics"]""",
                    TeachingStyle = "Practical",
                    Rating        = 2.5,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },

                // ── Al Hussein Technical University ───────────────────────────────
                new Professor
                {
                    FullName      = "Tarek A. Tutunji",
                    UniversityId  = htu.UniversityId,
                    Department    = "Engineering Technology",
                    Specialization = "Artificial intelligence, control systems, robotics, mechatronics",
                    CoursesJson   = """["Automatic Control","Robotics","Mechatronics","Artificial Intelligence"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.5,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Lutfi Al-Sharif",
                    UniversityId  = htu.UniversityId,
                    Department    = "Electrical Engineering",
                    Specialization = "Control systems, elevator and escalator systems, building transportation modelling",
                    CoursesJson   = """["Control Systems","Electrical Engineering","Building Transportation Systems","Modelling and Simulation"]""",
                    TeachingStyle = "Theoretical",
                    Rating        = 4.3,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Eyad Taqieddin",
                    UniversityId  = htu.UniversityId,
                    Department    = "Network Engineering and Security",
                    Specialization = "Cybersecurity, RFID security, wireless ad-hoc networks, multimedia security",
                    CoursesJson   = """["Network Security","Cybersecurity","Wireless Networks","RFID Systems"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.6,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Hussam J. Khasawneh",
                    UniversityId  = htu.UniversityId,
                    Department    = "Electrical Engineering",
                    Specialization = "Renewable energy systems, green hydrogen, IoT energy monitoring, electric vehicles",
                    CoursesJson   = """["Renewable Energy Systems","IoT","Electric Vehicles","Energy Storage"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.0,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Firas Jarrar",
                    UniversityId  = htu.UniversityId,
                    Department    = "Engineering Technology",
                    Specialization = "Advanced manufacturing, nanosatellites, robotic kinematics, nonlinear dynamics",
                    CoursesJson   = """["Advanced Manufacturing","Kinematics","Satellite Engineering","Nonlinear Dynamics"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.2,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Murad A. Yaghi",
                    UniversityId  = htu.UniversityId,
                    Department    = "Data Science and Artificial Intelligence",
                    Specialization = "Deep reinforcement learning, neural networks, intelligent control, evolutionary computation",
                    CoursesJson   = """["Deep Learning","Reinforcement Learning","Neural Networks","AI for Robotics"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.9,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Yara Haddad",
                    UniversityId  = htu.UniversityId,
                    Department    = "Energy Engineering",
                    Specialization = "Nuclear engineering, radiation shielding, renewable energy simulation",
                    CoursesJson   = """["Nuclear Engineering","Radiation Shielding","Renewable Energy","Energy Systems Simulation"]""",
                    TeachingStyle = "Practical",
                    Rating        = 3.7,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Dr. Ahmad Saleh",
                    UniversityId  = uj.UniversityId,
                    Department    = "AI",
                    Specialization = "Artificial Intelligence",
                    CoursesJson   = """["AI","Machine Learning","Data Mining"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.7,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Dr. Sara Khaled",
                    UniversityId  = uj.UniversityId,
                    Department    = "CS",
                    Specialization = "Networking",
                    CoursesJson   = """["Networking","Operating Systems","Computer Networks"]""",
                    TeachingStyle = "Theoretical",
                    Rating        = 4.2,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Dr. Omar Nasser",
                    UniversityId  = just.UniversityId,
                    Department    = "Math",
                    Specialization = "Calculus",
                    CoursesJson   = """["Calculus","Linear Algebra","Statistics"]""",
                    TeachingStyle = "Theoretical",
                    Rating        = 3.8,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Dr. Leen Mansour",
                    UniversityId  = just.UniversityId,
                    Department    = "AI",
                    Specialization = "Machine Learning",
                    CoursesJson   = """["AI","Machine Learning","Deep Learning"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.9,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                },
                new Professor
                {
                    FullName      = "Dr. Kareem Haddad",
                    UniversityId  = psut.UniversityId,
                    Department    = "CS",
                    Specialization = "Operating Systems",
                    CoursesJson   = """["Operating Systems","Data Structures","Programming"]""",
                    TeachingStyle = "Practical",
                    Rating        = 4.0,
                    ImageUrl      = null,
                    IsDeleted     = false,
                    CreatedAt     = DateTime.UtcNow
                }
            };

            db.Professors.AddRange(professors);
            await db.SaveChangesAsync();
        }
    }
}
