using Ardalis.Result;
using AutoMapper;
using EduMentor.Application.Features.Auth.DTOs;
using EduMentor.Application.Interfaces.Repositories;
using EduMentor.Application.Interfaces.Security;
using EduMentor.Domain.Generic;
using MediatR;

namespace EduMentor.Application.Features.Auth.Command;

public class AuthentificationCommand : IRequest<Result<ResponseType<LogInResponseDto>>>
{
    public required LogInRequestDto LogInRequest { get; set; }
}

public sealed class AuthenticatifionCommandHandler(IUserRepository userRepository, ISecurityService securityService, ITokenService tokenService, IMapper mapper)
    : IRequestHandler<AuthentificationCommand, Result<ResponseType<LogInResponseDto>>>
{
    public async Task<Result<ResponseType<LogInResponseDto>>> Handle(AuthentificationCommand request, CancellationToken cancellationToken)
    {
        var user = userRepository.GetUserByEmail(request.LogInRequest.Email);

        if (user is { IsSuccess: false, Object: null })
        {
            return Result<ResponseType<LogInResponseDto>>.Success(new ResponseType<LogInResponseDto>
            {
                IsSuccess = false,
                Message = user.Message,
            });
        }

        var isLogged = securityService.VerifyPassword(request.LogInRequest.Password, user.Object!.PasswordHash, user.Object!.PasswordSalt);

        if (!isLogged)
        {
            return Result<ResponseType<LogInResponseDto>>.Success(new ResponseType<LogInResponseDto>
            {
                IsSuccess = false,
                Message = "Email or password are wrong!",
            });
        }

        var response = mapper.Map<LogInResponseDto>(user.Object!);

        response.Token = tokenService.CreateToken(user.Object!.Id);

        return Result<ResponseType<LogInResponseDto>>.Success(new ResponseType<LogInResponseDto>
        {
            IsSuccess = true,
            Object = response,
            Message = "Log in successfully!",
        });
    }
}