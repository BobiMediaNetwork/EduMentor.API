namespace EduMentor.Application.Common.DTOs;

public class PasswordResetDto
{
    public string Email { get; set; } = string.Empty;
}

public class NewPasswordDto
{
    public string Password { get; set; } = string.Empty;
}