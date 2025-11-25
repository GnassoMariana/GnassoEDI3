using GnassoEDI3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Repository.IRepositories
{
    public interface IRegistroAsistenciaRepository : IRepository<RegistroAsistencia>
    {
        IList<RegistroAsistencia> GetByEmpleado(int empleadoId);
        IList<RegistroAsistencia> GetByMes(int empleadoId, int mes, int anio);
        decimal CalcularHorasTotales(int empleadoId, int mes, int anio);

        Task<RegistroAsistencia> FirmarEntradaAsync(Guid userGuid);
    }
}
