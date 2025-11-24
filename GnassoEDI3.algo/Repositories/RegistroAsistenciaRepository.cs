using GnassoEDI3.DataAccess;
using GnassoEDI3.Entities;
using GnassoEDI3.Enums;
using GnassoEDI3.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Repository.Repositories
{
    public class RegistroAsistenciaRepository : Repository<RegistroAsistencia>, IRegistroAsistenciaRepository
    {
        private readonly DbDataAccess _context;

        public RegistroAsistenciaRepository(DbDataAccess context) : base(context)
        {
            _context = context;
        }

        public IList<RegistroAsistencia> GetByEmpleado(int empleadoId)
        {
            return _context.RegistroAsistencias
                .Where(r => r.EmpleadoId == empleadoId)
                .ToList();
        }

        public IList<RegistroAsistencia> GetByMes(int empleadoId, int mes, int anio)
        {
            return _context.RegistroAsistencias
                .Where(r => r.EmpleadoId == empleadoId &&
                            r.Fecha.Month == mes &&
                            r.Fecha.Year == anio)
                .ToList();
        }

        public decimal CalcularHorasTotales(int empleadoId, int mes, int anio)
        {
            return GetByMes(empleadoId, mes, anio).Sum(r => r.HorasTrabajadas);
        }

        public async Task<RegistroAsistencia> FirmarEntradaAsync(Guid userGuid)
        {
            var empleado = await _context.Empleados.FirstOrDefaultAsync(e => e.UserId == userGuid);
            if (empleado == null) throw new InvalidOperationException("Empleado no coincide con el usuario");

            var today = DateTime.Today;
            var registrosHoy = await _context.RegistroAsistencias
                .Where(r => r.EmpleadoId == empleado.Id && r.Fecha.Date == today)
                .ToListAsync();

            var abierto = registrosHoy.FirstOrDefault(r => r.HoraSalida == default(DateTime) || r.Estado == Estado.Presente);
            if (abierto != null)
                throw new InvalidOperationException("Ya se firmo la entrada  para hoy.");

            var nuevo = new RegistroAsistencia();
            nuevo.SetEmpleadoId(empleado.Id);
            nuevo.SetFecha(DateTime.Now.Date);
            nuevo.SetHoraEntrada(DateTime.Now);
            nuevo.SetEstado(Estado.Presente);

            var saved = _context.RegistroAsistencias.Add(nuevo);
            await _context.SaveChangesAsync();
            return saved.Entity;
        }
    }
}
