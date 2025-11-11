using AutoMapper;
using GnassoEDI3.Application.DTOs.Usuario;
using GnassoEDI3.Entities;

namespace GnassoEDI3.Web.Mappers
{
    public class UsuarioMappingProfile : Profile
    {
        public UsuarioMappingProfile()
        {
            CreateMap<Usuario, UsuarioResponseDto>()
                .ForMember(dest => dest.Rol, ori => ori.MapFrom(src => src.Rol.ToString()));

            CreateMap<UsuarioRequestDto, Usuario>();
        }
    }
}
