using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceCorps.Business.Authorization;
using SpaceCorps.Business.Db;

namespace SpaceCorps.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserCredentialsController(DatabaseContext context) : ControllerBase
{
    private readonly DatabaseContext _context = context;

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        if (_context.UserCredentials.Any(u => u.Email == request.Email))
        {
            return BadRequest("User with this email already exists.");
        }

        var userCredential = new UserCredential(request.Email, request.Password);
        _context.UserCredentials.Add(userCredential);
        await _context.SaveChangesAsync();

        return Ok(new { userCredential.Id, userCredential.Email });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var userCredential = await _context.UserCredentials.FindAsync(id);
        if (userCredential == null)
        {
            return NotFound();
        }

        return Ok(new { userCredential.Id, userCredential.Email });
    }

    [HttpPost("verify")]
    public async Task<IActionResult> VerifyPassword([FromBody] VerifyPasswordRequest request)
    {
        var userCredential = await _context.UserCredentials
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (userCredential == null || !userCredential.VerifyPassword(request.Password))
        {
            return Unauthorized();
        }

        return Ok(new { userCredential.Id, userCredential.Email });
    }
}

public class CreateUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class VerifyPasswordRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
