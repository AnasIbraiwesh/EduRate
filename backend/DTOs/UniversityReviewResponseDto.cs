namespace eduRateSystem.DTOs
{
    public class UniversityReviewResponseDto
    {
        public int UniversityReviewId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int UniversityId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
