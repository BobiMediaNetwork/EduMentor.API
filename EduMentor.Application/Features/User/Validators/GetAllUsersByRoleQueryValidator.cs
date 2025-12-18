using EduMentor.Application.Features.User.Queries;
using FluentValidation;

namespace EduMentor.Application.Features.User.Validators;

public class GetAllUsersByRoleQueryValidator : AbstractValidator<GetAllUsersByRoleQuery>
{
    public GetAllUsersByRoleQueryValidator()
    {
        RuleFor(role => role)
            .IsInEnum().WithMessage("Invalid role.");
    }

}