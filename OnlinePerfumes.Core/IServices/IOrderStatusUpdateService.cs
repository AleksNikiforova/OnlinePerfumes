using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.IServices
{
    public interface IOrderStatusUpdateService:IService<OrderStatusUpdate>
    {
        Task Add(OrderStatusUpdate orderStatusUpdate);
        Task Delete(int id);
        Task Update(OrderStatusUpdate orderStatusUpdate);
        Task<IEnumerable<OrderStatusUpdate>> GetAll();
        Task<OrderStatusUpdate> GetById(int id);
    }
}
