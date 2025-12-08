using System.ComponentModel.DataAnnotations;

namespace EduMentor.Domain.Model;

public class Role
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    [Required][MaxLength(255)] public string Name { get; set; } = string.Empty;

    [Required] public bool IsDeleted { get; set; }
}
