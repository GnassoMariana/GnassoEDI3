using GnassoEDI3.Applications;
using GnassoEDI3.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteMensualController : ControllerBase
    {
        private readonly ILogger<ReporteMensualController> _logger;
        private readonly IApplication<ReporteMensual> _reporte;

        public ReporteMensualController(ILogger<ReporteMensualController> logger, IApplication<ReporteMensual> reporte)
        {
            _logger = logger;
            _reporte = reporte;
        }

        [HttpGet("All")]
        public IActionResult All() => Ok(_reporte.GetAll());

        [HttpGet("ById")]
        public IActionResult ById(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var reporte = _reporte.GetById(id.Value);
            if (reporte is null) return NotFound();
            return Ok(reporte);
        }

        [HttpPost]
        public IActionResult Crear(ReporteMensual reporte)
        {
            if (!ModelState.IsValid) return BadRequest();
            _reporte.Save(reporte);
            return Ok(reporte.Id);
        }

        [HttpPut]
        public IActionResult Editar(int? id, ReporteMensual reporte)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();

            var reporteBack = _reporte.GetById(id.Value);
            if (reporteBack is null) return NotFound();

            reporteBack.Mes = reporte.Mes;
            reporteBack.Anio = reporte.Anio;
            reporteBack.HorasTotales = reporte.HorasTotales;
            reporteBack.EmpleadoId = reporte.EmpleadoId;

            _reporte.Save(reporteBack);
            return Ok(reporteBack);
        }

        [HttpDelete]
        public IActionResult Borrar(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var reporte = _reporte.GetById(id.Value);
            if (reporte is null) return NotFound();

            _reporte.Delete(id.Value);
            return Ok();
        }
    }
}

