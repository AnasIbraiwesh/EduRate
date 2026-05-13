using System.ComponentModel.DataAnnotations;

namespace eduRateSystem.DTOs
{
    public class UniversityUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string WebsiteUrl { get; set; } = string.Empty;
    }
}
