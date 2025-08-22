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

            // Role bilgisi bit tipinde: 0 = normal, 1 = admin
            bool isAdmin = user.Role == 1;

            return Ok(new
            {
                message = "Giriş başarılı",
                userId = user.Id,
                username = user.Username,
                role = isAdmin // Unity tarafında bool olarak karşılanabilir
            });

        }

    }
}