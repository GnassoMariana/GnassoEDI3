using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Application.DTOs.RegistroAsistencia
{
    public class RegistroAsistenciaRequestDto
    {
        public int Id { get; set; }

        [Required]
        public int EmpleadoId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime HoraEntrada { get; set; }

        [DataType(DataType.Time)]
        public DateTime HoraSalida { get; set; }

        [Range(0, 24)]
        public decimal HorasTrabajadas { get; set; }

        [Required]
        public int Estado { get; set; }
    }
}
