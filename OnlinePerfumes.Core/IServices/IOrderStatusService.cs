using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.IServices
{
    public interface IOrderStatusService:IService<OrderStatus>
    {
        
        Task Add(OrderStatus orderStatus);
        Task Delete(int id);
        Task  Update(OrderStatus orderStatus);
        Task<IEnumerable<OrderStatus>> GetAll();
        Task<OrderStatus> GetById(int id);
    }
}
