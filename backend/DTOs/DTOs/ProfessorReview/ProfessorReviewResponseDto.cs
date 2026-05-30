using System;

namespace eduRateSystem.DTOs.ProfessorReview
{
    public class ProfessorReviewResponseDto
    {
        public int ProfessorReviewId { get; set; }
        public int ProfessorId { get; set; }
        public int Rating { get; set; }
        public int Difficulty { get; set; }
        public int Workload { get; set; }
        public int Friendliness { get; set; }
        public string Comment { get; set; } = string.Empty;
        public string? Sentiment { get; set; }
        public bool? WouldTakeAgain { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}