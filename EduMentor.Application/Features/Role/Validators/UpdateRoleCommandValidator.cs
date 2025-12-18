using EduMentor.Application.Features.Role.Commands;
using FluentValidation;

namespace EduMentor.Application.Features.Role.Validators;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(x => x.Role.Id).NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.Role.Name)
            .MaximumLength(255)
            .When(x => x.Role.Name != null);
    }
}