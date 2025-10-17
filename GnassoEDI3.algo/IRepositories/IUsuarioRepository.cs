using GnassoEDI3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Repository.IRepositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario GetByNombreUsuario(string nombreUsuario);
        Usuario Login(string nombreUsuario, string contrasena);
    }
}
