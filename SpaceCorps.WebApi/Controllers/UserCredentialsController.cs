using Microsoft.AspNetCore.Mvc;
using SpaceCorps.Business.Db;
using SpaceCorps.Business.Dto.Authorization;

namespace SpaceCorps.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserCredentialsController(DatabaseContext context) : ControllerBase
{
    [HttpPost("create")]
    public async Task<ActionResult<CreateUserResponse>> CreateUserAsync([FromBody] CreateUserRequest request)
    {
        var response = await context.CreateUserAsync(request);

        return response.ErrorCode switch
        {
            DbErrorCode.UserCreated => Created("api/UserCredentials/create", new CreateUserResponse(response.UserCredential!.Email, IsCreated: true)),
            DbErrorCode.UserAlreadyExists => Conflict("User already exists"),
            _ => NotFound()
        };
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetUserCredentialsResponse>> GetUserCredentialsAsync(int id)
    {
        var response = await context.GetUserByIdAsync(id);

        return response.ErrorCode switch
        {
            DbErrorCode.UserNotFound => NotFound(),
            DbErrorCode.Ok => new GetUserCredentialsResponse(response.UserCredential!),
            _ => NotFound()
        };
    }

    [HttpPost("verify")]
    public async Task<ActionResult<VerifyPasswordResponse>> VerifyPasswordAsync([FromBody] VerifyPasswordRequest request)
    {
        var response = await context.VerifyPasswordAsync(request);

        return response.DbErrorCode switch
        {
            DbErrorCode.UserNotFound => NotFound(),
            DbErrorCode.Ok => new VerifyPasswordResponse(response.Email!, IsLoggedIn: true),
            DbErrorCode.WrongPassword => Unauthorized(),
            _ => NotFound("Unknown error")
        };
    }
}
