using eduRateSystem.Data;
using eduRateSystem.DTOs.University;
using eduRateSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eduRateSystem.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UniversitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UniversityResponseDto>>> GetAllUniversities()
        {
            var universities = await _context.Universities
             .Where(u => !u.IsDeleted)
             .Select(u => new UniversityResponseDto
             {
                 UniversityId = u.UniversityId,
                 Name = u.Name,
                 Location = u.Location,
                 Description = u.Description,
                 WebsiteUrl = u.WebsiteUrl,
                 CreatedAt = u.CreatedAt
             })
             .ToListAsync();

            return Ok(universities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UniversityResponseDto>> GetUniversityById(int id)
        {
            var university = await _context.Universities
                .Where(u => u.UniversityId == id && !u.IsDeleted)
                .Select(u => new UniversityResponseDto
                {
                    UniversityId = u.UniversityId,
                    Name = u.Name,
                    Location = u.Location,
                    Description = u.Description,
                    WebsiteUrl = u.WebsiteUrl,
                    CreatedAt = u.CreatedAt
                })
                .FirstOrDefaultAsync();

            if (university == null)
            {
                return NotFound(new { message = "University not found." });
            }

            return Ok(university);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UniversityResponseDto>> CreateUniversity(CreateUniversityDto dto)
        {
            var university = new University
            {
                Name = dto.Name,
                Location = dto.Location,
                Description = dto.Description,
                WebsiteUrl = dto.WebsiteUrl,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Universities.Add(university);
            await _context.SaveChangesAsync();

            var response = new UniversityResponseDto
            {
                UniversityId = university.UniversityId,
                Name = dto.Name,
                Location = dto.Location,
                Description = dto.Description,
                WebsiteUrl = dto.WebsiteUrl,
                CreatedAt = university.CreatedAt
            };

            return CreatedAtAction(nameof(GetUniversityById), new { id = university.UniversityId }, response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UniversityResponseDto>> UpdateUniversity(int id, UpdateUniversityDto dto)
        {
            var university = await _context.Universities
                .FirstOrDefaultAsync(u => u.UniversityId == id && !u.IsDeleted);

            if (university == null)
            {
                return NotFound(new { message = "University Not Found" });
            }

            university.Name = dto.Name;
            university.Location = dto.Location;
            university.Description = dto.Description;
            university.WebsiteUrl = dto.WebsiteUrl;

            await _context.SaveChangesAsync();

            var response = new UniversityResponseDto
            {
                UniversityId = university.UniversityId,
                Name = university.Name,
                Location = university.Location,
                Description = university.Description,
                WebsiteUrl = university.WebsiteUrl,
                CreatedAt = university.CreatedAt
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUniversity(int id)
        {
            var university = await _context.Universities
                .FirstOrDefaultAsync(university => university.UniversityId == id && !university.IsDeleted);

            if (university == null)
            {
                return NotFound(new { message = "University no found." });
            }
            university.IsDeleted = true;

            await _context.SaveChangesAsync();

            return Ok(new { message = "University Deleted Successfully" });
        }
    }
}