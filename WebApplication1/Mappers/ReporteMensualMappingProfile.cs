using AutoMapper;
using GnassoEDI3.Application.DTOs.ReporteMensual;
using GnassoEDI3.Entities;

namespace GnassoEDI3.Web.Mappers
{
    public class ReporteMensualMappingProfile : Profile
    {
        public ReporteMensualMappingProfile()
        {
            CreateMap<ReporteMensual, ReporteMensualResponseDto>();

            CreateMap<ReporteMensualRequestDto, ReporteMensual>();
        }
    }
}
