using EduMentor.Application.Common.DTOs;
using EduMentor.Application.Features.Auth.Command;
using EduMentor.Domain.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduMentor.API.Controllers;

public class AuthController(ISender sender) : BaseAPIController
{
    [HttpPost("password-reset")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<bool>> PasswordReset([FromBody] PasswordResetDto passwordReset)
    {
        return await sender.Send(new PasswordResetCommand { PasswordReset = passwordReset });
    }
}
