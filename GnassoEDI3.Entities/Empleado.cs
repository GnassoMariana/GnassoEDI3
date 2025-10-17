using GnassoEDI3.Abstractions;
using GnassoEDI3.Enums;
using System.ComponentModel.DataAnnotations;

namespace GnassoEDI3.Entities
{
    public class Empleado: IEntidad
    {
        public Empleado()
        {
            Asistencias = new HashSet<RegistroAsistencia>();
            Reportes = new HashSet<ReporteMensual>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(30)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(15)]
        public string Dni { get; set; }

        [Required]
        public Jornada Jornada { get; set; } //Enum: Completa (1), Parcial (2), Media (3)

        [DataType(DataType.Currency)]
        public decimal SalarioBase { get; set; }

        public bool TrabajadorActivo { get; set; }

        public int Antiguedad { get; set; }  

        [DataType(DataType.Date)]
        public DateTime FechaAlta { get; set; }

        //1-N
        public virtual ICollection<RegistroAsistencia> Asistencias { get; set; }
        public virtual ICollection<ReporteMensual> Reportes { get; set; }

        // 1-1
        public virtual Usuario Usuario { get; set; }
    }
}

