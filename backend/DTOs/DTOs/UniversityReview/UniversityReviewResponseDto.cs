using System;

namespace eduRateSystem.DTOs.UniversityReview
{
    public class UniversityReviewResponseDto
    {
        public int UniversityReviewId { get; set; }
        public int UniversityId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public string? Sentiment { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CategoriesJson { get; set; }
    }
}