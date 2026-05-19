using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eduRateSystem.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public int RoleId { get; set; }

        public bool IsVerifiedStudent { get; set; } = false; 

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("RoleId")]
        public Role? Role { get; set; }

        public ICollection<UniversityReview> UniversityReviews { get; set; } = new List<UniversityReview>();
        public ICollection<ProfessorReview> ProfessorReviews { get; set; } = new List<ProfessorReview>();
        public ICollection<ProofSubmission> ProofSubmissions { get; set; } = new List<ProofSubmission>();
        public ICollection<Report> ReportsSubmitted { get; set; } = new List<Report>();

    }
}
