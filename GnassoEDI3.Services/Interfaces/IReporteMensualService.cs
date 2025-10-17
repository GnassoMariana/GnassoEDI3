using GnassoEDI3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Services.Interfaces
{
    public interface IReporteMensualService
    {
        IList<ReporteMensual> GetReportesMensuales();
        ReporteMensual GetReporteMensualById(int id);
        ReporteMensual SaveReporteMensual(ReporteMensual reporte);
        void DeleteReporteMensual(int id);
    }
}
