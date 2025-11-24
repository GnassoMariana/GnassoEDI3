using GnassoEDI3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Repository.IRepositories
{
    public interface IEmpleadoRepository : IRepository<Empleado>
    {
       
        Empleado GetByDni(string dni);
        IList<Empleado> GetActivos();
        Empleado GetByUserId(Guid userId);
    }
}
