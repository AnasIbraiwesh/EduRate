using System.ComponentModel.DataAnnotations;

namespace eduRateSystem.DTOs
{
    public class ProfessorUpdateDto
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public int UniversityId { get; set; }

        [Required]
        public string Department { get; set; } = string.Empty;

        [Required]
        public string Specialization { get; set; } = string.Empty;
    }
}
