using System.ComponentModel.DataAnnotations;

namespace eduRateSystem.DTOs.UniversityReview
{
    public class CreateUniversityReviewDto
    {
        [Required]
        public int UniversityId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [Required]
        public string Comment { get; set; } = string.Empty;

        public string? CategoriesJson { get; set; }
    }
}