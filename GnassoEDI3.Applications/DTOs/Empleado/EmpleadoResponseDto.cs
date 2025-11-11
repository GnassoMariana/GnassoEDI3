using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Application.DTOs.Empleado
{
    public class EmpleadoResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Jornada { get; set; } 
        public decimal SalarioBase { get; set; }
        public bool TrabajadorActivo { get; set; }
        public int Antiguedad { get; set; }
        public string FechaAlta { get; set; }
    }
}
