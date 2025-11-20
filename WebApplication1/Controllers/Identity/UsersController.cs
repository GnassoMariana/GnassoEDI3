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
            , UserManager<User> userManage
            , ILogger<UsersController> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManage;
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
    }

}

