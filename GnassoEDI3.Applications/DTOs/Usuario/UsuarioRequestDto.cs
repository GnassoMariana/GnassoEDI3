using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Application.DTOs.Usuario
{
    public class UsuarioRequestDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string NombreUsuario { get; set; }

        [Required]
        [StringLength(30)]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Mail { get; set; }

        [Required]
        public int Rol { get; set; }

        [Required]
        public int EmpleadoId { get; set; }
    }
}
