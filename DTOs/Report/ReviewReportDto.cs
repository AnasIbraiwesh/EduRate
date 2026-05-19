using System.ComponentModel.DataAnnotations;

namespace eduRateSystem.DTOs.Report
{
    public class ReviewReportDto
    {
        [Required]
        public string Status { get; set; } = string.Empty;
        public string? AdminNotes { get; set; }
        public bool RemoveTargetReview { get; set; } = false;
    }
}
