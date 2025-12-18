using AutoMapper;
using EduMentor.Application.Features.Auth.DTOs;
using EduMentor.Application.Features.User.DTOs;
using EduMentor.Domain.Model;
using System.Security.Cryptography;
using System.Text;

namespace EduMentor.Application.MappingRofiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, ReadUserDto>()
            .ForMember(dest
                => dest.RoleName, opt
                => opt.MapFrom(src => src.Role.Name));
        CreateMap<User, LogInResponseDto>()
            .ForMember(dest
                => dest.RoleName, opt
                => opt.MapFrom(src => src.Role.Name));
        CreateMap<AddUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false))
            .AfterMap((src, dest) =>
            {
                PasswordGenerator.CreatePasswordHash(src.Password, out var hash, out var salt);
                dest.PasswordHash = hash;
                dest.PasswordSalt = salt;
            });
        CreateMap<UpdateUserDto, User>()
            .ForMember(dest => dest.RoleId,
                opt =>
                    opt.Condition(src => src.RoleId.HasValue && src.RoleId != Guid.Empty))
            .AfterMap((src, dest) =>
            {
                if (string.IsNullOrWhiteSpace(src.Password)) return;
                PasswordGenerator.CreatePasswordHash(src.Password, out var hash, out var salt);
                dest.PasswordHash = hash;
                dest.PasswordSalt = salt;
            })
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                    srcMember != null && !(srcMember is Guid guid && guid == Guid.Empty)));
    }
}

public static class PasswordGenerator
{
    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
}