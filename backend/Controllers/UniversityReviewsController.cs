using System.Security.Claims;
using eduRateSystem.Data;
using eduRateSystem.DTOs;
using eduRateSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eduRateSystem.Controllers
{
    [ApiController]
    [Route("api/university-reviews")]
    public class UniversityReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UniversityReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _context.UniversityReviews
                .Where(r => !r.IsDeleted)
                .Select(r => new UniversityReviewResponseDto
                {
                    UniversityReviewId = r.UniversityReviewId,
                    UserId = r.UserId,
                    UniversityId = r.UniversityId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt
                })
                .ToListAsync();

            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var review = await _context.UniversityReviews
                .Where(r => r.UniversityReviewId == id && !r.IsDeleted)
                .Select(r => new UniversityReviewResponseDto
                {
                    UniversityReviewId = r.UniversityReviewId,
                    UserId = r.UserId,
                    UniversityId = r.UniversityId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt
                })
                .FirstOrDefaultAsync();

            if (review == null) return NotFound();
            return Ok(review);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] UniversityReviewCreateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var universityExists = await _context.Universities
                .AnyAsync(u => u.UniversityId == dto.UniversityId && !u.IsDeleted);

            if (!universityExists) return NotFound("University not found.");

            var alreadyReviewed = await _context.UniversityReviews
                .AnyAsync(r => r.UserId == userId && r.UniversityId == dto.UniversityId && !r.IsDeleted);

            if (alreadyReviewed) return Conflict("You have already reviewed this university.");

            var review = new UniversityReview
            {
                UserId = userId!,
                UniversityId = dto.UniversityId,
                Rating = dto.Rating,
                Comment = dto.Comment
            };

            _context.UniversityReviews.Add(review);
            await _context.SaveChangesAsync();

            var response = new UniversityReviewResponseDto
            {
                UniversityReviewId = review.UniversityReviewId,
                UserId = review.UserId,
                UniversityId = review.UniversityId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt
            };

            return CreatedAtAction(nameof(GetById), new { id = review.UniversityReviewId }, response);
        }
    }
}
