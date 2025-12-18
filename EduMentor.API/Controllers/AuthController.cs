using EduMentor.Application.Common.DTOs;
using EduMentor.Application.Features.Auth.Command;
using EduMentor.Application.Features.Auth.DTOs;
using EduMentor.Domain.Generic;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduMentor.API.Controllers;

[AllowAnonymous]
public class AuthController(ISender sender) : BaseAPIController
{
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<LogInResponseDto>> LogIn(LogInRequestDto logInRequest)
    {
        return await sender.Send(new AuthentificationCommand { LogInRequest = logInRequest });
    }

    [HttpPost("password-reset")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<bool>> PasswordReset([FromBody] PasswordResetDto passwordReset)
    {
        return await sender.Send(new PasswordResetCommand { PasswordReset = passwordReset });
    }
}
