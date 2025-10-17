using GnassoEDI3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Services.Interfaces
{
    public interface IUsuarioService
    {
        IList<Usuario> GetUsuarios();
        Usuario GetUsuarioById(int id);
        Usuario SaveUsuario(Usuario usuario);
        void DeleteUsuario(int id);
    }
}
