using GnassoEDI3.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GnassoEDI3.DataAccess
{
    public class DbDataAccess : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext
    {
        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<RegistroAsistencia> RegistroAsistencias { get; set; }
        public virtual DbSet<ReporteMensual> ReportesMensuales { get; set; }

        public DbDataAccess(DbContextOptions<DbDataAccess> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine).EnableDetailedErrors();
        }

    }
}
