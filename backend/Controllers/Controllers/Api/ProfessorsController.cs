using eduRateSystem.Data;
using eduRateSystem.DTOs.Professor;
using eduRateSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace eduRateSystem.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProfessorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProfessorResponseDto>>> GetAll([FromQuery] int? universityId)
        {
            var query = _context.Professors
                .Where(p => !p.IsDeleted)
                .AsQueryable();

            if (universityId.HasValue)
            {
                query = query.Where(p => p.UniversityId == universityId.Value);
            }

            var professors = await query
                .Include(p => p.University)
                .Select(p => new ProfessorResponseDto
                {
                    ProfessorId = p.ProfessorId,
                    FullName = p.FullName,
                    UniversityId = p.UniversityId,
                    UniversityName = p.University != null ? p.University.Name : string.Empty,
                    Department = p.Department,
                    Specialization = p.Specialization,
                    CreatedAt = p.CreatedAt,
                    OverallRating = p.ProfessorReviews.Any(r => !r.IsDeleted)
                        ? (2 * p.ProfessorReviews.Where(r => !r.IsDeleted).Average(r => (double)r.Friendliness)
                           + (6 - p.ProfessorReviews.Where(r => !r.IsDeleted).Average(r => (double)r.Difficulty))
                           + (6 - p.ProfessorReviews.Where(r => !r.IsDeleted).Average(r => (double)r.Workload))) / 4.0
                        : 0,
                    AvgDifficulty = p.ProfessorReviews.Any(r => !r.IsDeleted)
                        ? p.ProfessorReviews.Where(r => !r.IsDeleted).Average(r => (double)r.Difficulty)
                        : 0,
                    AvgWorkload = p.ProfessorReviews.Any(r => !r.IsDeleted)
                        ? p.ProfessorReviews.Where(r => !r.IsDeleted).Average(r => (double)r.Workload)
                        : 0,
                    AvgFriendliness = p.ProfessorReviews.Any(r => !r.IsDeleted)
                        ? p.ProfessorReviews.Where(r => !r.IsDeleted).Average(r => (double)r.Friendliness)
                        : 0,
                    WouldTakeAgainPercent = p.ProfessorReviews.Any(r => !r.IsDeleted && r.WouldTakeAgain != null)
                        ? (p.ProfessorReviews.Count(r => !r.IsDeleted && r.WouldTakeAgain == true) * 100
                           + p.ProfessorReviews.Count(r => !r.IsDeleted && r.WouldTakeAgain != null) / 2)
                          / p.ProfessorReviews.Count(r => !r.IsDeleted && r.WouldTakeAgain != null)
                        : 0,
                    TotalReviews = p.ProfessorReviews.Count(r => !r.IsDeleted),
                    CoursesJson = p.CoursesJson
                })
                .ToListAsync();

            return Ok(professors);

        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProfessorResponseDto>> GetById(int id)
        {
            var professor = await _context.Professors
                .Include(p => p.University)
                .Where(p => p.ProfessorId == id && !p.IsDeleted)
                .Select(p => new ProfessorResponseDto
                {
                    ProfessorId = p.ProfessorId,
                    FullName = p.FullName,
                    UniversityId = p.UniversityId,
                    UniversityName = p.University != null ? p.University.Name : string.Empty,
                    Department = p.Department,
                    Specialization = p.Specialization,
                    CreatedAt = p.CreatedAt,
                    OverallRating = p.ProfessorReviews.Any(r => !r.IsDeleted)
                        ? (2 * p.ProfessorReviews.Where(r => !r.IsDeleted).Average(r => (double)r.Friendliness)
                           + (6 - p.ProfessorReviews.Where(r => !r.IsDeleted).Average(r => (double)r.Difficulty))
                           + (6 - p.ProfessorReviews.Where(r => !r.IsDeleted).Average(r => (double)r.Workload))) / 4.0
                        : 0,
                    AvgDifficulty = p.ProfessorReviews.Any(r => !r.IsDeleted)
                        ? p.ProfessorReviews.Where(r => !r.IsDeleted).Average(r => (double)r.Difficulty)
                        : 0,
                    AvgWorkload = p.ProfessorReviews.Any(r => !r.IsDeleted)
                        ? p.ProfessorReviews.Where(r => !r.IsDeleted).Average(r => (double)r.Workload)
                        : 0,
                    AvgFriendliness = p.ProfessorReviews.Any(r => !r.IsDeleted)
                        ? p.ProfessorReviews.Where(r => !r.IsDeleted).Average(r => (double)r.Friendliness)
                        : 0,
                    WouldTakeAgainPercent = p.ProfessorReviews.Any(r => !r.IsDeleted && r.WouldTakeAgain != null)
                        ? (p.ProfessorReviews.Count(r => !r.IsDeleted && r.WouldTakeAgain == true) * 100
                           + p.ProfessorReviews.Count(r => !r.IsDeleted && r.WouldTakeAgain != null) / 2)
                          / p.ProfessorReviews.Count(r => !r.IsDeleted && r.WouldTakeAgain != null)
                        : 0,
                    TotalReviews = p.ProfessorReviews.Count(r => !r.IsDeleted),
                    CoursesJson = p.CoursesJson
                })
                .FirstOrDefaultAsync();

            if (professor == null)
            {
                return NotFound(new { message = "Professor not found." });
            }

            return Ok(professor);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProfessorResponseDto>> Create(CreateProfessorDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var universityExists = await _context.Universities
                .AnyAsync(u => u.UniversityId == dto.UniversityId && !u.IsDeleted);

            if (!universityExists)
            {
                return BadRequest(new { message = "The Selected University Does Not Exist." });
            }

            var professor = new Professor
            {
                FullName = dto.FullName,
                UniversityId = dto.UniversityId,
                Department = dto.Department,
                Specialization = dto.Specialization,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Professors.Add(professor);
            await _context.SaveChangesAsync();

            var response = new ProfessorResponseDto
            {
                ProfessorId = professor.ProfessorId,
                FullName = professor.FullName,
                UniversityId = professor.UniversityId,
                Department = professor.Department,
                Specialization = professor.Specialization,
                CreatedAt = professor.CreatedAt
            };

            return CreatedAtAction(nameof(GetById), new { id = professor.ProfessorId }, response);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProfessorResponseDto>> Update(int id, UpdateProfessorDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var professor = await _context.Professors
                .FirstOrDefaultAsync(p => p.ProfessorId == id && !p.IsDeleted);

            if (professor == null)
            {
                return NotFound(new { message = "Professor not found." });
            }

            var universityExists = await _context.Universities
                .AnyAsync(u => u.UniversityId == dto.UniversityId && !u.IsDeleted);

            if (!universityExists)
            {
                return BadRequest(new { message = "The Selected University Does Not Exist." });
            }

            professor.FullName = dto.FullName;
            professor.UniversityId = dto.UniversityId;
            professor.Department = dto.Department;
            professor.Specialization = dto.Specialization;

            await _context.SaveChangesAsync();

            var response = new ProfessorResponseDto
            {
                ProfessorId = professor.ProfessorId,
                FullName = professor.FullName,
                UniversityId = professor.UniversityId,
                Department = professor.Department,
                Specialization = professor.Specialization,
                CreatedAt = professor.CreatedAt
            };

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var professor = await _context.Professors
                .FirstOrDefaultAsync(p => p.ProfessorId == id && !p.IsDeleted);

            if (professor == null)
            {
                return NotFound(new { message = "Professor not found." });
            }

            professor.IsDeleted = true;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Professor deleted successfully." });
        }
    }
}