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
        void Add(OrderStatusUpdate orderStatusUpdate);
        void Delete(int id);
        void Update(OrderStatusUpdate orderStatusUpdate);
        IEnumerable<OrderStatusUpdate> GetAll();
        OrderStatusUpdate Get(int id);
    }
}
