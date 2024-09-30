using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace SpaceCorps.Business.Authorization;

public class UserCredential
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Email { get; init; } = null!;
    [NotMapped]
    public string Password { get; init; } = null!;
    public string Salt { get; init; }
    public string Hash { get; init; }

    public UserCredential()
    {
        Salt = GenerateSalt();
        Hash = HashPassword("", Salt);
    }

    public UserCredential(string email, string password) : this()
    {
        Email = email;
        Password = password;
        Hash = HashPassword(password, Salt);
    }

    private static string GenerateSalt()
    {
        byte[] saltBytes = new byte[16];
        RandomNumberGenerator.Fill(saltBytes);
        return Convert.ToBase64String(saltBytes);
    }

    private static string HashPassword(string password, string salt)
    {
        var hashedBytes = SHA512.HashData(Encoding.UTF8.GetBytes(password + salt));
        return Convert.ToHexString(hashedBytes);
    }

    public bool VerifyPassword(string password)
    {
        var hash = HashPassword(password, Salt);
        return hash.Equals(Hash, StringComparison.OrdinalIgnoreCase);
    }
}