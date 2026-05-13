namespace eduRateSystem.DTOs
{
    public class ProfessorReviewResponseDto
    {
        public int ProfessorReviewId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int ProfessorId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
