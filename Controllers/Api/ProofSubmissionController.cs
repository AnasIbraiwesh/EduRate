using eduRateSystem.Data;
using eduRateSystem.DTOs.ProofSubmission;
using eduRateSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eduRateSystem.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProofSubmissionsController : ControllerBase
    {
        private static readonly string[] AllowedTargetTypes = { "University", "Professor" };

        private static readonly string[] AllowedProofTypes =
        {
            "StudentIdCard",
            "TranscriptDocument",
            "AcceptanceLetter",
            "EnrollmentVerification",
            "UniversityEmail"
        };

        private const string Pending = "Pending";
        private const string Approved = "Approved";
        private const string Rejected = "Rejected";

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProofSubmissionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MyProofSubmissionResponseDto>>> GetMine()
        {
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var submissions = await _context.ProofSubmissions
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.SubmittedAt)
                .Select(p => new MyProofSubmissionResponseDto
                {
                    ProofSubmissionId = p.ProofSubmissionId,
                    ProofType = p.ProofType,
                    TargetType = p.TargetType,
                    TargetId = p.TargetId,
                    FilePath = p.FilePath,
                    Status = p.Status,
                    SubmittedAt = p.SubmittedAt,
                    ReviewedAt = p.ReviewedAt,
                    AdminNotes = p.AdminNotes
                })
                .ToListAsync();

            return Ok(submissions);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ProofSubmissionResponseDto>>> GetAll([FromQuery] string? status)
        {
            var query = _context.ProofSubmissions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(p => p.Status == status);
            }

            var submissions = await query
                .OrderByDescending(p => p.SubmittedAt)
                .Select(p => new ProofSubmissionResponseDto
                {
                    ProofSubmissionId = p.ProofSubmissionId,
                    UserId = p.UserId,
                    ProofType = p.ProofType,
                    TargetType = p.TargetType,
                    TargetId = p.TargetId,
                    FilePath = p.FilePath,
                    Status = p.Status,
                    SubmittedAt = p.SubmittedAt,
                    ReviewedAt = p.ReviewedAt,
                    ReviewedByAdminId = p.ReviewedByAdminId,
                    AdminNotes = p.AdminNotes
                })
                .ToListAsync();

            return Ok(submissions);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProofSubmissionResponseDto>> GetById(int id)
        {
            var submission = await _context.ProofSubmissions
                .Where(p => p.ProofSubmissionId == id)
                .Select(p => new ProofSubmissionResponseDto
                {
                    ProofSubmissionId = p.ProofSubmissionId,
                    UserId = p.UserId,
                    ProofType = p.ProofType,
                    TargetType = p.TargetType,
                    TargetId = p.TargetId,
                    FilePath = p.FilePath,
                    Status = p.Status,
                    SubmittedAt = p.SubmittedAt,
                    ReviewedAt = p.ReviewedAt,
                    ReviewedByAdminId = p.ReviewedByAdminId,
                    AdminNotes = p.AdminNotes
                })
                .FirstOrDefaultAsync();

            if (submission == null)
            {
                return NotFound(new { message = "Proof submission not found." });
            }

            return Ok(submission);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<MyProofSubmissionResponseDto>> Create(CreateProofSubmissionDto dto)
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

            if (!AllowedTargetTypes.Contains(dto.TargetType))
            {
                return BadRequest(new { message = "Invalid target type." });
            }

            if (!AllowedProofTypes.Contains(dto.ProofType))
            {
                return BadRequest(new { message = "Invalid proof type." });
            }

            var targetExists = dto.TargetType switch
            {
                "University" => await _context.Universities
                    .AnyAsync(u => u.UniversityId == dto.TargetId && !u.IsDeleted),

                "Professor" => await _context.Professors
                    .AnyAsync(p => p.ProfessorId == dto.TargetId && !p.IsDeleted),

                _ => false
            };

            if (!targetExists)
            {
                return BadRequest(new { message = "The selected target does not exist." });
            }

            var duplicatePending = await _context.ProofSubmissions.AnyAsync(p =>
                p.UserId == userId &&
                p.TargetType == dto.TargetType &&
                p.TargetId == dto.TargetId &&
                p.Status == Pending);

            if (duplicatePending)
            {
                return Conflict(new { message = "You already have a pending submission for this target." });
            }

            var submission = new ProofSubmission
            {
                UserId = userId,
                ProofType = dto.ProofType,
                TargetType = dto.TargetType,
                TargetId = dto.TargetId,
                FilePath = dto.FilePath,
                Status = Pending,
                SubmittedAt = DateTime.UtcNow
            };

            _context.ProofSubmissions.Add(submission);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = submission.ProofSubmissionId }, ToMineDto(submission));
        }

        [HttpPut("{id:int}/review")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProofSubmissionResponseDto>> Review(int id, ReviewProofSubmissionDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (dto.Status != Approved && dto.Status != Rejected)
            {
                return BadRequest(new { message = "Invalid review status." });
            }

            var adminId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(adminId))
            {
                return Unauthorized();
            }

            var submission = await _context.ProofSubmissions
                .FirstOrDefaultAsync(p => p.ProofSubmissionId == id);

            if (submission == null)
            {
                return NotFound(new { message = "Proof submission not found." });
            }

            if (submission.Status != Pending)
            {
                return BadRequest(new { message = "Only pending submissions can be reviewed." });
            }

            submission.Status = dto.Status;
            submission.AdminNotes = dto.AdminNotes;
            submission.ReviewedAt = DateTime.UtcNow;
            submission.ReviewedByAdminId = adminId;

            await _context.SaveChangesAsync();

            return Ok(ToAdminDto(submission));
        }

        private static ProofSubmissionResponseDto ToAdminDto(ProofSubmission submission)
        {
            return new ProofSubmissionResponseDto
            {
                ProofSubmissionId = submission.ProofSubmissionId,
                UserId = submission.UserId,
                ProofType = submission.ProofType,
                TargetType = submission.TargetType,
                TargetId = submission.TargetId,
                FilePath = submission.FilePath,
                Status = submission.Status,
                SubmittedAt = submission.SubmittedAt,
                ReviewedAt = submission.ReviewedAt,
                ReviewedByAdminId = submission.ReviewedByAdminId,
                AdminNotes = submission.AdminNotes
            };
        }

        private static MyProofSubmissionResponseDto ToMineDto(ProofSubmission submission)
        {
            return new MyProofSubmissionResponseDto
            {
                ProofSubmissionId = submission.ProofSubmissionId,
                ProofType = submission.ProofType,
                TargetType = submission.TargetType,
                TargetId = submission.TargetId,
                FilePath = submission.FilePath,
                Status = submission.Status,
                SubmittedAt = submission.SubmittedAt,
                ReviewedAt = submission.ReviewedAt,
                AdminNotes = submission.AdminNotes
            };
        }
    }
}