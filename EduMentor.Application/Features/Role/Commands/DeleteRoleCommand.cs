using Ardalis.Result;
using EduMentor.Application.Interfaces.Repositories;
using EduMentor.Domain.Generic;
using MediatR;

namespace EduMentor.Application.Features.Role.Commands;

public class DeleteRoleCommand : IRequest<Result<ResponseType<bool>>>
{
    public required Guid RoleId { get; set; }
}

public sealed class DeleteRoleCommandHandler(IRoleRepository roleRepository) : IRequestHandler<DeleteRoleCommand, Result<ResponseType<bool>>>
{
    public async Task<Result<ResponseType<bool>>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = roleRepository.GetObjectById(request.RoleId);

        if (role is not { IsSuccess: true, Object: not null })
        {
            return Result<ResponseType<bool>>.Success(new ResponseType<bool>
            {
                IsSuccess = false,
                Message = role.Message
            });
        }

        var res = roleRepository.Delete(request.RoleId);

        if (res is { IsSuccess: true })
        {
            return Result<ResponseType<bool>>.Success(new ResponseType<bool>
            {
                Object = true,
                IsSuccess = true,
                Message = res.Message
            });
        }

        return Result<ResponseType<bool>>.Success(new ResponseType<bool>
        {
            IsSuccess = false,
            Message = res.Message,
            Object = false
        });
    }
}