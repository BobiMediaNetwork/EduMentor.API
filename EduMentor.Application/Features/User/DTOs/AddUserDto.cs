using EduMentor.Application.Common;

namespace EduMentor.Application.Features.User.DTOs;

public class AddUserDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public DateOnly DateOfBirth { get; set; }

    public string Password { get; } = Utils.CreateUniquePasswordForNewUsers(50);

    public Guid RoleId { get; set; } = Guid.NewGuid();
}
