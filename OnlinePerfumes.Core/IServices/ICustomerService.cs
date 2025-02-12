using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.IServices
{
    public interface ICustomerService:IService<Customer>
    {
        Task AddAsync(Customer customer);
        Task DeleteAsync(int id);
        Task UpdateAsync(Customer customer);
        IQueryable<Customer> GetAll();
        Customer GetById(int id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int id);
    }
}
