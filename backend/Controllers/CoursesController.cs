using eduRateSystem.Data;
using eduRateSystem.DTOs;
using eduRateSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eduRateSystem.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _context.Courses
                .Where(c => !c.IsDeleted)
                .Select(c => new CourseResponseDto
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    Department = c.Department,
                    UniversityId = c.UniversityId,
                    ProfessorId = c.ProfessorId,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync();

            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _context.Courses
                .Where(c => c.CourseId == id && !c.IsDeleted)
                .Select(c => new CourseResponseDto
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    Department = c.Department,
                    UniversityId = c.UniversityId,
                    ProfessorId = c.ProfessorId,
                    CreatedAt = c.CreatedAt
                })
                .FirstOrDefaultAsync();

            if (course == null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CourseCreateDto dto)
        {
            var universityExists = await _context.Universities
                .AnyAsync(u => u.UniversityId == dto.UniversityId && !u.IsDeleted);

            if (!universityExists) return NotFound("University not found.");

            var professorExists = await _context.Professors
                .AnyAsync(p => p.ProfessorId == dto.ProfessorId && !p.IsDeleted);

            if (!professorExists) return NotFound("Professor not found.");

            var course = new Course
            {
                CourseName = dto.CourseName,
                Department = dto.Department,
                UniversityId = dto.UniversityId,
                ProfessorId = dto.ProfessorId
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            var response = new CourseResponseDto
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                Department = course.Department,
                UniversityId = course.UniversityId,
                ProfessorId = course.ProfessorId,
                CreatedAt = course.CreatedAt
            };

            return CreatedAtAction(nameof(GetById), new { id = course.CourseId }, response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] CourseUpdateDto dto)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.CourseId == id && !c.IsDeleted);

            if (course == null) return NotFound();

            var universityExists = await _context.Universities
                .AnyAsync(u => u.UniversityId == dto.UniversityId && !u.IsDeleted);

            if (!universityExists) return NotFound("University not found.");

            var professorExists = await _context.Professors
                .AnyAsync(p => p.ProfessorId == dto.ProfessorId && !p.IsDeleted);

            if (!professorExists) return NotFound("Professor not found.");

            course.CourseName = dto.CourseName;
            course.Department = dto.Department;
            course.UniversityId = dto.UniversityId;
            course.ProfessorId = dto.ProfessorId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.CourseId == id && !c.IsDeleted);

            if (course == null) return NotFound();

            course.IsDeleted = true;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
