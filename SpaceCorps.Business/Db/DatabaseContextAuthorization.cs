using Microsoft.EntityFrameworkCore;
using SpaceCorps.Business.Authorization;
using SpaceCorps.Business.Dto.Authorization;
using SpaceCorps.Business.Dto.Db;

namespace SpaceCorps.Business.Db;

public partial class DatabaseContext
{
    public async Task<DbUserCredentialsResponse> CreateUserAsync(CreateUserRequest request)
    {
        if (UserCredentials.Any(u => u.Email == request.Email))
        {
            return new DbUserCredentialsResponse(DbErrorCode.UserAlreadyExists, null);
        }

        var userCredential = new UserCredential(request.Email, request.Password);
        UserCredentials.Add(userCredential);
        await SaveChangesAsync();

        return new DbUserCredentialsResponse(DbErrorCode.UserCreated, userCredential);
    }

    public async Task<DbUserVerifyPasswordResponse> VerifyPasswordAsync(VerifyPasswordRequest request)
    {
        if (!UserCredentials.Any(u => u.Email == request.Email))
        {
            return new DbUserVerifyPasswordResponse(DbErrorCode.UserNotFound, null);
        }

        var userCredential = await UserCredentials.FirstAsync(u => u.Email == request.Email);

        if (!userCredential.VerifyPassword(request.Password))
        {
            return new DbUserVerifyPasswordResponse(DbErrorCode.WrongPassword, userCredential.Email);
        }

        return new DbUserVerifyPasswordResponse(DbErrorCode.Ok, userCredential.Email);

    }

    public async Task<DbUserCredentialsResponse> GetUserByIdAsync(int id)
    {
        var userCredential = await UserCredentials.FindAsync(id);
        return userCredential == null
            ? new DbUserCredentialsResponse(DbErrorCode.UserNotFound, null)
            : new DbUserCredentialsResponse(DbErrorCode.Ok, userCredential);
    }
}