using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Application.DTOs.Usuario
{
    public class UsuarioResponseDto
    {
        public int Id { get; set; }

        public string NombreUsuario { get; set; }

        public string Mail { get; set; }

        public string Rol { get; set; } 

        public int EmpleadoId { get; set; }
    }
}
