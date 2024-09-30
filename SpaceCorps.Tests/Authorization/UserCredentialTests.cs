using SpaceCorps.Business.Authorization;

namespace SpaceCorps.Tests.Authorization;

public class UserCredentialTests
{
    [Fact]
    public void UserCredential_HashPassword_GeneratesHash()
    {
        var userCredential = new UserCredential();
        var hash = userCredential.Hash;

        Assert.NotNull(hash);
    }

    [Fact]
    public void UserCredential_VerifyPassword_ReturnsTrue()
    {
        var password = "password";
        var userCredential = new UserCredential("user@gmail.com", password);
        var result = userCredential.VerifyPassword(password);

        Assert.True(result);
    }

    [Fact]
    public void UserCredential_VerifyPassword_ReturnsFalse()
    {
        var userCredential = new UserCredential("user@gmail.com", "lol");
        var password = "wrongpassword";
        var result = userCredential.VerifyPassword(password);

        Assert.False(result);
    }
}