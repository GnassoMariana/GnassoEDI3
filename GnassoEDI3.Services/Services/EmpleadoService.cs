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
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        public EmpleadoService(IEmpleadoRepository empleadoRep)
        {
            _empleadoRepository = empleadoRep;
        }

        public Empleado GetEmmpleadoById(int id)
        {
            return _empleadoRepository.GetById(id);

        }

        public Empleado SaveEmpleado(Empleado empleado)
        {
            return _empleadoRepository.Save(empleado);
        }

        public void DeleteEmmpleado(int id)
        {
             _empleadoRepository.Delete(id);
        }

        public IList<Empleado> GetEmpleado()
        {
            return _empleadoRepository.GetAll();
        }
    }
}
