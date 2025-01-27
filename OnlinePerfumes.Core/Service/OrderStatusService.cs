using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.Service
{
    public class OrderStatusService:IOrderStatusService
    {
        public void Add(OrderStatus orderStatus)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderStatus orderStatus)
        {
            throw new NotImplementedException();
        }

        IEnumerable<OrderStatus> IOrderStatusService.GetAll()
        {
            throw new NotImplementedException();
        }

        OrderStatus IOrderStatusService.Get(int id)
        {
            throw new NotImplementedException();
        }

        public OrderStatus GetById(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<OrderStatus> IService<OrderStatus>.GetAll()
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
