using System.ComponentModel.DataAnnotations;

namespace SpaceCorps.Business.Authorization;

public class UserCredential
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Email { get; init; } = null!;
    [Required]
    public string Password { get; init; } = null!;
    public string? Salt { get; init; }
    public string? Hash { get; init; }
    
}