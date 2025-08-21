using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwordloginApi.Data;
using SwordloginApi.Models;

namespace SwordloginApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User loginData)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginData.Username && u.Password == loginData.Password);

            if (user == null)
                return Unauthorized(new { message = "Geçersiz kullanıcı adı veya şifre" });

            return Ok(new { message = "Giriş başarılı", userId = user.Id });
        }
    }
}