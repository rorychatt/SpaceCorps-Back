using SpaceCorps.Business.Db;

namespace SpaceCorps.Business.Dto.Authorization;

public record DbUserVerifyPasswordResponse()
{
    public DbErrorCode DbErrorCode { get; init; }
    public string? Email { get; init; }

    public DbUserVerifyPasswordResponse(DbErrorCode dbErrorCode, string? email) : this()
    {
        DbErrorCode = dbErrorCode;
        Email = email;
    }

}