using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SwordloginApi.Data;
using SwordloginApi.Models;

namespace SwordloginApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login([FromBody] User request)
        {
            var user = _context.Users

                .FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);

            if (user == null)
                return Unauthorized(new { message = "Kullanıcı adı veya şifre hatalı" });

            return Ok(new
            {
                message = "Giriş başarılı",
                userId = user.Id,
                username = user.Username,
                role = user.Role // Role bilgisi doğrudan dönüyor
            });

        }
        [Authorize(Roles = "User")]
        [HttpPost("report")]
        public IActionResult SubmitReport([FromBody] PrivateReportDto dto)
        {
            var userIdClaim = User.FindFirst("id")?.Value;
            if (userIdClaim == null)
                return Unauthorized(new { message = "Kullanıcı kimliği doğrulanamadı." });

            var report = new PrivateReport
            {
                UserId = int.Parse(userIdClaim),
                Subject = dto.Subject,
                Message = dto.Message,
                CreatedAt = DateTime.Now
            };

            _context.PrivateReports.Add(report);
            _context.SaveChanges();

            return Ok(new { message = "Bildirim başarıyla gönderildi." });
        }
    }
}