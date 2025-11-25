using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Abstractions
{
    public interface ITokenParametros
    {
        string NombreUsuario { get; set; }
        string Email { get; set; }
        string HashContrasena { get; set; }
        string Id { get; set; }

    }
}
