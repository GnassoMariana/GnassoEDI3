using AutoMapper;
using GnassoEDI3.Application.DTOs.Identity;
using GnassoEDI3.Entities.MicrosoftIdentity;

namespace GnassoEDI3.Web.Mappers
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role, RoleResponseDto>();
            CreateMap<RoleRequestDto, Role>();
        }

    }
}
