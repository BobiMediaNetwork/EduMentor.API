namespace EduMentor.Application.Features.User.DTOs;

public class ReadUserDto
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public DateOnly DateOfBirth { get; set; }

    public Guid RoleId { get; set; } = Guid.NewGuid();

    public string RoleName { get; set; } = string.Empty;
}
