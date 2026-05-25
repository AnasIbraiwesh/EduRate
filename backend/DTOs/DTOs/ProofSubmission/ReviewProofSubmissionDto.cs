using System.ComponentModel.DataAnnotations;

namespace eduRateSystem.DTOs.ProofSubmission
{
    public class ReviewProofSubmissionDto
    {
        [Required]
        public string Status { get; set; } = string.Empty;

        public string? AdminNotes { get; set; }
    }
}