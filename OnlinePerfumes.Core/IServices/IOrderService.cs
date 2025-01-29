using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.IServices
{
    public interface IOrderService:IService<Order>
    {
        Task Add(Order order);
        Task Delete(int id);
        Task Update(Order order);
        Task<IEnumerable<Order>> GetAll();
        Task<Order> GetById(int id);
    }
}
