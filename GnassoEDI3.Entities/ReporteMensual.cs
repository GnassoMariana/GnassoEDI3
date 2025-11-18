using GnassoEDI3.Abstractions;
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
    public class ReporteMensual: IEntidad
    {
        public ReporteMensual()
        {
            //Inicializar luego ReportesEpleados, AsistenciasMes
        }

        #region Properties
        [Key]
        public int Id { get; set; }

        
        [Required]
        [ForeignKey(nameof(Empleado))]
        public int EmpleadoId { get; set; }

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
        #endregion

        #region Virtual
        [JsonIgnore]
        public virtual Empleado Empleado { get; set; }


        #endregion

        #region Setters y getters
        public void SetEmpleadoId(int empleadoId)
        {
            if (empleadoId <= 0)
                throw new ArgumentException("El Id del empleado no puede ser cero o negativo.");
            EmpleadoId = empleadoId;
        }

        public void SetMes(int mes)
        {
            if (mes < 1 || mes > 12)
                throw new ArgumentException("El mes debe estar entre 1 y 12.");
            Mes = mes;
        }

        public void SetAnio(int anio)
        {
            if (anio < 1900 || anio > DateTime.Now.Year)
                throw new ArgumentException($"El año debe ser entre 1900 y {DateTime.Now.Year}.");
            Anio = anio;
        }

        public void SetHorasTotales(decimal horas)
        {
            if (horas < 0)
                throw new ArgumentException("Las horas totales no pueden ser negativas.");
            HorasTotales = horas;
        }

        public void SetSueldoFinal(decimal sueldo)
        {
            if (sueldo < 0)
                throw new ArgumentException("El sueldo final no puede ser negativo.");
            SueldoFinal = sueldo;
        }

        public void SetDiasAusencias(int dias)
        {
            if (dias < 0)
                throw new ArgumentException("Los días de ausencias no pueden ser negativos.");
            DiasAusencias = dias;
        }

        public void SetDiasRetraso(int dias)
        {
            if (dias < 0)
                throw new ArgumentException("Los días de retraso no pueden ser negativos.");
            DiasRetraso = dias;
        }

        public void SetDiasVacaciones(int dias)
        {
            if (dias < 0)
                throw new ArgumentException("Los días de vacaciones no pueden ser negativos.");
            DiasVacaciones = dias;
        }

        public void SetPremioAsistencia(bool premio)
        {
            PremioAsistencia = premio;
        }
        #endregion
    }
}

