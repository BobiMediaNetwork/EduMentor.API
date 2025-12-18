using Ardalis.Result;
using AutoMapper;
using EduMentor.Application.Features.Role.DTOs;
using EduMentor.Application.Interfaces.Repositories;
using EduMentor.Domain.Generic;
using MediatR;

namespace EduMentor.Application.Features.Role.Queries;

public class GetRoleByIdQuery : IRequest<Result<ResponseType<ReadRoleDto>>>
{
    public Guid RoleId { get; set; }
}

public sealed class GetRoleByIdQueryHandler(IRoleRepository roleRepository, IMapper mapper)
    : IRequestHandler<GetRoleByIdQuery, Result<ResponseType<ReadRoleDto>>>
{
    public async Task<Result<ResponseType<ReadRoleDto>>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = roleRepository.GetObjectById(request.RoleId);

        if (role is { IsSuccess: false, Object: null })
        {
            return Result<ResponseType<ReadRoleDto>>.Success(new ResponseType<ReadRoleDto>
            {
                IsSuccess = false,
                Message = role.Message
            });
        }

        return Result<ResponseType<ReadRoleDto>>.Success(new ResponseType<ReadRoleDto>
        {
            IsSuccess = true,
            Message = role.Message,
            Object = mapper.Map<ReadRoleDto>(role.Object)
        });
    }
}