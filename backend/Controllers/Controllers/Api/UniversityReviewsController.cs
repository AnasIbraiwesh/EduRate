using eduRateSystem.Data;
using eduRateSystem.DTOs.UniversityReview;
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
    public class UniversityReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public UniversityReviewsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UniversityReviewResponseDto>>> GetAll([FromQuery] int? universityId)
        {
            var query = _context.UniversityReviews
                .Where(r => !r.IsDeleted)
                .AsQueryable();

            if(universityId.HasValue)
            {
                query= query.Where(r => r.UniversityId == universityId.Value);
            }

            var review = await query
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new UniversityReviewResponseDto
                {
                    UniversityReviewId = r.UniversityReviewId,
                    UniversityId = r.UniversityId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    Sentiment = r.Sentiment,
                    CreatedAt = r.CreatedAt,
                    CategoriesJson = r.CategoriesJson
                })
                .ToListAsync();

            return Ok(review);
        }
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<UniversityReviewResponseDto>> GetById(int id)
        {
            var review = await _context.UniversityReviews
                .Where(r => r.UniversityReviewId == id && !r.IsDeleted)
                .Select(r => new UniversityReviewResponseDto
                {
                    UniversityReviewId = r.UniversityReviewId,
                    UniversityId = r.UniversityId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    Sentiment = r.Sentiment,
                    CreatedAt = r.CreatedAt,
                    CategoriesJson = r.CategoriesJson
                })
                .FirstOrDefaultAsync();

            if (review == null)
            {
                return NotFound(new { message = "Review Not Found." });
            }

            return Ok(review);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UniversityReviewResponseDto>>> GetMine()
        {
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var reviews = await _context.UniversityReviews
                .Where(r => r.UserId == userId && !r.IsDeleted)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new UniversityReviewResponseDto
                {
                    UniversityReviewId = r.UniversityReviewId,
                    UniversityId = r.UniversityId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    Sentiment = r.Sentiment,
                    CreatedAt = r.CreatedAt,
                    CategoriesJson = r.CategoriesJson
                })
                .ToListAsync();

            return Ok(reviews);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<UniversityReviewResponseDto>> Create(CreateUniversityReviewDto dto)
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

            var universityExists = await _context.Universities
                .AnyAsync(u => u.UniversityId == dto.UniversityId && !u.IsDeleted);

            if (!universityExists)
            {
                return BadRequest(new { message = "The Selected University Does Not Exist." });
            }

            var reviewer = await _userManager.FindByIdAsync(userId!);
            if (reviewer == null || !reviewer.IsVerifiedStudent)
                return StatusCode(403, new { message = "You must sign up with a university email (.edu.jo) to write reviews." });

            var alreadyReviewed = await _context.UniversityReviews.AnyAsync(r =>
                r.UserId == userId &&
                r.UniversityId == dto.UniversityId &&
                !r.IsDeleted);

            if (alreadyReviewed)
            {
                return Conflict(new { message = "You Already Reviewed This University." });
            }

            var review = new UniversityReview
            {
                UserId = userId,
                UniversityId = dto.UniversityId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CategoriesJson = dto.CategoriesJson,
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

            _context.UniversityReviews.Add(review);
            await _context.SaveChangesAsync();

            var response = new UniversityReviewResponseDto
            {
                UniversityReviewId = review.UniversityReviewId,
                UniversityId = review.UniversityId,
                Rating = review.Rating,
                Comment = review.Comment,
                Sentiment = review.Sentiment,
                CreatedAt = review.CreatedAt,
                CategoriesJson = review.CategoriesJson
            };

            return CreatedAtAction(nameof(GetById), new { id = review.UniversityReviewId }, response);
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<ActionResult<UniversityReviewResponseDto>> Update(int id, UpdateUniversityReviewDto dto)
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

            var review = await _context.UniversityReviews
                .FirstOrDefaultAsync(r => r.UniversityReviewId == id && !r.IsDeleted);

            if (review == null)
            {
                return NotFound(new { message = "Review Not Found." });
            }

            if (review.UserId != userId)
            {
                return Forbid();
            }

            review.Rating = dto.Rating;
            review.Comment = dto.Comment;

            await _context.SaveChangesAsync();

            var response = new UniversityReviewResponseDto
            {
                UniversityReviewId = review.UniversityReviewId,
                UniversityId = review.UniversityId,
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

            var review = await _context.UniversityReviews
                .FirstOrDefaultAsync(r => r.UniversityReviewId == id && !r.IsDeleted);

            if (review == null)
            {
                return NotFound(new { message = "Review Not Found." });
            }

            if (review.UserId != userId)
            {
                return Forbid();
            }

            review.IsDeleted = true;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Review Deleted Successfully." });
        }

        [HttpDelete("admin/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminDelete(int id)
        {
            var review = await _context.UniversityReviews
                .FirstOrDefaultAsync(r => r.UniversityReviewId == id && !r.IsDeleted);

            if (review == null)
            {
                return NotFound(new { message = "Review Not Found." });
            }

            review.IsDeleted = true;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Review Removed By Admin." });
        }
    }
}