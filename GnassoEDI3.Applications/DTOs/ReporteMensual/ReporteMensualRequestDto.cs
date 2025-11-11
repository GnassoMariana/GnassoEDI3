using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Application.DTOs.ReporteMensual
{
    public class ReporteMensualRequestDto
    {
        public int Id { get; set; }

        [Required]
        public int EmpleadoId { get; set; }

        [Required]
        [Range(1, 12)]
        public int Mes { get; set; }

        [Required]
        public int Anio { get; set; }

        [Range(0, double.MaxValue)]
        public decimal HorasTotales { get; set; }

        [Range(0, double.MaxValue)]
        public decimal SueldoFinal { get; set; }

        public bool PremioAsistencia { get; set; }

        [Range(0, 31)]
        public int DiasAusencias { get; set; }

        [Range(0, 31)]
        public int DiasRetraso { get; set; }

        [Range(0, 31)]
        public int DiasVacaciones { get; set; }
    }
}
