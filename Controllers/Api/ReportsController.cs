using eduRateSystem.Data;

using eduRateSystem.DTOs.Report;

using eduRateSystem.Models;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

namespace eduRateSystem.Controllers.Api

{

    [ApiController]

    [Route("api/[controller]")]

    public class ReportsController : ControllerBase

    {

        private const string UniversityReviewType = "UniversityReview";

        private const string ProfessorReviewType = "ProfessorReview";

        private const string Pending = "Pending";

        private const string Resolved = "Resolved";

        private const string Dismissed = "Dismissed";

        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public ReportsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)

        {

            _context = context;

            _userManager = userManager;

        }

        [HttpGet]

        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<IEnumerable<ReportResponseDto>>> GetAll([FromQuery] string? status)

        {

            var query = _context.Reports.AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))

            {

                query = query.Where(r => r.Status == status);

            }

            var reports = await query

                .OrderByDescending(r => r.CreatedAt)

                .Select(r => new ReportResponseDto

                {

                    ReportId = r.ReportId,

                    ReportedByUserId = r.ReportedByUserId,

                    ReportType = r.ReportType,

                    TargetId = r.TargetId,

                    Reason = r.Reason,

                    Status = r.Status,

                    CreatedAt = r.CreatedAt,

                    ReviewedByAdminId = r.ReviewedByAdminId,

                    ReviewedAt = r.ReviewedAt,

                    AdminNotes = r.AdminNotes

                })

                .ToListAsync();

            return Ok(reports);

        }

        [HttpGet("{id:int}")]

        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<ReportResponseDto>> GetById(int id)

        {

            var report = await _context.Reports

                .Where(r => r.ReportId == id)

                .Select(r => new ReportResponseDto

                {

                    ReportId = r.ReportId,

                    ReportedByUserId = r.ReportedByUserId,

                    ReportType = r.ReportType,

                    TargetId = r.TargetId,

                    Reason = r.Reason,

                    Status = r.Status,

                    CreatedAt = r.CreatedAt,

                    ReviewedByAdminId = r.ReviewedByAdminId,

                    ReviewedAt = r.ReviewedAt,

                    AdminNotes = r.AdminNotes

                })

                .FirstOrDefaultAsync();

            if (report == null)

            {

                return NotFound(new { message = "Report not found." });

            }

            return Ok(report);

        }

        [HttpGet("me")]

        [Authorize]

        public async Task<ActionResult<IEnumerable<MyReportResponseDto>>> GetMine()

        {

            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))

            {

                return Unauthorized();

            }

            var reports = await _context.Reports

                .Where(r => r.ReportedByUserId == userId)

                .OrderByDescending(r => r.CreatedAt)

                .Select(r => new MyReportResponseDto

                {

                    ReportId = r.ReportId,

                    ReportType = r.ReportType,

                    TargetId = r.TargetId,

                    Reason = r.Reason,

                    Status = r.Status,

                    CreatedAt = r.CreatedAt,

                    ReviewedAt = r.ReviewedAt,

                    AdminNotes = r.AdminNotes

                })

                .ToListAsync();

            return Ok(reports);

        }

        [HttpPost]

        [Authorize]

        public async Task<ActionResult<MyReportResponseDto>> Create(CreateReportDto dto)

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

            if (dto.ReportType != UniversityReviewType && dto.ReportType != ProfessorReviewType)

            {

                return BadRequest(new { message = "Invalid report type." });

            }

            var targetExists = dto.ReportType switch

            {

                UniversityReviewType => await _context.UniversityReviews

                    .AnyAsync(r => r.UniversityReviewId == dto.TargetId && !r.IsDeleted),

                ProfessorReviewType => await _context.ProfessorReviews

                    .AnyAsync(r => r.ProfessorReviewId == dto.TargetId && !r.IsDeleted),

                _ => false

            };

            if (!targetExists)

            {

                return BadRequest(new { message = "The selected review does not exist." });

            }

            var duplicatePending = await _context.Reports.AnyAsync(r =>

                r.ReportedByUserId == userId &&

                r.ReportType == dto.ReportType &&

                r.TargetId == dto.TargetId &&

                r.Status == Pending);

            if (duplicatePending)

            {

                return Conflict(new { message = "You already have a pending report for this review." });

            }

            var report = new Report

            {

                ReportedByUserId = userId,

                ReportType = dto.ReportType,

                TargetId = dto.TargetId,

                Reason = dto.Reason,

                Status = Pending,

                CreatedAt = DateTime.UtcNow

            };

            _context.Reports.Add(report);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = report.ReportId }, ToMyDto(report));

        }

        [HttpPut("{id:int}/review")]

        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<ReportResponseDto>> Review(int id, ReviewReportDto dto)

        {

            if (!ModelState.IsValid)

            {

                return BadRequest(ModelState);

            }

            if (dto.Status != Resolved && dto.Status != Dismissed)

            {

                return BadRequest(new { message = "Invalid review status." });

            }

            if (dto.Status == Dismissed && dto.RemoveTargetReview)

            {

                return BadRequest(new

                {

                    message = "You can't remove the target review when dismissing the report."

                });

            }

            var adminId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(adminId))

            {

                return Unauthorized();

            }

            var report = await _context.Reports.FirstOrDefaultAsync(r => r.ReportId == id);

            if (report == null)

            {

                return NotFound(new { message = "Report not found." });

            }

            if (report.Status != Pending)

            {

                return BadRequest(new { message = "Only pending reports can be reviewed." });

            }

            if (dto.Status == Resolved && dto.RemoveTargetReview)

            {

                var removed = await RemoveReportedReview(report);

                if (!removed)

                {

                    return BadRequest(new { message = "Unable to remove the reported review." });

                }

            }

            report.Status = dto.Status;

            report.AdminNotes = dto.AdminNotes;

            report.ReviewedAt = DateTime.UtcNow;

            report.ReviewedByAdminId = adminId;

            await _context.SaveChangesAsync();

            return Ok(ToAdminDto(report));

        }

        private async Task<bool> RemoveReportedReview(Report report)

        {

            switch (report.ReportType)

            {

                case UniversityReviewType:

                    {

                        var review = await _context.UniversityReviews

                            .FirstOrDefaultAsync(r => r.UniversityReviewId == report.TargetId && !r.IsDeleted);

                        if (review != null)

                        {

                            review.IsDeleted = true;

                        }

                        return true;

                    }

                case ProfessorReviewType:

                    {

                        var review = await _context.ProfessorReviews

                            .FirstOrDefaultAsync(r => r.ProfessorReviewId == report.TargetId && !r.IsDeleted);

                        if (review != null)

                        {

                            review.IsDeleted = true;

                        }

                        return true;

                    }

                default:

                    return false;

            }

        }

        private static ReportResponseDto ToAdminDto(Report report)

        {

            return new ReportResponseDto

            {

                ReportId = report.ReportId,

                ReportedByUserId = report.ReportedByUserId,

                ReportType = report.ReportType,

                TargetId = report.TargetId,

                Reason = report.Reason,

                Status = report.Status,

                CreatedAt = report.CreatedAt,

                ReviewedByAdminId = report.ReviewedByAdminId,

                ReviewedAt = report.ReviewedAt,

                AdminNotes = report.AdminNotes

            };

        }

        private static MyReportResponseDto ToMyDto(Report report)

        {

            return new MyReportResponseDto

            {

                ReportId = report.ReportId,

                ReportType = report.ReportType,

                TargetId = report.TargetId,

                Reason = report.Reason,

                Status = report.Status,

                CreatedAt = report.CreatedAt,

                ReviewedAt = report.ReviewedAt,

                AdminNotes = report.AdminNotes

            };

        }

    }

}