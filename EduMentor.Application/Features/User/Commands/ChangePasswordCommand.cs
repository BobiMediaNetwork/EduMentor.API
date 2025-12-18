using Ardalis.Result;
using AutoMapper;
using EduMentor.Application.Common.DTOs;
using EduMentor.Application.Features.User.DTOs;
using EduMentor.Application.Interfaces.Repositories;
using EduMentor.Domain.Generic;
using MediatR;

namespace EduMentor.Application.Features.User.Commands;

public class ChangePasswordCommand : IRequest<Result<ResponseType<bool>>>
{
    public required NewPasswordDto NewPassword { get; set; }
}

public sealed class ChangePasswordCommandHandler(
    IUserRepository userRepository,
    IMapper mapper)
    : IRequestHandler<ChangePasswordCommand, Result<ResponseType<bool>>>
{
    public async Task<Result<ResponseType<bool>>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        //TODO: Blocked by email config
        var user = userRepository.GetUserByIdWithOwnProperties(new Guid());

        if (user is { IsSuccess: false, Object: null })
        {
            return Result<ResponseType<bool>>.Success(new ResponseType<bool>
            {
                IsSuccess = false,
                Message = user.Message
            });
        }

        var updateUserDto = new UpdateUserDto
        {
            Id = user.Object!.Id,
            DateOfBirth = user.Object!.DateOfBirth,
            Password = request.NewPassword.Password
        };

        mapper.Map(updateUserDto, user.Object!);

        var updateUser = userRepository.Update(user.Object!);

        if (updateUser is { IsSuccess: false, Object: null })
        {
            return Result<ResponseType<bool>>.Success(new ResponseType<bool>
            {
                IsSuccess = false,
                Message = updateUser.Message
            });
        }

        return Result<ResponseType<bool>>.Success(new ResponseType<bool>
        {
            IsSuccess = true,
            Message = "Password changed successfully!"
        });
    }
}
