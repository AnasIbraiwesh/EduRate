using eduRateSystem.Data;
using eduRateSystem.DTOs.ProfessorReview;
using eduRateSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

namespace eduRateSystem.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ProfessorReviewsController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProfessorReviewResponseDto>>> GetAll([FromQuery] int? professorId)
        {
            var query = _context.ProfessorReviews
                .Where(r => !r.IsDeleted)
                .AsQueryable();

            if (professorId.HasValue)
            {
                query = query.Where(r => r.ProfessorId == professorId.Value);
            }

            var reviews = await query
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new ProfessorReviewResponseDto
                {
                    ProfessorReviewId = r.ProfessorReviewId,
                    ProfessorId = r.ProfessorId,
                    Rating = r.Rating,
                    Difficulty = r.Difficulty,
                    Workload = r.Workload,
                    Friendliness = r.Friendliness,
                    Comment = r.Comment,
                    Sentiment = r.Sentiment,
                    WouldTakeAgain = r.WouldTakeAgain,
                    CreatedAt = r.CreatedAt
                })
                .ToListAsync();

            return Ok(reviews);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProfessorReviewResponseDto>> GetById(int id)
        {
            var review = await _context.ProfessorReviews
                .Where(r => r.ProfessorReviewId == id && !r.IsDeleted)
                .Select(r => new ProfessorReviewResponseDto
                {
                    ProfessorReviewId = r.ProfessorReviewId,
                    ProfessorId = r.ProfessorId,
                    Rating = r.Rating,
                    Difficulty = r.Difficulty,
                    Workload = r.Workload,
                    Friendliness = r.Friendliness,
                    Comment = r.Comment,
                    Sentiment = r.Sentiment,
                    WouldTakeAgain = r.WouldTakeAgain,
                    CreatedAt = r.CreatedAt
                })
                .FirstOrDefaultAsync();

            if (review == null)
            {
                return NotFound(new { message = "Review not found." });
            }

            return Ok(review);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProfessorReviewResponseDto>>> GetMine()
        {
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var reviews = await _context.ProfessorReviews
                .Where(r => r.UserId == userId && !r.IsDeleted)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new ProfessorReviewResponseDto
                {
                    ProfessorReviewId = r.ProfessorReviewId,
                    ProfessorId = r.ProfessorId,
                    Rating = r.Rating,
                    Difficulty = r.Difficulty,
                    Workload = r.Workload,
                    Friendliness = r.Friendliness,
                    Comment = r.Comment,
                    Sentiment = r.Sentiment,
                    WouldTakeAgain = r.WouldTakeAgain,
                    CreatedAt = r.CreatedAt
                })
                .ToListAsync();

            return Ok(reviews);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProfessorReviewResponseDto>> Create(CreateProfessorReviewDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var professor = await _context.Professors
                .FirstOrDefaultAsync(p => p.ProfessorId == dto.ProfessorId && !p.IsDeleted);

            if (professor == null)
            {
                return BadRequest(new { message = "The selected professor does not exist." });
            }

            var reviewer = await _userManager.FindByIdAsync(userId!);
            if (reviewer == null || !reviewer.IsVerifiedStudent)
                return StatusCode(403, new { message = "You must sign up with a university email (.edu.jo) to write reviews." });

            if (reviewer.UniversityId == null)
                return StatusCode(403, new { message = "Please select your university in Settings before writing reviews." });

            if (professor.UniversityId != reviewer.UniversityId)
                return StatusCode(403, new { message = "You can only review professors from your university." });

            var currentSemester = GetCurrentSemester(DateTime.UtcNow);

            var alreadyReviewed = await _context.ProfessorReviews.AnyAsync(r =>
                r.UserId == userId &&
                r.ProfessorId == dto.ProfessorId &&
                r.Semester == currentSemester &&
                !r.IsDeleted);

            if (alreadyReviewed)
            {
                return Conflict(new { message = "You already reviewed this professor this semester." });
            }

            var review = new ProfessorReview
            {
                UserId = userId,
                ProfessorId = dto.ProfessorId,
                Difficulty = dto.Difficulty,
                Workload = dto.Workload,
                Friendliness = dto.Friendliness,
                Rating = (int)Math.Round((2.0 * dto.Friendliness + (6 - dto.Difficulty) + (6 - dto.Workload)) / 4.0),
                Comment = dto.Comment,
                WouldTakeAgain = dto.WouldTakeAgain,
                Semester = currentSemester,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };

            var aiUrl = _configuration["AI_SERVICE_URL"];
            if (!string.IsNullOrEmpty(aiUrl))
            {
                try
                {
                    var client = _httpClientFactory.CreateClient();
                    var payload = JsonSerializer.Serialize(new { text = dto.Comment });

                    var filterContent    = new StringContent(payload, Encoding.UTF8, "application/json");
                    var sentimentContent = new StringContent(payload, Encoding.UTF8, "application/json");
                    var filterTask    = client.PostAsync($"{aiUrl}/filter",    filterContent);
                    var sentimentTask = client.PostAsync($"{aiUrl}/sentiment", sentimentContent);
                    await Task.WhenAll(filterTask, sentimentTask);

                    if (!filterTask.Result.IsSuccessStatusCode)
                        return StatusCode(503, new { message = "The AI moderation service is currently unavailable. Please try again later." });

                    var filterData = JsonSerializer.Deserialize<JsonElement>(await filterTask.Result.Content.ReadAsStringAsync());
                    if (!filterData.GetProperty("approved").GetBoolean())
                        return StatusCode(422, new { message = "Your review was flagged as inappropriate and could not be submitted." });

                    if (sentimentTask.Result.IsSuccessStatusCode)
                    {
                        var sentimentData = JsonSerializer.Deserialize<JsonElement>(await sentimentTask.Result.Content.ReadAsStringAsync());
                        review.Sentiment = sentimentData.GetProperty("label").GetString();
                    }
                }
                catch { return StatusCode(503, new { message = "The AI moderation service is currently unavailable. Please try again later." }); }
            }

            _context.ProfessorReviews.Add(review);
            await _context.SaveChangesAsync();

            await RecomputeProfessorRating(review.ProfessorId);

            var response = new ProfessorReviewResponseDto
            {
                ProfessorReviewId = review.ProfessorReviewId,
                ProfessorId = review.ProfessorId,
                Rating = review.Rating,
                Difficulty = review.Difficulty,
                Workload = review.Workload,
                Friendliness = review.Friendliness,
                Comment = review.Comment,
                Sentiment = review.Sentiment,
                WouldTakeAgain = review.WouldTakeAgain,
                CreatedAt = review.CreatedAt
            };

            return CreatedAtAction(nameof(GetById), new { id = review.ProfessorReviewId }, response);
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<ActionResult<ProfessorReviewResponseDto>> Update(int id, UpdateProfessorReviewDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var review = await _context.ProfessorReviews
                .FirstOrDefaultAsync(r => r.ProfessorReviewId == id && !r.IsDeleted);

            if (review == null)
            {
                return NotFound(new { message = "Review not found." });
            }

            if (review.UserId != userId)
            {
                return Forbid();
            }

            review.Difficulty = dto.Difficulty;
            review.Workload = dto.Workload;
            review.Friendliness = dto.Friendliness;
            review.Rating = (int)Math.Round((2.0 * dto.Friendliness + (6 - dto.Difficulty) + (6 - dto.Workload)) / 4.0);
            review.Comment = dto.Comment;

            await _context.SaveChangesAsync();

            await RecomputeProfessorRating(review.ProfessorId);

            var response = new ProfessorReviewResponseDto
            {
                ProfessorReviewId = review.ProfessorReviewId,
                ProfessorId = review.ProfessorId,
                Rating = review.Rating,
                Difficulty = review.Difficulty,
                Workload = review.Workload,
                Friendliness = review.Friendliness,
                Comment = review.Comment,
                Sentiment = review.Sentiment,
                WouldTakeAgain = review.WouldTakeAgain,
                CreatedAt = review.CreatedAt
            };

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var review = await _context.ProfessorReviews
                .FirstOrDefaultAsync(r => r.ProfessorReviewId == id && !r.IsDeleted);

            if (review == null)
            {
                return NotFound(new { message = "Review not found." });
            }

            if (review.UserId != userId)
            {
                return Forbid();
            }

            review.IsDeleted = true;
            await _context.SaveChangesAsync();

            await RecomputeProfessorRating(review.ProfessorId);

            return Ok(new { message = "Review deleted successfully." });
        }

        [HttpDelete("admin/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminDelete(int id)
        {
            var review = await _context.ProfessorReviews
                .FirstOrDefaultAsync(r => r.ProfessorReviewId == id && !r.IsDeleted);

            if (review == null)
            {
                return NotFound(new { message = "Review not found." });
            }

            review.IsDeleted = true;
            await _context.SaveChangesAsync();

            await RecomputeProfessorRating(review.ProfessorId);

            return Ok(new { message = "Review removed by admin." });
        }

        // Overall = (2*Friendliness + (6 - Difficulty) + (6 - Workload)) / 4
        // Friendliness raises the score; higher Difficulty/Workload lower it. Stays within 1-5.
        private async Task RecomputeProfessorRating(int professorId)
        {
            var professor = await _context.Professors.FindAsync(professorId);
            if (professor == null) return;

            var reviews = _context.ProfessorReviews
                .Where(r => r.ProfessorId == professorId && !r.IsDeleted);

            if (await reviews.AnyAsync())
            {
                var avgDifficulty   = await reviews.AverageAsync(r => (double)r.Difficulty);
                var avgWorkload     = await reviews.AverageAsync(r => (double)r.Workload);
                var avgFriendliness = await reviews.AverageAsync(r => (double)r.Friendliness);
                professor.Rating = (2 * avgFriendliness + (6 - avgDifficulty) + (6 - avgWorkload)) / 4.0;
            }
            else
            {
                professor.Rating = 0;
            }

            await _context.SaveChangesAsync();
        }

        private static string GetCurrentSemester(DateTime date)
        {
            var month = date.Month;
            var season = month >= 9 ? "Fall" : month >= 7 ? "Summer" : "Spring";
            return $"{season}-{date.Year}";
        }
    }
}