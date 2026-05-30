using System.ComponentModel.DataAnnotations;

namespace eduRateSystem.DTOs.ProfessorReview
{
    public class CreateProfessorReviewDto
    {
        [Required]
        public int ProfessorId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Difficulty must be between 1 and 5.")]
        public int Difficulty { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Workload must be between 1 and 5.")]
        public int Workload { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Friendliness must be between 1 and 5.")]
        public int Friendliness { get; set; }

        [Required]
        public string Comment { get; set; } = string.Empty;

        public bool? WouldTakeAgain { get; set; }
    }
}