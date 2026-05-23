using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eduRateSystem.Models
{
    public class ProfessorReview
    {
        [Key]
        public int ProfessorReviewId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int ProfessorId { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public string Comment { get; set; } = string.Empty;

        public string? Sentiment { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("ProfessorId")]
        public Professor? Professor { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
    }
}
