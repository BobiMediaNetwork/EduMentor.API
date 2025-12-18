using EduMentor.Application.Features.User.Commands;
using FluentValidation;

namespace EduMentor.Application.Features.User.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.User.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(255);

        RuleFor(x => x.User.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(500);

        RuleFor(x => x.User.Username)
            .NotEmpty().WithMessage("Username is required")
            .MaximumLength(500);

        RuleFor(x => x.User.DateOfBirth)
            .NotEqual(DateOnly.MinValue).WithMessage("Date of birth is required.")
            .LessThan(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Date of birth must be in the past.");

        RuleFor(x => x.User.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.")
            .MaximumLength(255);

        RuleFor(x => x.User.RoleId)
            .NotEqual(Guid.Empty).WithMessage("RoleId is required");
    }
}