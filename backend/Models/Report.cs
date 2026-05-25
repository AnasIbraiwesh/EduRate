using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eduRateSystem.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }

        [Required]
        public string ReportedByUserId { get; set; } = string.Empty;

        [Required]
        public string ReportType { get; set; } = string.Empty;

        [Required]
        public int TargetId { get; set; }

        [Required]
        public string Reason { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? ReviewedByAdminId { get; set; }

        public DateTime? ReviewedAt { get; set; }

        public string? AdminNotes { get; set; }

        [ForeignKey("ReportedByUserId")]
        public ApplicationUser? ReportedByUser { get; set; }

        [ForeignKey("ReviewedByAdminId")]
        public ApplicationUser? ReviewedByAdmin {  get; set; }
    }
}
