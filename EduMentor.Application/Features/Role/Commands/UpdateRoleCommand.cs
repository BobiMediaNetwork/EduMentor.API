using Ardalis.Result;
using AutoMapper;
using EduMentor.Application.Features.Role.DTOs;
using EduMentor.Application.Interfaces.Repositories;
using EduMentor.Domain.Generic;
using MediatR;

namespace EduMentor.Application.Features.Role.Commands;

public class UpdateRoleCommand : IRequest<Result<ResponseType<ReadRoleDto>>>
{
    public required UpdateRoleDto Role { get; set; }
}

public sealed class UpdateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper)
    : IRequestHandler<UpdateRoleCommand, Result<ResponseType<ReadRoleDto>>>
{
    public async Task<Result<ResponseType<ReadRoleDto>>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {

        var role = roleRepository.GetObjectById(request.Role.Id);

        if (role is { IsSuccess: false, Object: null })
        {
            return Result<ResponseType<ReadRoleDto>>.Success(new ResponseType<ReadRoleDto>
            {
                IsSuccess = false,
                Message = role.Message
            });
        }

        mapper.Map(request.Role, role.Object);

        var updateResponse = roleRepository.Update(role.Object!);

        if (updateResponse is { IsSuccess: false, Object: null })
        {
            return Result<ResponseType<ReadRoleDto>>.Success(new ResponseType<ReadRoleDto>
            {
                IsSuccess = false,
                Message = updateResponse.Message,
            });
        }

        return Result<ResponseType<ReadRoleDto>>.Success(new ResponseType<ReadRoleDto>
        {
            IsSuccess = true,
            Message = "Role updated successfully!",
            Object = mapper.Map<ReadRoleDto>(updateResponse.Object),
        });
    }
}