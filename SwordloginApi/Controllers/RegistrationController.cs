using Microsoft.AspNetCore.Mvc;
using SwordloginApi.Data;
using SwordloginApi.Models;
using SwordloginApi.Helpers;

namespace SwordloginApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegistrationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("request")]
        public async Task<IActionResult> CreateRequest([FromBody] RegistrationRequestDto dto)
        {
            var hashedPassword = PasswordHasher.Hash(dto.Password);

            var request = new RegistrationRequest
            {
                Email = dto.Email,
                Username = dto.Username,
                PasswordHash = hashedPassword,
                Token = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                IsApproved = false
            };

            _context.RegistrationRequests.Add(request);
            await _context.SaveChangesAsync();

            return Ok("Kayıt talebi başarıyla oluşturuldu.");
        }
    }
}