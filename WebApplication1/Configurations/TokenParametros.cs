using GnassoEDI3.Abstractions;

namespace GnassoEDI3.Web.Configurations
{
    public class TokenParametros : ITokenParametros
    {
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string HashContrasena { get; set; }
        public string Id { get; set; }
    }
}
