
using GnassoEDI3.Abstractions;
using GnassoEDI3.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GnassoEDI3.Entities
{
    public class RegistroAsistencia: IEntidad
    {
        public RegistroAsistencia()
        {
            // Inicializar luego AsistenciaReportes
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Empleado))]
        public int EmpleadoId { get; set; }

        [JsonIgnore]
        public virtual Empleado Empleado { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [DataType(DataType.Time)]
        public DateTime HoraEntrada { get; set; }

        [DataType(DataType.Time)]
        public DateTime HoraSalida { get; set; }

        [DataType(DataType.Duration)]
        public decimal HorasTrabajadas { get; set; }

        [Required]
        public Estado Estado { get; set; }
    }
}

