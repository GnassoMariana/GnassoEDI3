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
    public class EmpleadoRepository : Repository<Empleado>, IEmpleadoRepository
    {
        private readonly DbDataAccess _context;

        public EmpleadoRepository(DbDataAccess context) : base(context)
        {
            _context = context;
        }

        public Empleado GetByDni(string dni)
        {
            return _context.Empleados.FirstOrDefault(e => e.Dni == dni);
        }

        public IList<Empleado> GetActivos()
        {
            return _context.Empleados.Where(e => e.TrabajadorActivo).ToList();
        }
    }
}
