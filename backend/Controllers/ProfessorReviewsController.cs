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
    [Route("api/professor-reviews")]
    public class ProfessorReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProfessorReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _context.ProfessorReviews
                .Where(r => !r.IsDeleted)
                .Select(r => new ProfessorReviewResponseDto
                {
                    ProfessorReviewId = r.ProfessorReviewId,
                    UserId = r.UserId,
                    ProfessorId = r.ProfessorId,
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
            var review = await _context.ProfessorReviews
                .Where(r => r.ProfessorReviewId == id && !r.IsDeleted)
                .Select(r => new ProfessorReviewResponseDto
                {
                    ProfessorReviewId = r.ProfessorReviewId,
                    UserId = r.UserId,
                    ProfessorId = r.ProfessorId,
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
        public async Task<IActionResult> Create([FromBody] ProfessorReviewCreateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var professorExists = await _context.Professors
                .AnyAsync(p => p.ProfessorId == dto.ProfessorId && !p.IsDeleted);

            if (!professorExists) return NotFound("Professor not found.");

            var alreadyReviewed = await _context.ProfessorReviews
                .AnyAsync(r => r.UserId == userId && r.ProfessorId == dto.ProfessorId && !r.IsDeleted);

            if (alreadyReviewed) return Conflict("You have already reviewed this professor.");

            var review = new ProfessorReview
            {
                UserId = userId!,
                ProfessorId = dto.ProfessorId,
                Rating = dto.Rating,
                Comment = dto.Comment
            };

            _context.ProfessorReviews.Add(review);
            await _context.SaveChangesAsync();

            var response = new ProfessorReviewResponseDto
            {
                ProfessorReviewId = review.ProfessorReviewId,
                UserId = review.UserId,
                ProfessorId = review.ProfessorId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt
            };

            return CreatedAtAction(nameof(GetById), new { id = review.ProfessorReviewId }, response);
        }
    }
}
