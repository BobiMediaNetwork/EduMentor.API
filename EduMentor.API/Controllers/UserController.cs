using EduMentor.Application.Common.DTOs;
using EduMentor.Application.Features.User.Commands;
using EduMentor.Application.Features.User.DTOs;
using EduMentor.Application.Features.User.Queries;
using EduMentor.Domain.Enum;
using EduMentor.Domain.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EduMentor.API.Controllers;

public class UserController(ISender sender) : BaseAPIController
{
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<ReadUserDto>> GetAllUsers()
    {
        return await sender.Send(new GetAllUsersQuery());
    }

    [HttpGet("{userId:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<ReadUserDto>> GetUserById(Guid userId)
    {
        return await sender.Send(new GetUserByIdQuery { UserId = userId });
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<ReadUserDto>> AddUser(AddUserDto user)
    {
        return await sender.Send(new CreateUserCommand { User = user });
    }

    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<ReadUserDto>> UpdateUser(UpdateUserDto user)
    {
        return await sender.Send(new UpdateUserCommand { User = user });
    }

    [HttpDelete("{userId:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<bool>> DeleteUser(Guid userId)
    {
        return await sender.Send(new DeleteUserCommand { UserId = userId });
    }

    [HttpGet("all/{role}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<ReadUserDto>> GetAllUserByRole(RoleEnum role)
    {
        return await sender.Send(new GetAllUsersByRoleQuery { Role = role });
    }

    [HttpPatch("password-reset/{passwordResetId:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<bool>> UpdatePassword(Guid passwordResetId, [FromBody] NewPasswordDto newPassword)
    {
        return await sender.Send(new ChangePasswordCommand { PasswordResetId = passwordResetId, NewPassword = newPassword });
    }
}