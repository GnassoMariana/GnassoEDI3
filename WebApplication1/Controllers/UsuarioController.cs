using GnassoEDI3.Applications;
using GnassoEDI3.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IApplication<Usuario> _usuario;

        public UsuarioController(ILogger<UsuarioController> logger, IApplication<Usuario> usuario)
        {
            _logger = logger;
            _usuario = usuario;
        }

        [HttpGet("All")]
        public IActionResult All() => Ok(_usuario.GetAll());

        [HttpGet("ById")]
        public IActionResult ById(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var usuario = _usuario.GetById(id.Value);
            if (usuario is null) return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Crear(Usuario usuario)
        {
            if (!ModelState.IsValid) return BadRequest();
            _usuario.Save(usuario);
            return Ok(usuario.Id);
        }

        [HttpPut]
        public IActionResult Editar(int? id, Usuario usuario)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();

            var usuarioBack = _usuario.GetById(id.Value);
            if (usuarioBack is null) return NotFound();

            usuarioBack.NombreUsuario = usuario.NombreUsuario;
            usuarioBack.Contrasena = usuario.Contrasena;
            usuarioBack.EmpleadoId = usuario.EmpleadoId;

            _usuario.Save(usuarioBack);
            return Ok(usuarioBack);
        }

        [HttpDelete]
        public IActionResult Borrar(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var usuario = _usuario.GetById(id.Value);
            if (usuario is null) return NotFound();

            _usuario.Delete(id.Value);
            return Ok();
        }
    }
}
