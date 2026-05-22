using System;

namespace eduRateSystem.DTOs.Professor
{
    public class ProfessorResponseDto
    {
        public int ProfessorId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int UniversityId { get; set; }
        public string Department { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string UniversityName { get; set; } = string.Empty;
        public double OverallRating { get; set; }
        public int TotalReviews { get; set; }
    }
}