using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduMentor.Domain.Model;

public class User
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    [Required][MaxLength(255)] public string FirstName { get; set; } = string.Empty;

    [Required][MaxLength(255)] public string LastName { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required][MaxLength(255)] public string Username { get; set; } = string.Empty;

    [Required] public DateOnly DateOfBirth { get; set; }

    [Required] public byte[] PasswordHash { get; set; } = [];

    [Required] public byte[] PasswordSalt { get; set; } = [];

    [Required] public Guid RoleId { get; set; } = Guid.NewGuid();

    [ForeignKey(nameof(RoleId))] public Role Role { get; set; } = null!;

    [Required] public bool IsDeleted { get; set; }
}
