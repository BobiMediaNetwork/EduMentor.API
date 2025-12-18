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
    public required Guid PasswordResetId { get; set; }
    public required NewPasswordDto NewPassword { get; set; }
}

public sealed class ChangePasswordCommandHandler(
    IUserRepository userRepository,
    IMapper mapper,
    IPasswordResetRepository passwordResetRepository)
    : IRequestHandler<ChangePasswordCommand, Result<ResponseType<bool>>>
{
    public async Task<Result<ResponseType<bool>>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var getPasswordReset = passwordResetRepository.GetPasswordResetByIdWithOwnProperties(request.PasswordResetId);

        if (getPasswordReset is { IsSuccess: false, Object: null })
        {
            return Result<ResponseType<bool>>.Success(new ResponseType<bool>
            {
                IsSuccess = false,
                Message = getPasswordReset.Message
            });
        }

        if (getPasswordReset.Object!.ExpirationTime < DateTime.Now)
        {
            var deletePasswordLinkInvalid = passwordResetRepository.Delete(request.PasswordResetId);

            if (deletePasswordLinkInvalid is { IsSuccess: false, Object: null })
            {
                return Result<ResponseType<bool>>.Success(new ResponseType<bool>
                {
                    IsSuccess = false,
                    Message = deletePasswordLinkInvalid.Message
                });
            }

            return Result<ResponseType<bool>>.Success(new ResponseType<bool>
            {
                IsSuccess = false,
                Message = "Link is expired!"
            });
        }

        var user = userRepository.GetUserByIdWithOwnProperties(getPasswordReset.Object!.UserId);

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

        var deletePasswordLink = passwordResetRepository.Delete(request.PasswordResetId);

        if (deletePasswordLink is { IsSuccess: false, Object: null })
        {
            return Result<ResponseType<bool>>.Success(new ResponseType<bool>
            {
                IsSuccess = false,
                Message = deletePasswordLink.Message
            });
        }

        return Result<ResponseType<bool>>.Success(new ResponseType<bool>
        {
            IsSuccess = true,
            Message = "Password changed successfully!"
        });
    }
}