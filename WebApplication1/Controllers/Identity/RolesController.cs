using AutoMapper;
using GnassoEDI3.Application.DTOs.Identity;
using GnassoEDI3.Entities.MicrosoftIdentity;
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
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<RolesController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public RolesController(RoleManager<Role> roleManager
            , ILogger<RolesController> logger
            , IMapper mapper, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var roles = _roleManager.Roles.ToList();
            var dto = _mapper.Map<IList<RoleResponseDto>>(roles);
            return Ok(dto);
            //return Ok(_mapper.Map<IList<RoleResponseDto>>(_roleManager.Roles.ToList()));
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Guardar([FromBody] RoleRequestDto roleRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest("Datos inválidos.");

            var role = _mapper.Map<Role>(roleRequestDto);
            if (string.IsNullOrWhiteSpace(role.Name)) return BadRequest("Nombre de rol obligatorio.");

            var exists = await _roleManager.RoleExistsAsync(role.Name);
            if (exists) return Conflict($"El rol '{role.Name}' ya existe.");

            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded) return Problem(detail: result.Errors.First().Description, instance: role.Name, statusCode: StatusCodes.Status409Conflict);

            return Ok(role.Id);
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Modificar([FromBody] RoleRequestDto roleRequestDto, [FromQuery] Guid id)
        {
            if (ModelState.IsValid)
            {
                var userId = Guid.Parse(User.FindFirst("Id")?.Value);
                try
                {
                    var role = _mapper.Map<Role>(roleRequestDto);
                    role.Id = id;
                    var result = _roleManager.UpdateAsync(role).Result;
                    if (result.Succeeded)
                    {
                        return Ok(role.Id);
                    }
                    return Problem(detail: result.Errors.First().Description, instance: role.Name, statusCode: StatusCodes.Status409Conflict);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return BadRequest("Los datos enviados no son validos.");
            }
        }

        [Route("GetById")]
        [HttpGet]
        public IActionResult GetById(Guid? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    return BadRequest();
                }
                var role = _roleManager.FindByIdAsync(id.Value.ToString());
                if (role == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<RoleResponseDto>(role));
            }
            catch (Exception ex)
            {
                return Conflict();
            }
        }

        [HttpPost("AssignToUser")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AssignToUser([FromBody] RoleAssignDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.UserId) || string.IsNullOrWhiteSpace(dto.RoleName))
                return BadRequest("UserId y RoleName son obligatorios.");

            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null) return NotFound("Usuario no encontrado.");

            if (!await _roleManager.RoleExistsAsync(dto.RoleName))
                await _roleManager.CreateAsync(new Role { Name = dto.RoleName });

            if (await _userManager.IsInRoleAsync(user, dto.RoleName))
                return BadRequest("Usuario ya tiene ese rol.");

            var res = await _userManager.AddToRoleAsync(user, dto.RoleName);
            if (!res.Succeeded) return BadRequest(res.Errors.Select(e => e.Description));

            return Ok(new { user = user.UserName, role = dto.RoleName });
        }

        public class RoleAssignDto
        {
            public string UserId { get; set; } = string.Empty;
            public string RoleName { get; set; } = string.Empty;
        }

    }
}
