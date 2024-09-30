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


}