using GnassoEDI3.Application.DTOs.Identity;
using GnassoEDI3.Entities.MicrosoftIdentity;
using GnassoEDI3.Enums;
using GnassoEDI3.Services.AuthServices;
using GnassoEDI3.Web.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GnassoEDI3.Web.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<EmpleadosController> _logger;
        private readonly ITokenHandlerService _servicioToken;
        private readonly RoleManager<Role> _roleManager;

        public AuthController(
            UserManager<User> userManager,
            ILogger<EmpleadosController> logger,
            ITokenHandlerService servicioToken,
            RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _logger = logger;
            _servicioToken = servicioToken;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UserRegistroRequestDto user)
        {
            if (ModelState.IsValid)
            {
                var existeUsuario = await _userManager.FindByEmailAsync(user.Email);
                if (existeUsuario != null)
                {
                    return BadRequest("Existe un usuario registrado con el mal " + user.Email + ".");
                }
                var Creado = await _userManager.CreateAsync(new User()
                {
                    Email = user.Email,
                    UserName = user.Email.Substring(0, user.Email.IndexOf('@')),
                    Nombres = user.Nombres,
                    Apellidos = user.Apellidos,
                    FechaNacimiento = user.FechaNacimiento,
                    EmpleadoId = user.EmpleadoId     // <--- ASOCIACIÓN
                }, user.Contrasena);
                if (Creado.Succeeded)
                {
                    var nombreRol = Rol.Empleado.ToString(); // ejemplo
                    var existeRol = await _roleManager.RoleExistsAsync(nombreRol);

                    if (!existeRol)
                        await _roleManager.CreateAsync(new Role { Name = nombreRol });

                    await _userManager.AddToRoleAsync( user, nombreRol);
                    return Ok(new UserRegistroResponseDto
                    {
                        NombreCompleto = string.Join(" ", user.Nombres, user.Apellidos),
                        Email = user.Email,
                        NombreUsuario = user.Email.Substring(0, user.Email.IndexOf('@'))
                    });
                }
                else
                {
                    return BadRequest(Creado.Errors.Select(e => e.Description).ToList());
                }
            }
            else
            {
                return BadRequest("Los datos enviados no son validos.");
            }
        }

        [HttpPost]
        [Route("RegisterSincronico")]
        public IActionResult RegistrarUsuarioincronico([FromBody] UserRegistroRequestDto user)
        {
            if (ModelState.IsValid)
            {
                var existeUsuario = _userManager.FindByEmailAsync(user.Email).Result;
                if (existeUsuario != null)
                {
                    return BadRequest("Existe un usuario registrado con el mal " + user.Email + ".");
                }
                var Creado = _userManager.CreateAsync(new User()
                {
                    Email = user.Email,
                    UserName = user.Email.Substring(0, user.Email.IndexOf('@')),
                    Nombres = user.Nombres,
                    Apellidos = user.Apellidos,
                    FechaNacimiento = user.FechaNacimiento,
                    EmpleadoId = user.EmpleadoId     // <--- ASOCIACIÓN
                }, user.Contrasena).Result;
                if (Creado.Succeeded)
                {
                    return Ok(new UserRegistroResponseDto
                    {
                        NombreCompleto = string.Join(" ", user.Nombres, user.Apellidos),
                        Email = user.Email,
                        NombreUsuario = user.Email.Substring(0, user.Email.IndexOf('@'))
                    });
                }
                else
                {
                    return BadRequest(Creado.Errors.Select(e => e.Description).ToList());
                }
            }
            else
            {
                return BadRequest("Los datos enviados no son validos.");
            }

        }


        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserRequestDto userlogin)
        {
            if (ModelState.IsValid)
            {
                var existeUsuario = await _userManager.FindByEmailAsync(userlogin.Email);
                if (existeUsuario != null)
                {
                    var isCorrect = await _userManager.CheckPasswordAsync(existeUsuario, userlogin.Contrasena);
                    if (isCorrect)
                    {
                        try
                        {
                            var parametros = new TokenParametros()
                            {
                                Id = existeUsuario.Id.ToString(),
                                HashContrasena = existeUsuario.PasswordHash,
                                NombreUsuario = existeUsuario.UserName,
                                Email = existeUsuario.Email
                            };
                            var jwt = _servicioToken.GenerateJwtTokens(parametros);
                            return Ok(new LoginUserResponseDto()
                            {
                                Login = true,
                                Token = jwt,
                                NombreUsuario = existeUsuario.UserName,
                                Mail = existeUsuario.Email,
                                 EmpleadoId = existeUsuario.EmpleadoId   // <--- agrega relacion
                            });
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                }
            }
            return BadRequest(new LoginUserResponseDto()
            {
                Login = false,
                Errores = new List<string>()
                    {
                       "Usuario o contraseña incorrecto!"
                    }
            });
        }

    }
}
