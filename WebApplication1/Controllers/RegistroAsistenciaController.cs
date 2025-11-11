using AutoMapper;
using GnassoEDI3.Application.DTOs.RegistroAsistencia;
using GnassoEDI3.Applications;
using GnassoEDI3.Entities;
using GnassoEDI3.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GnassoEDI3.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroAsistenciaController : ControllerBase
    {
        private readonly ILogger<RegistroAsistenciaController> _logger;
        //private readonly IApplication<RegistroAsistencia> _registro;
        private readonly IRegistroAsistenciaService _registroService;
        private readonly IMapper _mapper;

        public RegistroAsistenciaController(ILogger<RegistroAsistenciaController> logger, IRegistroAsistenciaService registroService, IMapper mapper)
        {
            _logger = logger;
            //_registro = registro;
            _registroService = registroService;
            _mapper = mapper;
        }

        [HttpGet("All")]
        public IActionResult All() 
        { 
            var registros = _registroService.GetRegistroAsistencias();
            return Ok(_mapper.Map<IList<RegistroAsistenciaResponseDto>>(registros));
        }

        [HttpGet("ById")]
        public IActionResult ById(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var registro = _registroService.GetRegistroAsistenciaById(id.Value);
            if (registro is null) return NotFound();
            return Ok(_mapper.Map<RegistroAsistenciaResponseDto>(registro));
        }

        [HttpPost]
        public IActionResult Crear(RegistroAsistenciaRequestDto registroRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            var registro = _mapper.Map<RegistroAsistencia>(registroRequest);
            _registroService.SaveRegistroAsistencia(registro);
            return Ok(registro.Id);
        }

        [HttpPut]
        public IActionResult Editar(int? id, RegistroAsistenciaRequestDto registroRequest)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();

            var registroBack = _registroService.GetRegistroAsistenciaById(id.Value);
            if (registroBack is null) return NotFound();

            //registroBack.Fecha = registro.Fecha;
            //registroBack.HoraEntrada = registro.HoraEntrada;
            //registroBack.HoraSalida = registro.HoraSalida;
            //registroBack.EmpleadoId = registro.EmpleadoId;
            _mapper.Map(registroRequest, registroBack);
            _registroService.SaveRegistroAsistencia(registroBack);
            return Ok();
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

