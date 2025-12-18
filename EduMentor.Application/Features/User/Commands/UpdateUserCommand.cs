using Ardalis.Result;
using AutoMapper;
using EduMentor.Application.Features.User.DTOs;
using EduMentor.Application.Interfaces.Repositories;
using EduMentor.Domain.Generic;
using MediatR;

namespace EduMentor.Application.Features.User.Commands;

public class UpdateUserCommand : IRequest<Result<ResponseType<ReadUserDto>>>
{
    public required UpdateUserDto User { get; set; }
}

public sealed class UpdateUserCommandHandler(
    IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
    : IRequestHandler<UpdateUserCommand, Result<ResponseType<ReadUserDto>>>
{
    public async Task<Result<ResponseType<ReadUserDto>>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (request.User.RoleId.HasValue)
        {
            var role = roleRepository.GetObjectById(request.User.RoleId.Value);

            if (role is { IsSuccess: false, Object: null })
            {
                return Result<ResponseType<ReadUserDto>>.Success(new ResponseType<ReadUserDto>
                {
                    IsSuccess = false,
                    Message = role.Message,
                });
            }
        }

        var isEmailAndUsernameUnique = userRepository.IsEmailAndUsernameUnique(request.User.Email, request.User.Username);

        if (isEmailAndUsernameUnique is { IsSuccess: false, Object: null })
        {
            return Result<ResponseType<ReadUserDto>>.Success(new ResponseType<ReadUserDto>
            {
                IsSuccess = false,
                Message = isEmailAndUsernameUnique.Message,
            });
        }

        var user = userRepository.GetUserByIdWithOwnProperties(request.User.Id);

        if (user is { IsSuccess: false, Object: null })
        {
            return Result<ResponseType<ReadUserDto>>.Success(new ResponseType<ReadUserDto>
            {
                IsSuccess = false,
                Message = user.Message
            });
        }

        request.User.DateOfBirth ??= user.Object!.DateOfBirth;
        mapper.Map(request.User, user.Object);

        var updateResponse = userRepository.Update(user.Object!);

        if (updateResponse is { IsSuccess: false, Object: null })
        {
            return Result<ResponseType<ReadUserDto>>.Success(new ResponseType<ReadUserDto>
            {
                IsSuccess = false,
                Message = updateResponse.Message,
            });
        }

        return Result<ResponseType<ReadUserDto>>.Success(new ResponseType<ReadUserDto>
        {
            IsSuccess = true,
            Message = "User updated successfully!",
            Object = mapper.Map<ReadUserDto>(updateResponse.Object!),
        });
    }
}