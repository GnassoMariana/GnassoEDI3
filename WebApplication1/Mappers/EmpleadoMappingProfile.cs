using AutoMapper;
using GnassoEDI3.Application.DTOs.Empleado;
using GnassoEDI3.Entities;

namespace GnassoEDI3.Web.Mappers
{
    public class EmpleadoMappingProfile : Profile
    {
        public EmpleadoMappingProfile()
        {
            CreateMap<Empleado, EmpleadoResponseDto>()
                .ForMember(dest => dest.Jornada, ori => ori.MapFrom(src => src.Jornada.ToString()))
                .ForMember(dest => dest.FechaAlta, ori => ori.MapFrom(src => src.FechaAlta.ToShortDateString()));

            CreateMap<EmpleadoRequestDto, Empleado>();
        }
    }
}
