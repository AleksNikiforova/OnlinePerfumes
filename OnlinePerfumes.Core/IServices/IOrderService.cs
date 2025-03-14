using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.IServices
{
    public interface IOrderService:IService<Order>
    {
        Task AddAsync(Order order);
        Task DeleteAsync(int id);
        Task UpdateAsync(Order order);
        IQueryable<Order> GetAll();
        Order GetById(int id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order>GetByIdAsync(int id);
        Task<Order>GetOrderById(int id);
      


    }
}
