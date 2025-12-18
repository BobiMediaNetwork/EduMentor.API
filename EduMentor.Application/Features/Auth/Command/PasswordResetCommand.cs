using Ardalis.Result;
using EduMentor.Application.Common.DTOs;
using EduMentor.Application.Interfaces.Email;
using EduMentor.Application.Interfaces.Repositories;
using EduMentor.Domain.Generic;
using EduMentor.Domain.Model;
using MediatR;

namespace EduMentor.Application.Features.Auth.Command;

public class PasswordResetCommand : IRequest<Result<ResponseType<bool>>>
{
    public required PasswordResetDto PasswordReset { get; set; }
}

public sealed class PasswordResetCommandHandler(
    IUserRepository userRepository,
    IPasswordResetRepository passwordResetRepository,
    IEmailService emailService)
    : IRequestHandler<PasswordResetCommand, Result<ResponseType<bool>>>
{
    public async Task<Result<ResponseType<bool>>> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
    {
        var user = userRepository.GetUserByEmail(request.PasswordReset.Email);

        if (user is { IsSuccess: false, Object: null })
        {
            return Result<ResponseType<bool>>.Success(new ResponseType<bool>
            {
                IsSuccess = false,
                Message = user.Message
            });
        }

        var addPasswordReset = passwordResetRepository.Add(new PasswordReset
        {
            UserId = user.Object!.Id,
            ExpirationTime = DateTime.Now.AddHours(1)
        });

        if (addPasswordReset is { IsSuccess: false, Object: null })
        {
            return Result<ResponseType<bool>>.Success(new ResponseType<bool>
            {
                IsSuccess = false,
                Message = addPasswordReset.Message
            });
        }

        try
        {
            var resetId = addPasswordReset.Object!.Id.ToString();
            var resetLink = $"https://localhost:7239/api/user/password-reset/{resetId}";

            const string subject = "Reset Your EduMentor Password";

            var body =
                $"""
                 <p>Hello!</p>
                 <p>Forgot your password? No worries. It happens to everyone. We’ve made it easy for you to access EduMentor again.</p>
                 <p>You can reset your password immediately by clicking <a href='{resetLink}'>here</a> or pasting the following link in your browser:</p>
                 <p><a href='{resetLink}'>{resetLink}</a></p>
                 <p><strong>Link is available for 1 hour!</strong></p>
                 <p>Cheers,<br>The EduMentor Team</p>
                 """;

            await emailService.SendEmailAsync(user.Object!.Email, subject, body);
        }
        catch (Exception ex)
        {
            return Result<ResponseType<bool>>.Success(new ResponseType<bool>
            {
                IsSuccess = false,
                Message = ex.Message
            });
        }

        return Result<ResponseType<bool>>.Success(new ResponseType<bool>
        {
            IsSuccess = true,
            Message = "Password link sent succesfully!"
        });
    }
}