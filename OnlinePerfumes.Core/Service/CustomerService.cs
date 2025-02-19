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
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _repo;

        public CustomerService(IRepository<Customer> repo)
        {
            _repo = repo;
        }

        public async Task AddAsync(Customer customer)
        {
            await _repo.AddAsync(customer);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(await _repo.GetByIdAsync(id));
        }

        public IQueryable<Customer> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
           return await _repo.GetAllAsync();
        }

        public Customer GetById(int id)
        {
            return _repo.GetById(id);
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
           return await _repo.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Customer customer)
        {
           await _repo.UpdateAsync(customer);
        }
    }
}
