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
                    Comment = r.Comment,
                    Sentiment = r.Sentiment,
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
                    Comment = r.Comment,
                    Sentiment = r.Sentiment,
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
                    Comment = r.Comment,
                    Sentiment = r.Sentiment,
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

            var professorExists = await _context.Professors
                .AnyAsync(p => p.ProfessorId == dto.ProfessorId && !p.IsDeleted);

            if (!professorExists)
            {
                return BadRequest(new { message = "The selected professor does not exist." });
            }

            var reviewer = await _userManager.FindByIdAsync(userId!);
            if (reviewer == null || !reviewer.IsVerifiedStudent)
                return StatusCode(403, new { message = "You must sign up with a university email (.edu.jo) to write reviews." });

            var alreadyReviewed = await _context.ProfessorReviews.AnyAsync(r =>
                r.UserId == userId &&
                r.ProfessorId == dto.ProfessorId &&
                !r.IsDeleted);

            if (alreadyReviewed)
            {
                return Conflict(new { message = "You already reviewed this professor." });
            }

            var review = new ProfessorReview
            {
                UserId = userId,
                ProfessorId = dto.ProfessorId,
                Rating = dto.Rating,
                Comment = dto.Comment,
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

                    var filterContent = new StringContent(payload, Encoding.UTF8, "application/json");
                    var filterRes = await client.PostAsync($"{aiUrl}/filter", filterContent);
                    if (filterRes.IsSuccessStatusCode)
                    {
                        var filterData = JsonSerializer.Deserialize<JsonElement>(await filterRes.Content.ReadAsStringAsync());
                        if (!filterData.GetProperty("approved").GetBoolean())
                            return StatusCode(422, new { message = "Your review was flagged as inappropriate and could not be submitted." });
                    }

                    var sentimentContent = new StringContent(payload, Encoding.UTF8, "application/json");
                    var sentimentRes = await client.PostAsync($"{aiUrl}/sentiment", sentimentContent);
                    if (sentimentRes.IsSuccessStatusCode)
                    {
                        var sentimentData = JsonSerializer.Deserialize<JsonElement>(await sentimentRes.Content.ReadAsStringAsync());
                        review.Sentiment = sentimentData.GetProperty("label").GetString();
                    }
                }
                catch { /* AI service unavailable — skip silently */ }
            }

            _context.ProfessorReviews.Add(review);
            await _context.SaveChangesAsync();

            var professor = await _context.Professors.FindAsync(review.ProfessorId);
            if (professor != null)
            {
                professor.Rating = await _context.ProfessorReviews
                    .Where(r => r.ProfessorId == review.ProfessorId && !r.IsDeleted)
                    .AverageAsync(r => (double?)r.Rating) ?? 0;
                await _context.SaveChangesAsync();
            }

            var response = new ProfessorReviewResponseDto
            {
                ProfessorReviewId = review.ProfessorReviewId,
                ProfessorId = review.ProfessorId,
                Rating = review.Rating,
                Comment = review.Comment,
                Sentiment = review.Sentiment,
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

            review.Rating = dto.Rating;
            review.Comment = dto.Comment;

            await _context.SaveChangesAsync();

            var professor = await _context.Professors.FindAsync(review.ProfessorId);
            if (professor != null)
            {
                professor.Rating = await _context.ProfessorReviews
                    .Where(r => r.ProfessorId == review.ProfessorId && !r.IsDeleted)
                    .AverageAsync(r => (double?)r.Rating) ?? 0;
                await _context.SaveChangesAsync();
            }

            var response = new ProfessorReviewResponseDto
            {
                ProfessorReviewId = review.ProfessorReviewId,
                ProfessorId = review.ProfessorId,
                Rating = review.Rating,
                Comment = review.Comment,
                Sentiment = review.Sentiment,
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

            var professor = await _context.Professors.FindAsync(review.ProfessorId);
            if (professor != null)
            {
                professor.Rating = await _context.ProfessorReviews
                    .Where(r => r.ProfessorId == review.ProfessorId && !r.IsDeleted)
                    .AverageAsync(r => (double?)r.Rating) ?? 0;
                await _context.SaveChangesAsync();
            }

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

            var professor = await _context.Professors.FindAsync(review.ProfessorId);
            if (professor != null)
            {
                professor.Rating = await _context.ProfessorReviews
                    .Where(r => r.ProfessorId == review.ProfessorId && !r.IsDeleted)
                    .AverageAsync(r => (double?)r.Rating) ?? 0;
                await _context.SaveChangesAsync();
            }

            return Ok(new { message = "Review removed by admin." });
        }
    }
}