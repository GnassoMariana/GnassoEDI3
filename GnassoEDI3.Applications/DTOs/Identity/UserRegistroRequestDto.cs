using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Application.DTOs.Identity
{
    public class UserRegistroRequestDto
    {
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Contrasena { get; set; }
        [DataType(DataType.Date)]
        public DateTime? FechaNacimiento { get; set; }

        
        [Required]
        public int EmpleadoId { get; set; }

    }
}
