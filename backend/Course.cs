using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eduRateSystem.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        public string CourseName { get; set; } = string.Empty;

        [Required]
        public string Department { get; set; } = string.Empty;

        [Required]
        public int UniversityId { get; set; }

        [Required]
        public int ProfessorId { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("UniversityId")]
        public University? University { get; set; }

        [ForeignKey("ProfessorId")]
        public Professor? Professor { get; set; }
    }
}
