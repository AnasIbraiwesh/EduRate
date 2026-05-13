using System.ComponentModel.DataAnnotations;

namespace eduRateSystem.DTOs
{
    public class UniversityReviewCreateDto
    {
        [Required]
        public int UniversityId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public string Comment { get; set; } = string.Empty;
    }
}
