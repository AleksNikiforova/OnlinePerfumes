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
        public async Task Add(T entity)
        {
            await dbset.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
           var entity = await dbset.FindAsync(id);
            if (entity == null)
            {
                throw new ArgumentException("Entity is null");
            }
            dbset.Remove(entity);
            await this._context.SaveChangesAsync();
        }

        public async Task<T> GetById(int id)
        {
            var entity=await dbset.FindAsync(id);
            if(entity == null)
            {
                throw new ArgumentException("id is null");
            }
            return entity;

        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await dbset.ToListAsync();
        }

        public async Task Update(T entity)
        {
           dbset.Update(entity);  
           await _context.SaveChangesAsync();

        }
    }
}
