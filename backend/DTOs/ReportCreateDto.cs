using System.ComponentModel.DataAnnotations;

namespace eduRateSystem.DTOs
{
    public class ReportCreateDto
    {
        [Required]
        public string ReportType { get; set; } = string.Empty;

        [Required]
        public int TargetId { get; set; }

        [Required]
        public string Reason { get; set; } = string.Empty;
    }
}
