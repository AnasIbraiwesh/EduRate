using System.ComponentModel.DataAnnotations;

namespace eduRateSystem.DTOs.ProofSubmission
{
    public class CreateProofSubmissionDto
    {
        [Required]
        public string ProofType { get; set; } = string.Empty;

        [Required]
        public string TargetType { get; set; } = string.Empty;

        [Required]
        public int TargetId { get; set; }

        [Required]
        public string FilePath { get; set; } = string.Empty;
    }
}