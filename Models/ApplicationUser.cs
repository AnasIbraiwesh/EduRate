using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eduRateSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        public bool IsVerifiedStudent { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<UniversityReview> UniversityReviews { get; set; } = new List<UniversityReview>();
        public ICollection<ProfessorReview> ProfessorReviews { get; set; } = new List<ProfessorReview>();
        public ICollection<ProofSubmission> ProofSubmissions { get; set;} = new List<ProofSubmission>();
        public ICollection<Report> ReportsSubmitted { get; set; } = new List<Report>();
    }
}
