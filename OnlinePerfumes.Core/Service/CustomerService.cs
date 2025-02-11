using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.DataAccess.Repository;
using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.Service
{
    public class CustomerService : ICustomerrService
    {
        private readonly IRepository<Customer> _repo; 
        public CustomerService(IRepository<Customer> repo)
        {
            _repo = repo;
        }

        public async Task Add(Customer user)
        {
            await _repo.Add(user);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task<List<Customer>> Find(Expression<Func<Customer, bool>> filter)
        {
           return await _repo.Find(filter);
        }

        public IQueryable<Customer> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task<Customer> GetById(int id)
        {
           return await _repo.GetById(id);
        }

        public async Task Update(Customer user)
        {
            await _repo.Update(user);
        }

       
    }
}
