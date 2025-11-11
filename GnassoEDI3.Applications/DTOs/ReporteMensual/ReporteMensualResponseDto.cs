using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Application.DTOs.ReporteMensual
{
    public class ReporteMensualResponseDto
    {
        public int Id { get; set; }

        public int EmpleadoId { get; set; }

        public string Mes { get; set; } 

        public int Anio { get; set; }

        public decimal HorasTotales { get; set; }

        public string SueldoFinal { get; set; } 

        public bool PremioAsistencia { get; set; }

        public int DiasAusencias { get; set; }

        public int DiasRetraso { get; set; }

        public int DiasVacaciones { get; set; }
    }
}
