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
    public async Task<ActionResult<CreateUserResponse>> CreateUserAsync([FromBody] CreateUserRequest request)
    {
        var response = await _context.CreateUserAsync(request);

        return response.ErrorCode switch
        {
            DbErrorCode.UserCreated => new CreateUserResponse(response.UserCredential!.Email, true),
            DbErrorCode.UserAlreadyExists => Conflict("User already exists"),
            _ => NotFound()
        };
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetUserCredentialsResponse>> GetUserCredentials(int id)
    {
        var response = await _context.GetUserByIdAsync(id);

        return response.ErrorCode switch
        {
            DbErrorCode.UserNotFound => NotFound(),
            DbErrorCode.Ok => new GetUserCredentialsResponse(response.UserCredential!),
            _ => NotFound()
        };
    }

    [HttpPost("verify")]
    public async Task<ActionResult<VerifyPasswordResponse>> VerifyPassword([FromBody] VerifyPasswordRequest request)
    {
        var response = await _context.VerifyPasswordAsync(request);

        return response.DbErrorCode switch
        {
            DbErrorCode.UserNotFound => NotFound(),
            DbErrorCode.Ok => new VerifyPasswordResponse(response.Email!, IsLoggedIn: true),
            DbErrorCode.WrongPassword => Unauthorized(),
            _ => NotFound("Unknown error")
        };
    }
}
