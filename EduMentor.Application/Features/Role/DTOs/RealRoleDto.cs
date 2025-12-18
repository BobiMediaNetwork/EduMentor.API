namespace EduMentor.Application.Features.Role.DTOs;

public class ReadRoleDto
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;
}