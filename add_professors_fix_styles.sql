-- Step 1: Normalize teaching styles — Mixed becomes Practical
-- Result: only 'Practical' and 'Theoretical' remain
UPDATE Professors
SET TeachingStyle = 'Practical'
WHERE TeachingStyle = 'Mixed';

-- Step 2: Add 5 new professors
INSERT INTO Professors (FullName, UniversityId, Department, Specialization, CoursesJson, TeachingStyle, Rating, ImageUrl, IsDeleted, CreatedAt)
VALUES
('Dr. Ahmad Saleh',   1, 'AI',   'Artificial Intelligence', '["AI","Machine Learning","Data Mining"]',               'Practical',   4.7, NULL, 0, GETUTCDATE()),
('Dr. Sara Khaled',   1, 'CS',   'Networking',              '["Networking","Operating Systems","Computer Networks"]', 'Theoretical', 4.2, NULL, 0, GETUTCDATE()),
('Dr. Omar Nasser',   2, 'Math', 'Calculus',                '["Calculus","Linear Algebra","Statistics"]',            'Theoretical', 3.8, NULL, 0, GETUTCDATE()),
('Dr. Leen Mansour',  2, 'AI',   'Machine Learning',        '["AI","Machine Learning","Deep Learning"]',             'Practical',   4.9, NULL, 0, GETUTCDATE()),
('Dr. Kareem Haddad', 3, 'CS',   'Operating Systems',       '["Operating Systems","Data Structures","Programming"]', 'Practical',   4.0, NULL, 0, GETUTCDATE());
