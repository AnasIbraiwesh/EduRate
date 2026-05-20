using System;

namespace eduRateSystem.DTOs.Course
{
    public class CourseResponseDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public int UniversityId { get; set; }
        public int ProfessorId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}