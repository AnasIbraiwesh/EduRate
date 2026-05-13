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
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ReportCreateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var report = new Report
            {
                ReportedByUserId = userId!,
                ReportType = dto.ReportType,
                TargetId = dto.TargetId,
                Reason = dto.Reason,
                Status = "Pending"
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            var response = MapToResponse(report);
            return CreatedAtAction(nameof(GetById), new { id = report.ReportId }, response);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var reports = await _context.Reports
                .Select(r => MapToResponse(r))
                .ToListAsync();

            return Ok(reports);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var report = await _context.Reports
                .FirstOrDefaultAsync(r => r.ReportId == id);

            if (report == null) return NotFound();
            return Ok(MapToResponse(report));
        }

        [HttpPut("{id}/resolve")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Resolve(int id, [FromBody] AdminReviewDto dto)
        {
            var report = await _context.Reports
                .FirstOrDefaultAsync(r => r.ReportId == id);

            if (report == null) return NotFound();
            if (report.Status != "Pending") return BadRequest("Report is not pending.");

            report.Status = "Resolved";
            report.ReviewedAt = DateTime.UtcNow;
            report.ReviewedByAdminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            report.AdminNotes = dto.AdminNotes;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}/dismiss")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Dismiss(int id, [FromBody] AdminReviewDto dto)
        {
            var report = await _context.Reports
                .FirstOrDefaultAsync(r => r.ReportId == id);

            if (report == null) return NotFound();
            if (report.Status != "Pending") return BadRequest("Report is not pending.");

            report.Status = "Dismissed";
            report.ReviewedAt = DateTime.UtcNow;
            report.ReviewedByAdminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            report.AdminNotes = dto.AdminNotes;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        private static ReportResponseDto MapToResponse(Report r) => new()
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
        };
    }
}
