using GnassoEDI3.Application.DTOs.Identity;
using GnassoEDI3.Entities.MicrosoftIdentity;
using GnassoEDI3.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GnassoEDI3.Web.Controllers.Identity
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UsersController> _logger;
        public UsersController(RoleManager<Role> roleManager
            , UserManager<User> userManager
            , ILogger<UsersController> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("AddRoleToUser")]
        public async Task<IActionResult> Guardar(string userId, Rol rolEnum)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            //var role = _roleManager.FindByIdAsync(rolEnum).Result;
            //if (user is not null && role is not null)
            //{
            //    var status = await _userManager.AddToRoleAsync(user, role.Name);
            //    if (status.Succeeded)
            //    {
            //        return Ok(new { user = user.UserName, rol = role.Name });
            //    }
            //}
            //return BadRequest(new { userId = userId, roleId = roleId });
            if (user == null) return BadRequest("Usuario no encontrado.");

            var rolAString = rolEnum.ToString();
            var resultado = await _userManager.AddToRoleAsync (user, rolAString);
            if(resultado.Succeeded)
            {
                return Ok(new {user = user.UserName, rolEnum = rolAString});
            }
            else
            {
                return BadRequest(resultado.Errors.Select(e => e.Description));
            }
        }

        [HttpGet("GetUsuarios")]
        public async Task<ActionResult<List<UserRegistroResponseDto>>> GetUsuarios()
        {
            var users = _userManager.Users
                .Select(u => new UserRegistroResponseDto
                {
                    //NombreCompleto = u.NombreCompleto,
                    Email = u.Email,
                    NombreUsuario = u.UserName
                })
                .ToList();

            return Ok(users);
        }
        [AllowAnonymous]
        [HttpPost("Regiatro")]
        public async Task<ActionResult<UserRegistroResponseDto>> Registro([FromBody] UserRegistroRequestDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new User
            {
                //NombreCompleto = model.NombreCompleto,
                Email = model.Email,
                UserName = model.Nombres,
                NormalizedEmail = model.Email?.ToUpperInvariant(),
                NormalizedUserName = model.Nombres?.ToUpperInvariant()
            };

            var result = await _userManager.CreateAsync(user, model.Contrasena);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors) ModelState.AddModelError(err.Code, err.Description);
                return ValidationProblem(ModelState);
            }
            var dto = new UserRegistroResponseDto
            {
                //NombreCompleto = user.NombreCompleto,
                Email = user.Email,
                NombreUsuario = user.UserName
            };

            return CreatedAtAction(nameof(GetUsuarios), dto);
        }
    }

}

