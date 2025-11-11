using AutoMapper;
using GnassoEDI3.Application.DTOs.Usuario;
using GnassoEDI3.Applications;
using GnassoEDI3.Entities;
using GnassoEDI3.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GnassoEDI3.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        //private readonly IApplication<Usuario> _usuario;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioService usuarioService, IMapper mapper)
        {
            _logger = logger;
            //_usuario = usuario;
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        [HttpGet("All")]
        public IActionResult All()
        {
            var usuarios = _usuarioService.GetUsuarios();
            return Ok(_mapper.Map<IList<UsuarioResponseDto>>(usuarios));
        }

        [HttpGet("ById")]
        public IActionResult ById(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var usuario = _usuarioService.GetUsuarioById(id.Value);
            if (usuario is null) return NotFound();
            return Ok(_mapper.Map<UsuarioResponseDto>(usuario));
        }

        [HttpPost]
        public IActionResult Crear(UsuarioRequestDto usuarioRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            var usuario = _mapper.Map<Usuario>(usuarioRequest);
            _usuarioService.SaveUsuario(usuario);
            return Ok(usuario.Id);
        }

        [HttpPut]
        public IActionResult Editar(int? id, UsuarioRequestDto usuarioRequest)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();

            var usuarioBack = _usuarioService.GetUsuarioById(id.Value);
            if (usuarioBack is null) return NotFound();

            //usuarioBack.NombreUsuario = usuario.NombreUsuario;
            //usuarioBack.Contrasena = usuario.Contrasena;
            //usuarioBack.EmpleadoId = usuario.EmpleadoId;
            _mapper.Map(usuarioRequest, usuarioBack);
            _usuarioService.SaveUsuario(usuarioBack);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Borrar(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var usuario = _usuarioService.GetUsuarioById(id.Value);
            if (usuario is null) return NotFound();

            _usuarioService.DeleteUsuario(id.Value);
            return Ok();
        }
    }
}
