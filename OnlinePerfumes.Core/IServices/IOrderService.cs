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
        void Add(Order order);
        void Delete(int id);
        void Update(Order order);
        IEnumerable<OrderProduct> GetAll();
        OrderProduct Get(int id);
    }
}
