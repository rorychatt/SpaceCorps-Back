using SpaceCorps.Business.Authorization;

namespace SpaceCorps.Business.Dto.Authorization;

public record GetUserCredentialsResponse()
{
    public string Email { get; init; } = null!;
    public string HashedPassword { get; init; } = null!;

    public GetUserCredentialsResponse(UserCredential userCredential) : this()
    {
        Email = userCredential.Email;
        HashedPassword = userCredential.Hash;
    }
}