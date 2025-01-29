using OnlinePerfumes.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repo;
        public Service(IRepository<T> repo)
        {
            this._repo = repo;
        }
        public async Task Add(T entity)
        {
            await _repo.Add(entity);
        }

        public async Task Delete(int id)
        {
           await _repo.Delete(id);
        }

       public  async  Task<IEnumerable<T>> GetAll()
       {
           return await _repo.GetAll();
       }

       public async Task<T> GetById(int id)
       {
            return await _repo.GetById(id);
       }

        public async Task Update(T entity)
        {
            await _repo.Update(entity);
        }
    }
}
