using GnassoEDI3.Applications;
using GnassoEDI3.Entities;
using GnassoEDI3.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroAsistenciaController : ControllerBase
    {
        private readonly ILogger<RegistroAsistenciaController> _logger;
        //private readonly IApplication<RegistroAsistencia> _registro;
        private readonly IRegistroAsistenciaService _registroService;

        public RegistroAsistenciaController(ILogger<RegistroAsistenciaController> logger, IRegistroAsistenciaService registroService)
        {
            _logger = logger;
            //_registro = registro;
            _registroService = registroService;
        }

        [HttpGet("All")]
        public IActionResult All() => Ok(_registroService.GetRegistroAsistencias());

        [HttpGet("ById")]
        public IActionResult ById(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var registro = _registroService.GetRegistroAsistenciaById(id.Value);
            if (registro is null) return NotFound();
            return Ok(registro);
        }

        [HttpPost]
        public IActionResult Crear(RegistroAsistencia registro)
        {
            if (!ModelState.IsValid) return BadRequest();
            _registroService.SaveRegistroAsistencia(registro);
            return Ok(registro.Id);
        }

        [HttpPut]
        public IActionResult Editar(int? id, RegistroAsistencia registro)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();

            var registroBack = _registroService.GetRegistroAsistenciaById(id.Value);
            if (registroBack is null) return NotFound();

            registroBack.Fecha = registro.Fecha;
            registroBack.HoraEntrada = registro.HoraEntrada;
            registroBack.HoraSalida = registro.HoraSalida;
            registroBack.EmpleadoId = registro.EmpleadoId;

            _registroService.SaveRegistroAsistencia(registroBack);
            return Ok(registroBack);
        }

        [HttpDelete]
        public IActionResult Borrar(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var registro = _registroService.GetRegistroAsistenciaById(id.Value);
            if (registro is null) return NotFound();

            _registroService.DeleteRegistroAsistencia(id.Value);
            return Ok();
        }
    }
}

