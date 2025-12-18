namespace EduMentor.Application.Features.Role.DTOs;

public class UpdateRoleDto
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Name { get; set; } = null;
}
