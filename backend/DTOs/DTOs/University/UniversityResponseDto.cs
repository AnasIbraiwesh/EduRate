using System;

namespace eduRateSystem.DTOs.University
{
    public class UniversityResponseDto
    {
        public int UniversityId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location {  get; set; } = string.Empty;
        public string WebsiteUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public double OverallRating { get; set; }
        public int TotalReviews { get; set; }
        public string? ImageUrl { get; set; }
        public int Ranking { get; set; }
    }
}
