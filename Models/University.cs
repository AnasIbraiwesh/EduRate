using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eduRateSystem.Models
{
    public class University
    {
        [Key]
        public int UniversityId { get; set; }

        [Required]
        public int BudgetLevel { get; set; }

        public int Ranking { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Majors { get; set; } = string.Empty;

        [Required]
        public string Levels { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required] 
        public string WebsiteUrl { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Professor> Professors { get; set; } = new List<Professor>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<UniversityReview> UniversityReviews { get; set; } = new List<UniversityReview>();


    }
}
