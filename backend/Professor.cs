using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eduRateSystem.Models
{
    public class Professor
    {
        [Key]
        public int ProfessorId { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;
        
        [Required]
        public int UniversityId { get; set; }

        [Required]
        public string Department { get; set; } = string.Empty;

        [Required]
        public string Specialization {  get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("UniversityId")]
        public University? University { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<ProfessorReview> ProfessorReviews { get; set; } = new List<ProfessorReview>();
    }
}
