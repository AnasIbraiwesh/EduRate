using System;

namespace eduRateSystem.DTOs.Report
{
    public class ReportResponseDto
    {
        public int ReportId { get; set; }
        public string ReportedByUserId { get; set; } = string.Empty;
        public string ReportType { get; set; } = string.Empty;
        public int TargetId { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? ReviewedByAdminId { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public string? AdminNotes { get; set; }
    }
}