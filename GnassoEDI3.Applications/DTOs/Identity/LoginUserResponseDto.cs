using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Application.DTOs.Identity
{
    public class LoginUserResponseDto
    {
        public string Token { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Mail { get; set; }
        public bool Login { get; set; }
        public List<string> Errores { get; set; }
        //----------- Para la relacion
        public int? EmpleadoId { get; set; }

    }
}
