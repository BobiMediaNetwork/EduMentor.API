using Ardalis.Result;
using AutoMapper;
using EduMentor.Application.Features.User.DTOs;
using EduMentor.Application.Interfaces.Repositories;
using EduMentor.Domain.Generic;
using MediatR;

namespace EduMentor.Application.Features.User.Commands;

public class CreateUserCommand : IRequest<Result<ResponseType<ReadUserDto>>>
{
    public required AddUserDto User { get; set; }
}

public sealed class UserCommandHandler(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IMapper mapper)
    : IRequestHandler<CreateUserCommand, Result<ResponseType<ReadUserDto>>>
{
    public async Task<Result<ResponseType<ReadUserDto>>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var role = roleRepository.GetObjectById(request.User.RoleId);

        if (role is { IsSuccess: false, Object: null })
        {
            return Result<ResponseType<ReadUserDto>>.Success(new ResponseType<ReadUserDto>
            {
                IsSuccess = false,
                Message = role.Message,
            });
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

        var addUser = mapper.Map<Domain.Model.User>(request.User);

        var addUserResponse = userRepository.Add(addUser);

        if (addUserResponse is { IsSuccess: false, Object: null })
        {
            return Result<ResponseType<ReadUserDto>>.Success(new ResponseType<ReadUserDto>
            {
                IsSuccess = false,
                Message = addUserResponse.Message,
            });
        }

        return Result<ResponseType<ReadUserDto>>.Success(new ResponseType<ReadUserDto>
        {
            IsSuccess = true,
            Message = "User added successfully!",
            Object = mapper.Map<ReadUserDto>(addUserResponse.Object!),
        });
    }
}