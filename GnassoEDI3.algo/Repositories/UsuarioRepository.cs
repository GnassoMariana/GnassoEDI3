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
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly DbDataAccess _context;

        public UsuarioRepository(DbDataAccess context) : base(context)
        {
            _context = context;
        }

        public Usuario GetByNombreUsuario(string nombreUsuario)
        {
            return _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == nombreUsuario)!;
        }

        public Usuario Login(string nombreUsuario, string contrasena)
        {
            return _context.Usuarios.FirstOrDefault(u =>
                u.NombreUsuario == nombreUsuario && u.Contrasena == contrasena)!;
        }
    }
}
