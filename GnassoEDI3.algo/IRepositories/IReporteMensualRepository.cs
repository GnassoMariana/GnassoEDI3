using GnassoEDI3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Repository.IRepositories
{
    public interface IReporteMensualRepository : IRepository<ReporteMensual>
    {
        IList<ReporteMensual> GetByEmpleado(int empleadoId);
        ReporteMensual GetByMes(int empleadoId, int mes, int anio);
    }
}
