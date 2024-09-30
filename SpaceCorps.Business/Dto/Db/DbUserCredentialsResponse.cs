using SpaceCorps.Business.Authorization;
using SpaceCorps.Business.Db;

namespace SpaceCorps.Business.Dto.Db;

public record DbUserCredentialsResponse
{
    public DbErrorCode ErrorCode { get; init; }
    public UserCredential? UserCredential { get; init; }

    public DbUserCredentialsResponse(DbErrorCode errorCode, UserCredential? userCredential)
    {
        ErrorCode = errorCode;
        UserCredential = userCredential;
    }
}