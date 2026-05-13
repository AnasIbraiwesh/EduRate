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
    [Route("api/proof-submissions")]
    public class ProofSubmissionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProofSubmissionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ProofSubmissionCreateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var submission = new ProofSubmission
            {
                UserId = userId!,
                ProofType = dto.ProofType,
                TargetType = dto.TargetType,
                TargetId = dto.TargetId,
                FilePath = dto.FilePath,
                Status = "Pending"
            };

            _context.ProofSubmissions.Add(submission);
            await _context.SaveChangesAsync();

            var response = MapToResponse(submission);
            return CreatedAtAction(nameof(GetById), new { id = submission.ProofSubmissionId }, response);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var submissions = await _context.ProofSubmissions
                .Select(s => MapToResponse(s))
                .ToListAsync();

            return Ok(submissions);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var submission = await _context.ProofSubmissions
                .FirstOrDefaultAsync(s => s.ProofSubmissionId == id);

            if (submission == null) return NotFound();
            return Ok(MapToResponse(submission));
        }

        [HttpPut("{id}/approve")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(int id, [FromBody] AdminReviewDto dto)
        {
            var submission = await _context.ProofSubmissions
                .FirstOrDefaultAsync(s => s.ProofSubmissionId == id);

            if (submission == null) return NotFound();
            if (submission.Status != "Pending") return BadRequest("Submission is not pending.");

            submission.Status = "Approved";
            submission.ReviewedAt = DateTime.UtcNow;
            submission.ReviewedByAdminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            submission.AdminNotes = dto.AdminNotes;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}/reject")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reject(int id, [FromBody] AdminReviewDto dto)
        {
            var submission = await _context.ProofSubmissions
                .FirstOrDefaultAsync(s => s.ProofSubmissionId == id);

            if (submission == null) return NotFound();
            if (submission.Status != "Pending") return BadRequest("Submission is not pending.");

            submission.Status = "Rejected";
            submission.ReviewedAt = DateTime.UtcNow;
            submission.ReviewedByAdminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            submission.AdminNotes = dto.AdminNotes;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        private static ProofSubmissionResponseDto MapToResponse(ProofSubmission s) => new()
        {
            ProofSubmissionId = s.ProofSubmissionId,
            UserId = s.UserId,
            ProofType = s.ProofType,
            TargetType = s.TargetType,
            TargetId = s.TargetId,
            FilePath = s.FilePath,
            Status = s.Status,
            SubmittedAt = s.SubmittedAt,
            ReviewedAt = s.ReviewedAt,
            ReviewedByAdminId = s.ReviewedByAdminId,
            AdminNotes = s.AdminNotes
        };
    }
}
