using System.ComponentModel.DataAnnotations;

namespace eduRateSystem.DTOs.Report
{
    public class CreateReportDto
    {
        [Required]
        public string ReportType { get; set; } = string.Empty;

        [Required]
        public int TargetId { get; set; }

        [Required]
        public string Reason { get; set; } = string.Empty;
    }
}