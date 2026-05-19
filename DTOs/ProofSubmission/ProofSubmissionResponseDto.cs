using System;

namespace eduRateSystem.DTOs.ProofSubmission
{
    public class ProofSubmissionResponseDto
    {
        public int ProofSubmissionId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string ProofType { get; set; } = string.Empty;
        public string TargetType { get; set; } = string.Empty;
        public int TargetId { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime SubmittedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public string? ReviewedByAdminId { get; set; }
        public string? AdminNotes { get; set; }
    }
}