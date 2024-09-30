using Microsoft.AspNetCore.Mvc;
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
        var response = await _context.GetUserByIdAsync(id);

        return response.ErrorCode switch
        {
            DbErrorCode.UserNotFound => NotFound(),
            DbErrorCode.Ok => Ok(response.UserCredential),
            _ => NotFound()
        };
    }

    [HttpPost("verify")]
    public async Task<IActionResult> VerifyPassword([FromBody] VerifyPasswordRequest request)
    {
        var response = await _context.VerifyPasswordAsync(request);

        return response.DbErrorCode switch
        {
            DbErrorCode.UserNotFound => NotFound(),
            DbErrorCode.Ok => Ok(new { response.Email }),
            DbErrorCode.WrongPassword => Unauthorized(),
            _ => NotFound("Unknown error")
        };
    }
}
