using GnassoEDI3.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GnassoEDI3.DataAccess
{
    public class DbContext<T> : IDbContext<T> where T : class, IEntidad
    {

        DbSet<T> _Items;
        DbDataAccess _context;

        public DbContext(DbDataAccess context)
        {
            _context = context;
            _Items = _context.Set<T>();
        }

        public void Delete(int id)
        {
            var entity = _Items.FirstOrDefault(i => i.Id == id);
            if (entity != null) { _Items.Remove(entity); }
            _context.SaveChanges();
        }

        public IList<T> GetAll()
        {
            return _Items.ToList();
        }

        public T GetById(int id)
        {
            return _Items.FirstOrDefault(i => i.Id == id);
        }

        public T Save(T entity)
        {
            if (entity.Id.Equals(0))
            {
                _Items.Add(entity);
            }
            else
            {
                var entityDb = GetById(entity.Id);
                _context.Entry(entityDb).State = EntityState.Detached;
                _Items.Update(entity);
            }
            _context.SaveChanges();
            return entity;
        }
    }
}
