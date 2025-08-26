using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SwordloginApi.Data;
using SwordloginApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SwordloginApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public LoginController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        public IActionResult Login([FromBody] User request)
        {
            var user = _context.Users

                .FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);

            if (user == null)
                return Unauthorized(new { message = "Kullanıcı adı veya şifre hatalı" });

            var claims = new[]
    {
        new Claim("id", user.Id.ToString()),
        new Claim(ClaimTypes.Role, user.Role),
        new Claim("username", user.Username)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Jwt:Key")); // config'den alınabilir
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "Jwt:Issuer",
                audience: "Jwt:Audience",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);


            return Ok(new
            {
                message = "Giriş başarılı",
                userId = user.Id,
                username = user.Username,
                role = user.Role, // Role bilgisi doğrudan dönüyor
                token = tokenString // Token çağırıyor

            });

        }
        [Authorize(Roles = "User")]
        [HttpPost("report")]
        public IActionResult SubmitReport([FromBody] PrivateReportDto dto)
        {
            try
            {

                var userIdClaim = User.FindFirst("id")?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                    return Unauthorized(new { message = "JWT içinde kullanıcı kimliği yok" });
                int userId;
                if (!int.TryParse(userIdClaim, out userId))
                    return BadRequest(new { message = "Kullanıcı kimliği sayısal değil" });

                var report = new PrivateReport
                {
                    UserId = userId,
                    Subject = dto.subject,
                    Message = dto.message,
                    CreatedAt = DateTime.Now
                };

                _context.PrivateReports.Add(report);
                _context.SaveChanges();

                return Ok(new { message = "Bildirim başarıyla gönderildi." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Sunucu hatası", error = ex.Message });
            }
        }

            [Authorize(Roles = "Admin")]
        [HttpGet("reports")]
        public IActionResult GetAllReports()
        {
            var reports = _context.PrivateReports
                .Join(_context.Users,
                      report => report.UserId,
                      user => user.Id,
                      (report, user) => new
                      {
                          report.Id,
                          report.Subject,
                          report.Message,
                          report.CreatedAt,
                          Username = user.Username
                      })
                .OrderByDescending(r => r.CreatedAt)
                .ToList();

            return Ok(reports);
        }
    }
}