using eduRateSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

namespace eduRateSystem.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public RecommendationsController(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet("universities")]
        public async Task<IActionResult> GetUniversityRecommendations(
            [FromQuery] string city = "",
            [FromQuery] string major = "",
            [FromQuery] string level = "Bachelor",
            [FromQuery] string distanceSensitivity = "Medium",
            [FromQuery] string budgetSensitivity = "Medium",
            [FromQuery] int ranking = 3)
        {
            var universities = await _context.Universities
                .Where(u => !u.IsDeleted)
                .Select(u => new
                {
                    UniversityId = u.UniversityId,
                    Name = u.Name,
                    Location = u.Location,
                    Description = u.Description,
                    WebsiteUrl = u.WebsiteUrl,
                    ImageUrl = u.ImageUrl,
                    Majors = u.Majors,
                    Levels = u.Levels,
                    BudgetLevel = u.BudgetLevel,
                    Ranking = u.Ranking,
                    IsDeleted = u.IsDeleted,
                    OverallRating = u.UniversityReviews.Any(r => !r.IsDeleted)
                        ? u.UniversityReviews.Where(r => !r.IsDeleted).Average(r => (double)r.Rating)
                        : 0,
                    TotalReviews = u.UniversityReviews.Count(r => !r.IsDeleted)
                })
                .ToListAsync();

            var aiUrl = _configuration["AI_SERVICE_URL"];
            if (string.IsNullOrEmpty(aiUrl))
                return Ok(universities.OrderByDescending(u => u.OverallRating));

            var payload = JsonSerializer.Serialize(new
            {
                universities,
                city,
                preferred_major = major,
                level_of_study = level,
                distance_sensitivity = distanceSensitivity,
                budget_sensitivity = budgetSensitivity,
                preferred_ranking = ranking
            });

            try
            {
                var client = _httpClientFactory.CreateClient();
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var res = await client.PostAsync($"{aiUrl}/recommend/universities", content);
                if (!res.IsSuccessStatusCode)
                    return Ok(universities.OrderByDescending(u => u.OverallRating));
                var result = await res.Content.ReadAsStringAsync();
                return Content(result, "application/json");
            }
            catch
            {
                return Ok(universities.OrderByDescending(u => u.OverallRating));
            }
        }

        [HttpGet("professors")]
        public async Task<IActionResult> GetProfessorRecommendations(
            [FromQuery] string department = "",
            [FromQuery] string course = "",
            [FromQuery] string teachingStyle = "",
            [FromQuery] double rating = 4.0)
        {
            var professors = await _context.Professors
                .Where(p => !p.IsDeleted)
                .Select(p => new
                {
                    ProfessorId = p.ProfessorId,
                    FullName = p.FullName,
                    UniversityId = p.UniversityId,
                    Department = p.Department,
                    Specialization = p.Specialization,
                    TeachingStyle = p.TeachingStyle,
                    Rating = p.Rating,
                    Courses = p.CoursesJson,
                    IsDeleted = p.IsDeleted,
                    OverallRating = p.ProfessorReviews.Any(r => !r.IsDeleted)
                        ? p.ProfessorReviews.Where(r => !r.IsDeleted).Average(r => (double)r.Rating)
                        : 0,
                    TotalReviews = p.ProfessorReviews.Count(r => !r.IsDeleted)
                })
                .ToListAsync();

            var aiUrl = _configuration["AI_SERVICE_URL"];
            if (string.IsNullOrEmpty(aiUrl))
                return Ok(professors.OrderByDescending(p => p.OverallRating));

            var payload = JsonSerializer.Serialize(new
            {
                professors,
                preferred_department = department,
                course_interest = course,
                teaching_style_preference = teachingStyle,
                preferred_rating = rating
            });

            try
            {
                var client = _httpClientFactory.CreateClient();
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var res = await client.PostAsync($"{aiUrl}/recommend/professors", content);
                if (!res.IsSuccessStatusCode)
                    return Ok(professors.OrderByDescending(p => p.OverallRating));
                var result = await res.Content.ReadAsStringAsync();
                return Content(result, "application/json");
            }
            catch
            {
                return Ok(professors.OrderByDescending(p => p.OverallRating));
            }
        }
    }
}
