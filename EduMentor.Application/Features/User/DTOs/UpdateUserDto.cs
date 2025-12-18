namespace EduMentor.Application.Features.User.DTOs;

public class UpdateUserDto
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? FirstName { get; set; } = null;

    public string? LastName { get; set; } = null;

    public string? Email { get; set; } = null;

    public string? Username { get; set; } = null;

    public DateOnly? DateOfBirth { get; set; } = null;

    public string? Password { get; set; } = null;

    public Guid? SchoolId { get; set; } = null;

    public Guid? RoleId { get; set; } = null;
}
