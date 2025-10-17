using GnassoEDI3.Applications;
using GnassoEDI3.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroAsistenciaController : ControllerBase
    {
        private readonly ILogger<RegistroAsistenciaController> _logger;
        private readonly IApplication<RegistroAsistencia> _registro;

        public RegistroAsistenciaController(ILogger<RegistroAsistenciaController> logger, IApplication<RegistroAsistencia> registro)
        {
            _logger = logger;
            _registro = registro;
        }

        [HttpGet("All")]
        public IActionResult All() => Ok(_registro.GetAll());

        [HttpGet("ById")]
        public IActionResult ById(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var registro = _registro.GetById(id.Value);
            if (registro is null) return NotFound();
            return Ok(registro);
        }

        [HttpPost]
        public IActionResult Crear(RegistroAsistencia registro)
        {
            if (!ModelState.IsValid) return BadRequest();
            _registro.Save(registro);
            return Ok(registro.Id);
        }

        [HttpPut]
        public IActionResult Editar(int? id, RegistroAsistencia registro)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();

            var registroBack = _registro.GetById(id.Value);
            if (registroBack is null) return NotFound();

            registroBack.Fecha = registro.Fecha;
            registroBack.HoraEntrada = registro.HoraEntrada;
            registroBack.HoraSalida = registro.HoraSalida;
            registroBack.EmpleadoId = registro.EmpleadoId;

            _registro.Save(registroBack);
            return Ok(registroBack);
        }

        [HttpDelete]
        public IActionResult Borrar(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var registro = _registro.GetById(id.Value);
            if (registro is null) return NotFound();

            _registro.Delete(id.Value);
            return Ok();
        }
    }
}

