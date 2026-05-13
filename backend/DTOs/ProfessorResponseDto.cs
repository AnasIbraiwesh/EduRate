namespace eduRateSystem.DTOs
{
    public class ProfessorResponseDto
    {
        public int ProfessorId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int UniversityId { get; set; }
        public string Department { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
