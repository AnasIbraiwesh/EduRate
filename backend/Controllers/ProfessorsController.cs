using eduRateSystem.Data;
using eduRateSystem.DTOs;
using eduRateSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eduRateSystem.Controllers
{
    [ApiController]
    [Route("api/professors")]
    public class ProfessorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProfessorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var professors = await _context.Professors
                .Where(p => !p.IsDeleted)
                .Select(p => new ProfessorResponseDto
                {
                    ProfessorId = p.ProfessorId,
                    FullName = p.FullName,
                    UniversityId = p.UniversityId,
                    Department = p.Department,
                    Specialization = p.Specialization,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();

            return Ok(professors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var professor = await _context.Professors
                .Where(p => p.ProfessorId == id && !p.IsDeleted)
                .Select(p => new ProfessorResponseDto
                {
                    ProfessorId = p.ProfessorId,
                    FullName = p.FullName,
                    UniversityId = p.UniversityId,
                    Department = p.Department,
                    Specialization = p.Specialization,
                    CreatedAt = p.CreatedAt
                })
                .FirstOrDefaultAsync();

            if (professor == null) return NotFound();
            return Ok(professor);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ProfessorCreateDto dto)
        {
            var universityExists = await _context.Universities
                .AnyAsync(u => u.UniversityId == dto.UniversityId && !u.IsDeleted);

            if (!universityExists) return NotFound("University not found.");

            var professor = new Professor
            {
                FullName = dto.FullName,
                UniversityId = dto.UniversityId,
                Department = dto.Department,
                Specialization = dto.Specialization
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

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] ProfessorUpdateDto dto)
        {
            var professor = await _context.Professors
                .FirstOrDefaultAsync(p => p.ProfessorId == id && !p.IsDeleted);

            if (professor == null) return NotFound();

            var universityExists = await _context.Universities
                .AnyAsync(u => u.UniversityId == dto.UniversityId && !u.IsDeleted);

            if (!universityExists) return NotFound("University not found.");

            professor.FullName = dto.FullName;
            professor.UniversityId = dto.UniversityId;
            professor.Department = dto.Department;
            professor.Specialization = dto.Specialization;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var professor = await _context.Professors
                .FirstOrDefaultAsync(p => p.ProfessorId == id && !p.IsDeleted);

            if (professor == null) return NotFound();

            professor.IsDeleted = true;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
