using Microsoft.Identity.Client;
using OnlinePerfumes.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repo;

        public async Task AddAsync(T entity)
        {
            await _repo.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity=await _repo.GetByIdAsync(id);
            if (entity != null)
            {
                await _repo.DeleteAsync(entity);
            }
        }

        public IQueryable<T> GetAll()
        {
           return _repo.GetAll();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return _repo.GetAllAsync();
        }

        public T GetById(int id)
        {
            return _repo.GetById(id);   
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            await _repo.UpdateAsync(entity);
        }
    }
}
