using AutoMapper;
using GnassoEDI3.Application.DTOs.ReporteMensual;
using GnassoEDI3.Applications;
using GnassoEDI3.Entities;
using GnassoEDI3.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GnassoEDI3.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteMensualController : ControllerBase
    {
        private readonly ILogger<ReporteMensualController> _logger;
        //private readonly IApplication<ReporteMensual> _reporte;
        private readonly IReporteMensualService _reporteService;
        private readonly IMapper _mapper;

        public ReporteMensualController(ILogger<ReporteMensualController> logger,IReporteMensualService reporteService, IMapper mapper)
        {
            _logger = logger;
            //_reporte = reporte;
            _reporteService = reporteService;
            _mapper = mapper;
        }

        [HttpGet("All")]
        public IActionResult All()
        {
            var reportes = _reporteService.GetReportesMensuales();
            return Ok(_mapper.Map<IList<ReporteMensualResponseDto>>(reportes));
        }

        [HttpGet("ById")]
        public IActionResult ById(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var reporte = _reporteService.GetReporteMensualById(id.Value);
            if (reporte is null) return NotFound();
            return Ok(_mapper.Map<ReporteMensualResponseDto>(reporte));
        }

        [HttpPost]
        public IActionResult Crear(ReporteMensualRequestDto reporteRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            var reporte = _mapper.Map<ReporteMensual>(reporteRequest);
            _reporteService.SaveReporteMensual(reporte);
            return Ok(reporte.Id);
        }

        [HttpPut]
        public IActionResult Editar(int? id, ReporteMensualRequestDto reporteRequest)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();

            var reporteBack = _reporteService.GetReporteMensualById(id.Value);
            if (reporteBack is null) return NotFound();

            //reporteBack.Mes = reporte.Mes;
            //reporteBack.Anio = reporte.Anio;
            //reporteBack.HorasTotales = reporte.HorasTotales;
            //reporteBack.EmpleadoId = reporte.EmpleadoId;
            _mapper.Map(reporteRequest, reporteBack);
            _reporteService.SaveReporteMensual(reporteBack);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Borrar(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var reporte = _reporteService.GetReporteMensualById(id.Value);
            if (reporte is null) return NotFound();

            _reporteService.DeleteReporteMensual(id.Value);
            return Ok();
        }
    }
}

