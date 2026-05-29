using System.ComponentModel.DataAnnotations;

namespace eduRateSystem.DTOs.Auth
{
    public class UpdateUniversityDto
    {
        [Required]
        public int UniversityId { get; set; }
    }
}
