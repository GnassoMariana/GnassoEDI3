using GnassoEDI3.Applications;
using GnassoEDI3.Entities;
using GnassoEDI3.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GnassoEDI3.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        //private readonly IApplication<Usuario> _usuario;
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioService usuarioService)
        {
            _logger = logger;
            //_usuario = usuario;
            _usuarioService = usuarioService;
        }

        [HttpGet("All")]
        public IActionResult All() => Ok(_usuarioService.GetUsuarios());

        [HttpGet("ById")]
        public IActionResult ById(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var usuario = _usuarioService.GetUsuarioById(id.Value);
            if (usuario is null) return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Crear(Usuario usuario)
        {
            if (!ModelState.IsValid) return BadRequest();
            _usuarioService.SaveUsuario(usuario);
            return Ok(usuario.Id);
        }

        [HttpPut]
        public IActionResult Editar(int? id, Usuario usuario)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();

            var usuarioBack = _usuarioService.GetUsuarioById(id.Value);
            if (usuarioBack is null) return NotFound();

            usuarioBack.NombreUsuario = usuario.NombreUsuario;
            usuarioBack.Contrasena = usuario.Contrasena;
            usuarioBack.EmpleadoId = usuario.EmpleadoId;

            _usuarioService.SaveUsuario(usuarioBack);
            return Ok(usuarioBack);
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
