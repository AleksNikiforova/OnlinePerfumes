using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.IServices
{
    public interface ICustomerrService:IService<Customer>
    {
        Task Add(Customer user);
        Task Delete(int id);
        Task Update(Customer user);
        IQueryable<Customer> GetAll();
        Task<Customer> GetById(int id);
        Task<List<Customer>> Find(Expression<Func<Customer, bool>> filter);
    }
}
