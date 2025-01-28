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
        public void Add(T entity)
        {
            _repo.Add(entity);
        }

        public void Delete(int id)
        {
             _repo.Delete(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _repo.GetAll();
        }

        public T GetById(int id)
        {
            return _repo.Get(id);
        }

        public void Update(T entity)
        {
            _repo.Update(entity);
        }
    }
}
