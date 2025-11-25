using GnassoEDI3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Services.Interfaces
{
    public interface IRegistroAsistenciaService
    {
        IList<RegistroAsistencia> GetRegistroAsistencias();
        RegistroAsistencia GetRegistroAsistenciaById(int id);
        RegistroAsistencia SaveRegistroAsistencia(RegistroAsistencia registro);
        void DeleteRegistroAsistencia(int id);
        Task<RegistroAsistencia> FirmarEntradaAsync(Guid userGuid);
    }
}
