using System;
namespace eduRateSystem.DTOs.ProofSubmission
{
    public class MyProofSubmissionResponseDto
    {
        //doesn't expose userId or adminId
        public int ProofSubmissionId { get; set; }
        public string ProofType { get; set; } = string.Empty;
        public string TargetType {  get; set; } = string.Empty;
        public int TargetId { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public string Status {  get; set; } = string.Empty;
        public DateTime SubmittedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public string? AdminNotes { get; set; }
    }
}
