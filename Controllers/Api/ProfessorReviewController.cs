using eduRateSystem.Data;
using eduRateSystem.DTOs.ProfessorReview;
using eduRateSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eduRateSystem.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfessorReviewsController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

            var hasApprovedProof = await _context.ProofSubmissions.AnyAsync(p =>
                p.UserId == userId &&
                p.TargetType == "Professor" &&
                p.TargetId == dto.ProfessorId &&
                p.Status == "Approved");

            if (!hasApprovedProof)
            {
                return BadRequest(new
                {
                    message = "You need an approved proof submission before reviewing this professor."
                });
            }

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

            _context.ProfessorReviews.Add(review);
            await _context.SaveChangesAsync();

            var response = new ProfessorReviewResponseDto
            {
                ProfessorReviewId = review.ProfessorReviewId,
                ProfessorId = review.ProfessorId,
                Rating = review.Rating,
                Comment = review.Comment,
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

            var response = new ProfessorReviewResponseDto
            {
                ProfessorReviewId = review.ProfessorReviewId,
                ProfessorId = review.ProfessorId,
                Rating = review.Rating,
                Comment = review.Comment,
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

            return Ok(new { message = "Review removed by admin." });
        }
    }
}