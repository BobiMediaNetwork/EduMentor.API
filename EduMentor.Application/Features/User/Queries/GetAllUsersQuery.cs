using Ardalis.Result;
using AutoMapper;
using EduMentor.Application.Features.User.DTOs;
using EduMentor.Application.Interfaces.Repositories;
using EduMentor.Domain.Generic;
using MediatR;

namespace EduMentor.Application.Features.User.Queries;

public class GetAllUsersQuery : IRequest<Result<ResponseType<ReadUserDto>>>
{
}

public sealed class GGetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<GetAllUsersQuery, Result<ResponseType<ReadUserDto>>>
{
    public async Task<Result<ResponseType<ReadUserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = userRepository.GetAll();

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