using Ardalis.Result;
using AutoMapper;
using EduMentor.Application.Features.User.DTOs;
using EduMentor.Application.Interfaces.Repositories;
using EduMentor.Domain.Enum;
using EduMentor.Domain.Generic;
using MediatR;

namespace EduMentor.Application.Features.User.Queries;

public class GetAllUsersByRoleQuery : IRequest<Result<ResponseType<ReadUserDto>>>
{
    public required RoleEnum Role { get; set; }
}

public sealed class GetAllUserByRoleQueryHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<GetAllUsersByRoleQuery, Result<ResponseType<ReadUserDto>>>
{
    public async Task<Result<ResponseType<ReadUserDto>>> Handle(GetAllUsersByRoleQuery request, CancellationToken cancellationToken)
    {
        var users = userRepository.GetAllUsersByRole(request.Role);

        if (users is { IsSuccess: false, Collection: null })
        {
            return Result<ResponseType<ReadUserDto>>.Success(new ResponseType<ReadUserDto>
            {
                IsSuccess = false,
                Message = users.Message
            });
        }

        return Result<ResponseType<ReadUserDto>>.Success(new ResponseType<ReadUserDto>
        {
            IsSuccess = true,
            Message = users.Message,
            Collection = users.Collection!.Select(mapper.Map<ReadUserDto>).ToList()
        });
    }
}