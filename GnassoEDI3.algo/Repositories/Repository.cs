using GnassoEDI3.Abstractions;
using GnassoEDI3.DataAccess;
using GnassoEDI3.Repository.IRepositories;
using Microsoft.EntityFrameworkCore; 

namespace GnassoEDI3.Repository.Repositories
{
   
    public class Repository<T> : IRepository<T> where T : class, IEntidad
    {
        //IDbContext<T> _context;
        protected readonly DbDataAccess _context;
        protected readonly DbSet<T> _dbSet;
        public Repository(DbDataAccess context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
            //_context.Delete(id);
        }

        public virtual IList<T> GetAll()
        {
            //return _context.GetAll();
            return _dbSet.ToList();
        }

        public virtual T GetById(int id)
        {
            //return _context.GetById(id);
            return _dbSet.FirstOrDefault(e => e.Id == id)!;
        }


        public virtual T Save(T entity)
        {
            if (entity.Id == 0) _dbSet.Add(entity);
            else _dbSet.Update(entity);
            _context.SaveChanges();
            return entity;
            //return _context.Save(entity);
        }
    }
}
