using AutoMapper;
using GnassoEDI3.Application.DTOs.RegistroAsistencia;
using GnassoEDI3.Applications;
using GnassoEDI3.Entities;
using GnassoEDI3.Entities.MicrosoftIdentity;
using GnassoEDI3.Enums;
using GnassoEDI3.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        private readonly UserManager<User> _userManager;

        public RegistroAsistenciaController(ILogger<RegistroAsistenciaController> logger, IRegistroAsistenciaService registroService, IMapper mapper, UserManager<User> userManager)
        {
            _logger = logger;
            //_registro = registro;
            _registroService = registroService;
            _mapper = mapper;
            _userManager = userManager;
        }
        [Authorize(Roles = "Gerente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("All")]
        public IActionResult All() 
        { 
            var registros = _registroService.GetRegistroAsistencias();
            return Ok(_mapper.Map<IList<RegistroAsistenciaResponseDto>>(registros));
        }


        [Authorize(Roles = "Gerente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("ById")]
        public IActionResult ById(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var registro = _registroService.GetRegistroAsistenciaById(id.Value);
            if (registro is null) return NotFound();
            return Ok(_mapper.Map<RegistroAsistenciaResponseDto>(registro));
        }


        [Authorize(Roles = "Gerente, Empleado", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("FirmaEntrada")]
        public async Task<IActionResult> FirmarEntrada()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(idClaim)) return Unauthorized();

            if (!Guid.TryParse(idClaim, out var userGuid)) return Unauthorized();

            try
            {
                var registro = await _registroService.FirmarEntradaAsync(userGuid);
                var dto = _mapper.Map<RegistroAsistenciaResponseDto>(registro);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en FirmarEntrada para user {User}", userGuid);
                return StatusCode(500, "Error interno");
            }
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

