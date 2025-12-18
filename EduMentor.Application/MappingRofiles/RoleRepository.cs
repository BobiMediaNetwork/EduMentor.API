using AutoMapper;
using EduMentor.Application.Features.Role.DTOs;
using EduMentor.Domain.Model;

namespace EduMentor.Application.MappingRofiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, ReadRoleDto>();
        CreateMap<AddRoleDto, Role>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false));
        CreateMap<UpdateRoleDto, Role>()
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}