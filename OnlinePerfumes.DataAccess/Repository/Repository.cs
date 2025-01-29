using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbset;

        public Repository(ApplicationDbContext context)
        {
            this._context = context;
            this.dbset = _context.Set<T>();
        }

        public void Add(T entity)
        {
            dbset.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            T obj=dbset.Find(id);
            dbset.Remove(obj);
            _context.SaveChanges();
        }

        public T Get(int id)
        {
            T obj=dbset.Find(id);
            return obj;
        }

        public IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public void Update(T entity)
        {
           dbset.Update(entity);
           _context.SaveChanges();
        }
    }
}
