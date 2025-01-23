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
        void Add(OrderStatus orderStatus);
        void Delete(int id);
        void Update(OrderStatus orderStatus);
        IEnumerable<OrderStatus> GetAll();
        OrderStatus Get(int id);
    }
}
