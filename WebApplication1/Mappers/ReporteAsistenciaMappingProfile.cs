using AutoMapper;
using GnassoEDI3.Application.DTOs.RegistroAsistencia;
using GnassoEDI3.Entities;

namespace GnassoEDI3.Web.Mappers
{
    public class RegistroAsistenciaMappingProfile : Profile
    {
        public RegistroAsistenciaMappingProfile()
        {
            CreateMap<RegistroAsistencia, RegistroAsistenciaResponseDto>()
                .ForMember(dest => dest.Fecha, ori => ori.MapFrom(src => src.Fecha.ToShortDateString()))
                .ForMember(dest => dest.HoraEntrada, ori => ori.MapFrom(src => src.HoraEntrada.ToString("HH:mm")))
                .ForMember(dest => dest.HoraSalida, ori => ori.MapFrom(src => src.HoraSalida.ToString("HH:mm")))
                .ForMember(dest => dest.Estado, ori => ori.MapFrom(src => src.Estado.ToString()));

            CreateMap<RegistroAsistenciaRequestDto, RegistroAsistencia>();
        }
    }
}
