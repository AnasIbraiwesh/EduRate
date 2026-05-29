using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using eduRateSystem.Data;
using eduRateSystem.DTOs.Auth;
using eduRateSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace eduRateSystem.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration config,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _context = context;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthUserDto>> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                return Conflict(new { message = "An account with this email already exists." });

            var user = new ApplicationUser
            {
                FullName = dto.FullName,
                UserName = dto.Email,
                Email = dto.Email,
                IsActive = true,
                IsVerifiedStudent = dto.Email.EndsWith(".edu.jo", StringComparison.OrdinalIgnoreCase),
                UniversityId = dto.UniversityId,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors.Select(e => e.Description) });

            await _userManager.AddToRoleAsync(user, "Student");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, "Student")
            };
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return Ok(new
            {
                id = user.Id,
                fullName = user.FullName,
                email = user.Email,
                role = "Student",
                isVerifiedStudent = user.IsVerifiedStudent,
                universityId = user.UniversityId,
                universityChangedAt = user.UniversityChangedAt,
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !user.IsActive)
                return Unauthorized(new { message = "Invalid email or password." });

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                return Unauthorized(new { message = "Invalid email or password." });

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? string.Empty;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, role)
            };
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return Ok(new
            {
                id = user.Id,
                fullName = user.FullName,
                email = user.Email,
                role,
                isVerifiedStudent = user.IsVerifiedStudent,
                universityId = user.UniversityId,
                universityChangedAt = user.UniversityChangedAt,
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        [HttpPost("logout")]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            return Ok(new { message = "Logged out." });
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Me()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId!);
            if (user == null) return Unauthorized();

            var roles = await _userManager.GetRolesAsync(user);
            return Ok(new
            {
                id = user.Id,
                fullName = user.FullName,
                email = user.Email,
                role = roles.FirstOrDefault() ?? string.Empty,
                isVerifiedStudent = user.IsVerifiedStudent,
                universityId = user.UniversityId,
                universityChangedAt = user.UniversityChangedAt
            });
        }

        [HttpPut("me/university")]
        [Authorize]
        public async Task<IActionResult> UpdateUniversity([FromBody] UpdateUniversityDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId!);
            if (user == null) return Unauthorized();

            var universityExists = await _context.Universities
                .AnyAsync(u => u.UniversityId == dto.UniversityId && !u.IsDeleted);
            if (!universityExists)
                return BadRequest(new { message = "The selected university does not exist." });

            if (user.UniversityId == dto.UniversityId)
                return Ok(new { universityId = user.UniversityId, universityChangedAt = user.UniversityChangedAt });

            if (user.UniversityChangedAt.HasValue &&
                GetCurrentSemester(user.UniversityChangedAt.Value) == GetCurrentSemester(DateTime.UtcNow))
                return Conflict(new { message = "You can only change your university once per semester." });

            user.UniversityId = dto.UniversityId;
            user.UniversityChangedAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return Ok(new { universityId = user.UniversityId, universityChangedAt = user.UniversityChangedAt });
        }

        private static string GetCurrentSemester(DateTime date)
        {
            var month = date.Month;
            var season = month >= 9 ? "Fall" : month >= 7 ? "Summer" : "Spring";
            return $"{season}-{date.Year}";
        }
    }
}
