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
    public class RegistroAsistenciaService : IRegistroAsistenciaService
    {
        private readonly IRegistroAsistenciaRepository _registroRepository;

        public RegistroAsistenciaService(IRegistroAsistenciaRepository registroRep)
        {
            _registroRepository = registroRep;
        }

        public IList<RegistroAsistencia> GetRegistroAsistencias()
        {
            return _registroRepository.GetAll();
        }

        public RegistroAsistencia GetRegistroAsistenciaById(int id)
        {
            return _registroRepository.GetById(id);
        }

        public RegistroAsistencia SaveRegistroAsistencia(RegistroAsistencia registro)
        {
            return _registroRepository.Save(registro);
        }

        public void DeleteRegistroAsistencia(int id)
        {
            _registroRepository.Delete(id);
        }
    }
}
