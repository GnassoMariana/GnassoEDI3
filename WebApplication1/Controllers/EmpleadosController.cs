using GnassoEDI3.Applications;
using GnassoEDI3.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly ILogger<EmpleadosController> _logger;
        private readonly IApplication<Empleado> _empleado;

        public EmpleadosController(ILogger<EmpleadosController> logger, IApplication<Empleado> empleado)
        {
            _logger = logger;
            _empleado = empleado;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_empleado.GetAll());
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }

            Empleado empleado = _empleado.GetById(Id.Value);
            if (empleado is null)
            {
                return NotFound();
            }

            return Ok(empleado);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _empleado.Save(empleado);
            return Ok(empleado.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, Empleado empleado)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Empleado empleadoBack = _empleado.GetById(Id.Value);
            if (empleadoBack is null)
            {
                return NotFound();
            }

            empleadoBack.Nombre = empleado.Nombre;
            empleadoBack.Apellido = empleado.Apellido;
            empleadoBack.Dni = empleado.Dni;
            empleadoBack.SalarioBase = empleado.SalarioBase;
            empleadoBack.Antiguedad = empleado.Antiguedad;
            empleadoBack.TrabajadorActivo = empleado.TrabajadorActivo;
            empleadoBack.Jornada = empleado.Jornada;

            _empleado.Save(empleadoBack);
            return Ok(empleadoBack);
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Empleado empleadoBack = _empleado.GetById(Id.Value);
            if (empleadoBack is null)
            {
                return NotFound();
            }

            _empleado.Delete(empleadoBack.Id);
            return Ok();
        }

    }
}
