using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> dbset;

        public Repository(ApplicationDbContext context)
        {
            this._context = context;
            this.dbset = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await dbset.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            dbset.Remove(entity);
            await _context.SaveChangesAsync();

        }

        public async Task<List<T>> Find(Expression<Func<T, bool>> filter)
        {
            return await dbset.Where(filter).ToListAsync();
        }

        public IQueryable<T> GetAll()
        {
            return dbset.AsQueryable();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbset.ToListAsync();
        }

        public T GetById(int id)
        {
            return dbset.Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
           return await dbset.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            dbset.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
