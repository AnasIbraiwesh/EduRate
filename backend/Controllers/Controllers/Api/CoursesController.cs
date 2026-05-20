using System.Xml;
using eduRateSystem.Data;
using eduRateSystem.DTOs.Course;
using eduRateSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eduRateSystem.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CourseResponseDto>>> GetAll(
            [FromQuery] int? universityId,
            [FromQuery] int? professorId)
        {
            var query = _context.Courses
                .Where(c => !c.IsDeleted)
                .AsQueryable();

            if (universityId.HasValue)
            {
                query = query.Where(c => c.UniversityId == universityId.Value);
            }

            if (professorId.HasValue)
            {
                query = query.Where(c => c.ProfessorId == professorId.Value);
            }

            var courses = await query.Select(c => new CourseResponseDto
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

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<CourseResponseDto>> GetById(int id)
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

            if (course == null)
            {
                return NotFound(new { message = "Course Not Found." });
            }

            return Ok(course);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CourseResponseDto>> Create(CreateCourseDto dto)
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

            var professor = await _context.Professors
                .FirstOrDefaultAsync(p => p.ProfessorId == dto.ProfessorId && !p.IsDeleted);

            if (professor == null)
            {
                return BadRequest(new { message = "The Selected Professor Does Not Exist." });
            }

            if (professor.UniversityId != dto.UniversityId)
            {
                return BadRequest(new { message = "The Selected Professor Does Not Belong To The Selected University." });
            }

            var course = new Course
            {
                CourseName = dto.CourseName,
                Department = dto.Department,
                UniversityId = dto.UniversityId,
                ProfessorId = dto.ProfessorId,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            var response = new CourseResponseDto
            {
                CourseName = course.CourseName,
                Department = course.Department,
                UniversityId = course.UniversityId,
                ProfessorId = course.ProfessorId,
                CreatedAt = course.CreatedAt
            };

            return CreatedAtAction(nameof(GetById), new {id = course.CourseId} ,response);
        }
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CourseResponseDto>> Update(int id, UpdateCourseDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.CourseId == id && !c.IsDeleted);

            if (course == null)
            {
                return NotFound(new { message = "Course Not Found." });
            }

            var universityExists = await _context.Universities
                .AnyAsync(u => u.UniversityId == dto.UniversityId && !u.IsDeleted);

            if (!universityExists)
            {
                return BadRequest(new { message = "The Selected University Does Not Exist." });
            }

            var professor = await _context.Professors
                .FirstOrDefaultAsync(p => p.ProfessorId == dto.ProfessorId && !p.IsDeleted);

            if (professor == null)
            {
                return BadRequest(new { message = "The Selected Professor Does Not Exist." });
            }

            if (professor.UniversityId != dto.UniversityId)
            {
                return BadRequest(new
                {
                    message = "The Selected Professor Does Not Belong To The Selected University."
                });
            }

            course.CourseName = dto.CourseName;
            course.Department = dto.Department;
            course.UniversityId = dto.UniversityId;
            course.ProfessorId = dto.ProfessorId;

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

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.CourseId == id && !c.IsDeleted);

            if (course == null)
            {
                return NotFound(new { message = "Course Not Found." });
            }

            course.IsDeleted = true;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Course Deleted Successfully." });
        }
    }
}