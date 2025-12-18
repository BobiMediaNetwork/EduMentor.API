using EduMentor.Application.Features.Role.Commands;
using EduMentor.Application.Features.Role.DTOs;
using EduMentor.Application.Features.Role.Queries;
using EduMentor.Domain.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduMentor.API.Controllers;

public class RoleController(ISender sender) : BaseAPIController
{
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<ReadRoleDto>> GetAllRoles()
    {
        return await sender.Send(new GetAllRolesQuery());
    }

    [HttpGet("{roleId:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<ReadRoleDto>> GetRoleById(Guid roleId)
    {
        return await sender.Send(new GetRoleByIdQuery { RoleId = roleId });
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<ReadRoleDto>> AddRole(AddRoleDto role)
    {
        return await sender.Send(new CreateRoleCommand() { Role = role });
    }

    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<ReadRoleDto>> UpdateRole(UpdateRoleDto role)
    {
        return await sender.Send(new UpdateRoleCommand { Role = role });
    }

    [HttpDelete("{roleId:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<bool>> DeleteRole(Guid roleId)
    {
        return await sender.Send(new DeleteRoleCommand { RoleId = roleId });
    }
}