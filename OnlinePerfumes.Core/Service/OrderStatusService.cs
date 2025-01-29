using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.Service
{
    public class OrderStatusService : IOrderStatusService
    {
        public async Task Add(OrderStatus orderStatus)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderStatus>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<OrderStatus> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(OrderStatus orderStatus)
        {
            throw new NotImplementedException();
        }
    }
}
