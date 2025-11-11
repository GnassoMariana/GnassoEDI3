using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Application.DTOs.Empleado
{
    public class EmpleadoRequestDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(30)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(10)]
        public string Dni { get; set; }

        [Required]
        public int Jornada { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal SalarioBase { get; set; }

        public bool TrabajadorActivo { get; set; } = true;

        [Range(0, int.MaxValue)]
        public int Antiguedad { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaAlta { get; set; }
    }
}
