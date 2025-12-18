using EduMentor.Application.Features.Auth.Command;
using FluentValidation;

namespace EduMentor.Application.Features.Auth.Validators;
public class PasswordResetCommandValidator : AbstractValidator<PasswordResetCommand>
{
    public PasswordResetCommandValidator()
    {
        RuleFor(x => x.PasswordReset.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.")
            .MaximumLength(255);
    }
}