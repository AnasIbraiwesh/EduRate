using eduRateSystem.Data;
using eduRateSystem.DTOs;
using eduRateSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eduRateSystem.Controllers
{
    [ApiController]
    [Route("api/universities")]
    public class UniversitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UniversitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
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
        public async Task<IActionResult> GetById(int id)
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

            if (university == null) return NotFound();
            return Ok(university);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] UniversityCreateDto dto)
        {
            var university = new University
            {
                Name = dto.Name,
                Location = dto.Location,
                Description = dto.Description,
                WebsiteUrl = dto.WebsiteUrl
            };

            _context.Universities.Add(university);
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

            return CreatedAtAction(nameof(GetById), new { id = university.UniversityId }, response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UniversityUpdateDto dto)
        {
            var university = await _context.Universities
                .FirstOrDefaultAsync(u => u.UniversityId == id && !u.IsDeleted);

            if (university == null) return NotFound();

            university.Name = dto.Name;
            university.Location = dto.Location;
            university.Description = dto.Description;
            university.WebsiteUrl = dto.WebsiteUrl;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var university = await _context.Universities
                .FirstOrDefaultAsync(u => u.UniversityId == id && !u.IsDeleted);

            if (university == null) return NotFound();

            university.IsDeleted = true;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
