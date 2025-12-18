using EduMentor.Application.Features.Auth.Command;
using FluentValidation;

namespace EduMentor.Application.Features.Auth.Validators;

public class AuthentificationCommandValidator : AbstractValidator<AuthentificationCommand>
{
    public AuthentificationCommandValidator()
    {
        RuleFor(x => x.LogInRequest.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.")
            .MaximumLength(255);

        RuleFor(x => x.LogInRequest.Password)
            .NotEmpty().WithMessage("Password is required")
            .MaximumLength(255);
    }
}