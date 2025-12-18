using Ardalis.Result;
using AutoMapper;
using EduMentor.Application.Features.User.DTOs;
using EduMentor.Application.Interfaces.Email;
using EduMentor.Application.Interfaces.Repositories;
using EduMentor.Domain.Generic;
using EduMentor.Domain.Model;
using MediatR;

namespace EduMentor.Application.Features.User.Commands;

public class CreateUserCommand : IRequest<Result<ResponseType<ReadUserDto>>>
{
    public required AddUserDto User { get; set; }
}

public sealed class UserCommandHandler(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IEmailService emailService,
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

        try
        {
            //TODO: add password reset link generation
            var resetLink = $"https://localhost:7226/api/user/password-rest/123";

            const string subject = "Welcome to EduMentor! We are glad to have you here!";

            var body =
                $"""
                 <p>Hello!</p>
                 <p>Welcome to <strong>EduMentor</strong>! Your account has been created!</p>
                 <p>To get started, please set your password by clicking <a href='{resetLink}'>here</a> or by copying and pasting the following link into your browser:</p>
                 <p><a href='{resetLink}'>{resetLink}</a></p>
                 <p><strong>This link will expire in 1 hour.</strong></p>
                 <p>Once your password is set, you can log in and start using EduMentor.</p>
                 <p>We're excited to have you on board!</p>
                 <p>Cheers,<br>The EduMentor Team</p>
                 """;

            await emailService.SendEmailAsync(addUserResponse.Object!.Email, subject, body);
        }
        catch (Exception ex)
        {
            return Result<ResponseType<ReadUserDto>>.Success(new ResponseType<ReadUserDto>
            {
                IsSuccess = false,
                Object = null,
                Message = ex.Message
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