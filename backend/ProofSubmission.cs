using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eduRateSystem.Models
{
    public class ProofSubmission
    {
        [Key]
        public int ProofSubmissionId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string ProofType { get; set; } = string.Empty;

        [Required]
        public string TargetType {  get; set; } = string.Empty;

        [Required]
        public int TargetId { get; set; }

        [Required]
        public string FilePath { get; set; } = string.Empty;

        [Required]
        public string Status {  get; set; } = string.Empty;

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ReviewedAt {  get; set; }

        public string? ReviewedByAdminId { get; set; }

        public string? AdminNotes { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        [ForeignKey("ReviewedByAdminId")]
        public ApplicationUser? ReviewedByAdmin { get; set; }
    }
}
