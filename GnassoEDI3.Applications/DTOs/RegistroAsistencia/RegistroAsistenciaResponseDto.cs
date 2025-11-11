using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Application.DTOs.RegistroAsistencia
{
    public class RegistroAsistenciaResponseDto
    {
        public int Id { get; set; }

        public int EmpleadoId { get; set; }

        public string Fecha { get; set; }

        public string HoraEntrada { get; set; }

        public string HoraSalida { get; set; }

        public decimal HorasTrabajadas { get; set; }

        public string Estado { get; set; }
    }
}
