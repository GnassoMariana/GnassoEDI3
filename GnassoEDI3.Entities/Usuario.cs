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
    public class Usuario: IEntidad
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string NombreUsuario { get; set; }

        [Required]
        [StringLength(30)]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Mail { get; set; }

        [Required]
        public Rol Rol { get; set; }   // Enum: Empleado (1), Gerenrte (2)

        // 1–1 
        [ForeignKey(nameof(Empleado))]
        public int EmpleadoId { get; set; }

        [JsonIgnore]
        public virtual Empleado? Empleado { get; set; }
    }

}

