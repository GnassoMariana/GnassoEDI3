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
    public class ReporteMensualRepository : Repository<ReporteMensual>, IReporteMensualRepository
    {
        private readonly DbDataAccess _context;

        public ReporteMensualRepository(DbDataAccess context) : base(context)
        {
            _context = context;
        }

        public IList<ReporteMensual> GetByEmpleado(int empleadoId)
        {
            return _context.ReportesMensuales
                .Where(r => r.EmpleadoId == empleadoId)
                .ToList();
        }

        public ReporteMensual GetByMes(int empleadoId, int mes, int anio)
        {
            return _context.ReportesMensuales
              .FirstOrDefault(r => r.EmpleadoId == empleadoId && r.Mes == mes && r.Anio == anio)!;

        }

    }
}
