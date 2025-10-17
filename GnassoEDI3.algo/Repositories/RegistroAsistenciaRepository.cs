using GnassoEDI3.DataAccess;
using GnassoEDI3.Entities;
using GnassoEDI3.Repository.IRepositories;
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
    }
}
