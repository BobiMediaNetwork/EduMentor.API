using EduMentor.Application.Features.User.Commands;
using FluentValidation;

namespace EduMentor.Application.Features.User.Validators;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.User.Id).NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.User.FirstName)
            .MaximumLength(255)
            .When(x => x.User.FirstName != null);

        RuleFor(x => x.User.LastName)
            .MaximumLength(500)
            .When(x => x.User.LastName != null);

        RuleFor(x => x.User.Username)
            .MaximumLength(500)
            .When(x => x.User.Username != null);

        RuleFor(x => x.User.Username)
            .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one number.")
            .Matches(@"[\!\?\*\.@#\$%\^&\+-]").WithMessage("Password must contain at least one special character.")
            .MaximumLength(255)
            .When(x => x.User.Password != null);

        RuleFor(x => x.User.Email)
            .EmailAddress()
            .MaximumLength(255)
            .When(x => x.User.Email != null);

        RuleFor(x => x.User.DateOfBirth)
            .NotEqual(DateOnly.MinValue).WithMessage("Date of birth is required.")
            .LessThan(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Date of birth must be in the past.");

        RuleFor(x => x.User.RoleId)
            .NotEqual(Guid.Empty).WithMessage("RoleId is required");

        RuleFor(x => x.User.RoleId)
            .Must(id => id == null || id != Guid.Empty)
            .WithMessage("If provided, RoleId cannot be empty.");

        RuleFor(x => x.User.DateOfBirth)
            .Must(dob => dob == null || (dob != DateOnly.MinValue && dob < DateOnly.FromDateTime(DateTime.Today)))
            .WithMessage("If provided, Date of birth must be valid and in the past.");
    }
}