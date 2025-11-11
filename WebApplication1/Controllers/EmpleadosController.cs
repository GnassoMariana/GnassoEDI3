using AutoMapper;
using GnassoEDI3.Application.DTOs.Empleado;
using GnassoEDI3.Applications;
using GnassoEDI3.Entities;
using GnassoEDI3.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GnassoEDI3.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly ILogger<EmpleadosController> _logger;
        //private readonly IApplication<Empleado> _empleado;
        private readonly IEmpleadoService _empleadoService;
        private readonly IMapper _mapper;

        public EmpleadosController(ILogger<EmpleadosController> logger, IEmpleadoService empleadoService, IMapper mapper)
        {
            _logger = logger;
            //_empleado = empleado;
            _empleadoService = empleadoService;
            _mapper = mapper;

        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            var empleados = _empleadoService.GetEmpleado();
            return Ok(_mapper.Map<IList<EmpleadoResponseDto>>(empleados));
            //return Ok(_empleadoService.GetEmpleado());
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }

            Empleado empleado = _empleadoService.GetEmmpleadoById(Id.Value);
            if (empleado is null)
            {
                return NotFound();
            }

            //return Ok(empleado);
            return Ok(_mapper.Map<EmpleadoRequestDto>(empleado));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(EmpleadoRequestDto empleadoRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var empleado = _mapper.Map<Empleado>(empleadoRequest);
            _empleadoService.SaveEmpleado(empleado);
            return Ok(empleado.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, EmpleadoRequestDto empleadoRequest)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Empleado empleadoBack = _empleadoService.GetEmmpleadoById(Id.Value);
            if (empleadoBack is null)
            {
                return NotFound();
            }

            //empleadoBack.Nombre = empleado.Nombre;
            //empleadoBack.Apellido = empleado.Apellido;
            //empleadoBack.Dni = empleado.Dni;
            //empleadoBack.SalarioBase = empleado.SalarioBase;
            //empleadoBack.Antiguedad = empleado.Antiguedad;
            //empleadoBack.TrabajadorActivo = empleado.TrabajadorActivo;
            //empleadoBack.Jornada = empleado.Jornada;
            _mapper.Map(empleadoRequest, empleadoBack);


            _empleadoService.SaveEmpleado(empleadoBack);
            return Ok();
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

            Empleado empleadoBack = _empleadoService.GetEmmpleadoById(Id.Value);
            if (empleadoBack is null)
            {
                return NotFound();
            }

            _empleadoService.DeleteEmmpleado(empleadoBack.Id);
            return Ok();
        }

    }
}
