using Ardalis.Result;
using AutoMapper;
using EduMentor.Application.Features.User.DTOs;
using EduMentor.Application.Interfaces.Repositories;
using EduMentor.Domain.Generic;
using MediatR;

namespace EduMentor.Application.Features.User.Queries;

public class GetUserByIdQuery : IRequest<Result<ResponseType<ReadUserDto>>>
{
    public Guid UserId { get; set; }
}

public sealed class GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<GetUserByIdQuery, Result<ResponseType<ReadUserDto>>>
{
    public async Task<Result<ResponseType<ReadUserDto>>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = userRepository.GetObjectById(request.UserId);

        if (user is { IsSuccess: false, Object: null })
        {
            return Result<ResponseType<ReadUserDto>>.Success(new ResponseType<ReadUserDto>
            {
                IsSuccess = false,
                Message = user.Message
            });
        }

        return Result<ResponseType<ReadUserDto>>.Success(new ResponseType<ReadUserDto>
        {
            IsSuccess = true,
            Message = user.Message,
            Object = mapper.Map<ReadUserDto>(user.Object!)
        });
    }
}