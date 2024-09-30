using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceCorps.Business.Db;
using SpaceCorps.Business.Dto.Authorization;

namespace SpaceCorps.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserCredentialsController(DatabaseContext context) : ControllerBase
{
    private readonly DatabaseContext _context = context;

    [HttpPost("create")]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
    {
        var response = await _context.CreateUserAsync(request);

        return response.ErrorCode switch
        {
            DbErrorCode.UserCreated => CreatedAtAction(nameof(GetUser), new { id = response.UserCredential!.Id }, response.UserCredential),
            DbErrorCode.UserAlreadyExists => Conflict("User already exists"),
            _ => NotFound()
        };
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
