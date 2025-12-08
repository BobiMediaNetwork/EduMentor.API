using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduMentor.Domain.Model;

public class PasswordReset
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    [Required] public Guid UserId { get; set; } = Guid.NewGuid();

    [ForeignKey(nameof(UserId))] public User User { get; set; } = null!;

    [Required] public DateTime ExpirationTime { get; set; }

    [Required] public bool IsDeleted { get; set; }
}