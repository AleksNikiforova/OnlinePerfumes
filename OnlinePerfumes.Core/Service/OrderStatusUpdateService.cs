using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.Service
{
    public class OrderStatusUpdateService : IOrderStatusUpdateService
    {
        public async Task Add(OrderStatusUpdate orderStatusUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderStatusUpdate>> Find(Expression<Func<OrderStatusUpdate, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderStatusUpdate>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<OrderStatusUpdate> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(OrderStatusUpdate orderStatusUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
