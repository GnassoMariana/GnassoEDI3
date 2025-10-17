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
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRep)
        {
            _usuarioRepository = usuarioRep;
        }

        public IList<Usuario> GetUsuarios()
        {
            return _usuarioRepository.GetAll();
        }

        public Usuario GetUsuarioById(int id)
        {
            return _usuarioRepository.GetById(id);
        }

        public Usuario SaveUsuario(Usuario usuario)
        {
            return _usuarioRepository.Save(usuario);
        }

        public void DeleteUsuario(int id)
        {
            _usuarioRepository.Delete(id);
        }
    }
}
