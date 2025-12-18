using EduMentor.Application.Features.Role.Commands;
using FluentValidation;

namespace EduMentor.Application.Features.Role.Validators;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x => x.Role.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(255);
    }
}