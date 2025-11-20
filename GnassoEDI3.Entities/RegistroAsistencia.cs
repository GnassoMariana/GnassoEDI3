
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
        #region Properties
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Empleado))]
        public int EmpleadoId { get; set; }


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
        #endregion
        #region Virtual
        [JsonIgnore]
        public virtual Empleado Empleado { get; set; }

        #endregion

        #region Getters y setters
        public void SetEmpleadoId(int empleadoId)
        {
            if (empleadoId <= 0)
                throw new ArgumentException("El Id del empleado no puede ser cero o negativo.");
            EmpleadoId = empleadoId;
        }
        public void SetFecha(DateTime fecha)
        {
            if (fecha > DateTime.Now)
                throw new ArgumentException("La fecha de asistencia no puede ser futura.");
            Fecha = fecha.Date;
        }

        public void SetHoraEntrada(DateTime hora)
        {
            HoraEntrada = hora;
            CalcularHorasTrabajadas();
        }

        public void SetHoraSalida(DateTime hora)
        {
            if (hora < HoraEntrada)
                throw new ArgumentException("La hora de salida no puede ser anterior a la hora de entrada.");
            HoraSalida = hora;
            CalcularHorasTrabajadas();
        }

        public void SetEstado(Estado estado)
        {
            Estado = estado;
        }
        #endregion
        private void CalcularHorasTrabajadas()
        {
            if (HoraSalida != default && HoraEntrada != default && HoraSalida >= HoraEntrada)
            {
                HorasTrabajadas = (decimal)(HoraSalida - HoraEntrada).TotalHours;
            }
        }

    }
}

