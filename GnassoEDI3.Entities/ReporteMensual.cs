using GnassoEDI3.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Entities
{
    public class ReporteMensual: IEntidad
    {
        public ReporteMensual()
        {
            //Inicializar luego ReportesEpleados, AsistenciasMes
        }

        [Key]
        public int Id { get; set; }

        
        [Required]
        [ForeignKey(nameof(Empleado))]
        public int EmpleadoId { get; set; }
        public virtual Empleado Empleado { get; set; }

        [Required]
        [Range(1, 12)]
        public int Mes { get; set; }

        [Required]
        public int Anio { get; set; }

        [DataType(DataType.Duration)]
        public decimal HorasTotales { get; set; }

        [DataType(DataType.Currency)]
        public decimal SueldoFinal { get; set; }

        public bool PremioAsistencia { get; set; } 

        public int DiasAusencias { get; set; }

        public int DiasRetraso { get; set; }

        public int DiasVacaciones { get; set; }

    }
}

