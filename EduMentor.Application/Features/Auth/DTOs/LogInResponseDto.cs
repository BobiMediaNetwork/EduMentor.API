using EduMentor.Application.Features.User.DTOs;

namespace EduMentor.Application.Features.Auth.DTOs;

public class LogInResponseDto : ReadUserDto
{
    public string Token { get; set; } = string.Empty;
}