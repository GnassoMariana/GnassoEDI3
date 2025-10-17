using GnassoEDI3.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnassoEDI3.Repository.IRepositories
{
    public interface IRepository<T> : IDbOperation<T>
    {
        IList<T> GetAll();
        T GetById(int id);
        T Save(T entity);
        void Delete(int id);
    }
}
