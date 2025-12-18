using Ardalis.Result;
using AutoMapper;
using EduMentor.Application.Features.Role.DTOs;
using EduMentor.Application.Interfaces.Repositories;
using EduMentor.Domain.Generic;
using MediatR;

namespace EduMentor.Application.Features.Role.Queries;

public class GetAllRolesQuery : IRequest<Result<ResponseType<ReadRoleDto>>>
{
}

public sealed class GGetAllRolesQueryHandler(IRoleRepository roleRepository, IMapper mapper)
    : IRequestHandler<GetAllRolesQuery, Result<ResponseType<ReadRoleDto>>>
{
    public async Task<Result<ResponseType<ReadRoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = roleRepository.GetAll();

        if (roles is { IsSuccess: false, Collection: null })
        {
            return Result<ResponseType<ReadRoleDto>>.Success(new ResponseType<ReadRoleDto>
            {
                IsSuccess = false,
                Message = roles.Message
            });
        }

        return Result<ResponseType<ReadRoleDto>>.Success(new ResponseType<ReadRoleDto>
        {
            IsSuccess = true,
            Message = roles.Message,
            Collection = [.. roles.Collection!.Select(mapper.Map<ReadRoleDto>)]
        });
    }
}