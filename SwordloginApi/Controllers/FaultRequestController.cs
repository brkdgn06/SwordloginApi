using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwordloginApi.Data;
using SwordloginApi.Models;
using System.Security.Claims;

[Authorize(Roles = "User,Admin")]
[ApiController]
[Route("api/[controller]")]
public class FaultRequestController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;

    public FaultRequestController(AppDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRequest([FromBody] FaultRequest model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        model.RequestedByUserId = userId;
        _context.FaultRequests.Add(model);
        await _context.SaveChangesAsync();
        return Ok(model);
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllRequests()
    {
        var requests = await _context.FaultRequests.ToListAsync();
        return Ok(requests);
    }
}