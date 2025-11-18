using GnassoEDI3.Entities;
using GnassoEDI3.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.AspNet.Identity.EntityFramework;

namespace GnassoEDI3.DataAccess
{
    public class DbDataAccess : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public virtual DbSet<Empleado> Empleados { get; set; }
        //public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<RegistroAsistencia> RegistroAsistencias { get; set; }
        public virtual DbSet<ReporteMensual> ReportesMensuales { get; set; }

        public DbDataAccess(DbContextOptions<DbDataAccess> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine).EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            //----------------- Para registrar la relacion con rol
            modelBuilder.Entity<User>()
                        .HasOne(u => u.Empleado)
                        .WithOne()
                        .HasForeignKey<User>(u => u.EmpleadoId)
                        .OnDelete(DeleteBehavior.Restrict);

        }


    }
}
