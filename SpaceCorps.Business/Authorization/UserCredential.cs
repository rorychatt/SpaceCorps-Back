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

    public UserCredential(string email, string password)
    {
        Email = email;
        Password = password;
        Salt = GenerateSalt();
        Hash = HashPassword();
    }

    private string GenerateSalt()
    {
        byte[] saltBytes = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    }

    private string HashPassword()
    {
        var hashedBytes = SHA512.HashData(Encoding.UTF8.GetBytes(Password + Salt));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
    }

    public bool VerifyPassword(string password)
    {
        var hashedBytes = SHA512.HashData(Encoding.UTF8.GetBytes(password + Salt));
        var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        return hash == Hash;
    }
}