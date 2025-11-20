using GnassoEDI3.Abstractions;
using GnassoEDI3.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GnassoEDI3.Entities
{
    public class Empleado: IEntidad
    {
        public Empleado()
        {
            Asistencias = new HashSet<RegistroAsistencia>();
            Reportes = new HashSet<ReporteMensual>();
        }

        #region Properties
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
        #endregion
        //1-N

        #region Virtuals
        [JsonIgnore]
        public virtual ICollection<RegistroAsistencia>? Asistencias { get; set; }

        [JsonIgnore]
        public virtual ICollection<ReporteMensual>? Reportes { get; set; }

        //// 1-1
        //[JsonIgnore]
        //public virtual Usuario? Usuario { get; set; }
        #endregion

        #region Getters y setters
        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío.");
            if (nombre.Length > 30)
                throw new ArgumentException("El nombre no puede tener más de 30 caracteres.");

            if (!Regex.IsMatch(nombre, @"^[a-zA-Z\s]+$"))
                throw new ArgumentException("El nombre solo puede contener letras y espacios.");

            Nombre = nombre;
        }

        public void SetApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido no puede estar vacío.");
            if (apellido.Length > 30)
                throw new ArgumentException("El apellido no puede tener más de 30 caracteres.");

            if (!Regex.IsMatch(apellido, @"^[a-zA-Z\s]+$"))
                throw new ArgumentException("El apellido solo puede contener letras y espacios.");

            Apellido = apellido;
        }

        public void SetDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new ArgumentException("El DNI no puede estar vacío.");
            if (!Regex.IsMatch(dni, @"^\d{1,8}$"))
                throw new ArgumentException("El DNI solo puede contener números y no más de 8 dígitos.");
            Dni = dni;
        }

        public void SetJornada(Jornada jornada)
        {
            Jornada = jornada;
        }

        public void SetSalarioBase(decimal salario)
        {
            if (salario < 0)
                throw new ArgumentException("El salario no puede ser negativo.");
            SalarioBase = salario;
        }

        public void SetTrabajadorActivo(bool activo)
        {
            TrabajadorActivo = activo;
        }

        public void SetAntiguedad(int antiguedad)
        {
            if (antiguedad < 0)
                throw new ArgumentException("La antigüedad no puede ser negativa.");
            Antiguedad = antiguedad;
        }

        public void SetFechaAlta(DateTime fecha)
        {
            if (fecha > DateTime.Now)
                throw new ArgumentException("La fecha de alta no puede ser futura.");
            FechaAlta = fecha;
        }

       
        public string GetNombreCompleto()
        {
            return $"{Apellido}, {Nombre}";
        }
        #endregion
    }
}

