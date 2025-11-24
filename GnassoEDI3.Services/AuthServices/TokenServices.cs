using GnassoEDI3.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Services.AuthServices
{
    public interface ITokenHandlerService
    {
        string GenerateJwtTokens(ITokenParametros parametros, IEnumerable<string> roles = null);
    }

    public class TokenServices : ITokenHandlerService
    {
        private readonly JwtConfig _jwtConfig;
        private readonly ILogger<TokenServices>? _logger;

        public TokenServices(IOptionsMonitor<JwtConfig> optionsMonitor, ILogger<TokenServices> logger)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
            _logger = logger;
        }

        public string GenerateJwtTokens(ITokenParametros parametros, IEnumerable<string> roles = null)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, parametros.Id),
                new Claim("Id", parametros.Id),
                new Claim(JwtRegisteredClaimNames.Sub, parametros.Id),
                new Claim(JwtRegisteredClaimNames.Name, parametros.NombreUsuario),
                new Claim(JwtRegisteredClaimNames.Email, parametros.Email)
            };
            if (roles != null)
            {
                claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
            }

            _logger?.LogInformation("Roles del usuario {UserId}: {Roles}", parametros.Id, string.Join(',', roles));
            _logger?.LogDebug("Generando JWT para {User} roles: {Roles}", parametros.NombreUsuario, roles == null ? "(none)" : string.Join(",", roles));
            _logger?.LogDebug("Claims antes de crear token: {Claims}", string.Join(", ", claims.Select(c => $"{c.Type}={c.Value}")));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}

