using System.ComponentModel.DataAnnotations;

namespace eduRateSystem.DTOs.Course
{
    public class CreateCourseDto
    {
        [Required]
        public string CourseName { get; set; } = string.Empty;

        [Required]
        public string Department { get; set; } = string.Empty;

        [Required]
        public int UniversityId { get; set; }

        [Required]
        public int ProfessorId { get; set; }
    }
}