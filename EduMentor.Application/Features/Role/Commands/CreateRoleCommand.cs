using Ardalis.Result;
using AutoMapper;
using EduMentor.Application.Features.Role.DTOs;
using EduMentor.Application.Interfaces.Repositories;
using EduMentor.Domain.Generic;
using MediatR;

namespace EduMentor.Application.Features.Role.Commands;

public class CreateRoleCommand : IRequest<Result<ResponseType<ReadRoleDto>>>
{
    public required AddRoleDto Role { get; set; }
}

public sealed class AddRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper)
    : IRequestHandler<CreateRoleCommand, Result<ResponseType<ReadRoleDto>>>
{
    public async Task<Result<ResponseType<ReadRoleDto>>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var existsRole = roleRepository.IsNameUnique(request.Role.Name);

        if (existsRole is { IsSuccess: false, Object: null })
        {
            return Result<ResponseType<ReadRoleDto>>.Success(new ResponseType<ReadRoleDto>
            {
                IsSuccess = false,
                Message = existsRole.Message,
            });
        }

        var addRole = mapper.Map<Domain.Model.Role>(request.Role);

        var addResponse = roleRepository.Add(addRole);

        if (addResponse is { IsSuccess: false, Object: null })
        {
            return Result<ResponseType<ReadRoleDto>>.Success(new ResponseType<ReadRoleDto>
            {
                IsSuccess = false,
                Message = addResponse.Message,
            });
        }

        return Result<ResponseType<ReadRoleDto>>.Success(new ResponseType<ReadRoleDto>
        {
            IsSuccess = true,
            Message = "Role added successfully!",
            Object = mapper.Map<ReadRoleDto>(addResponse.Object),
        });
    }
}