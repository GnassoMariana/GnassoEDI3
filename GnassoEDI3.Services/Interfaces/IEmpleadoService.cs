using GnassoEDI3.Entities;

namespace GnassoEDI3.Services.Interfaces
{
    public interface IEmpleadoService
    {
        IList<Empleado> GetEmpleado();
        Empleado GetEmmpleadoById(int id);
        Empleado SaveEmpleado(Empleado empleado);
        void DeleteEmmpleado(int id);
    }
}
