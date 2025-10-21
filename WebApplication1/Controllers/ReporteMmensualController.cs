using GnassoEDI3.Applications;
using GnassoEDI3.Entities;
using GnassoEDI3.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteMensualController : ControllerBase
    {
        private readonly ILogger<ReporteMensualController> _logger;
        //private readonly IApplication<ReporteMensual> _reporte;
        private readonly IReporteMensualService _reporteService;

        public ReporteMensualController(ILogger<ReporteMensualController> logger,IReporteMensualService reporteService)
        {
            _logger = logger;
            //_reporte = reporte;
            _reporteService = reporteService;
        }

        [HttpGet("All")]
        public IActionResult All() => Ok(_reporteService.GetReportesMensuales());

        [HttpGet("ById")]
        public IActionResult ById(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var reporte = _reporteService.GetReporteMensualById(id.Value);
            if (reporte is null) return NotFound();
            return Ok(reporte);
        }

        [HttpPost]
        public IActionResult Crear(ReporteMensual reporte)
        {
            if (!ModelState.IsValid) return BadRequest();
            _reporteService.SaveReporteMensual(reporte);
            return Ok(reporte.Id);
        }

        [HttpPut]
        public IActionResult Editar(int? id, ReporteMensual reporte)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();

            var reporteBack = _reporteService.GetReporteMensualById(id.Value);
            if (reporteBack is null) return NotFound();

            reporteBack.Mes = reporte.Mes;
            reporteBack.Anio = reporte.Anio;
            reporteBack.HorasTotales = reporte.HorasTotales;
            reporteBack.EmpleadoId = reporte.EmpleadoId;

            _reporteService.SaveReporteMensual(reporteBack);
            return Ok(reporteBack);
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

