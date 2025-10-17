using GnassoEDI3.Entities;
using GnassoEDI3.Repository.IRepositories;
using GnassoEDI3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Services.Services
{
    public class ReporteMensualService : IReporteMensualService
    {
        private readonly IReporteMensualRepository _reporteRepository;

        public ReporteMensualService(IReporteMensualRepository reporteRep)
        {
            _reporteRepository = reporteRep;
        }

        public IList<ReporteMensual> GetReportesMensuales()
        {
            return _reporteRepository.GetAll();
        }

        public ReporteMensual GetReporteMensualById(int id)
        {
            return _reporteRepository.GetById(id);
        }

        public ReporteMensual SaveReporteMensual(ReporteMensual reporte)
        {
            return _reporteRepository.Save(reporte);
        }

        public void DeleteReporteMensual(int id)
        {
            _reporteRepository.Delete(id);
        }
    }
}
