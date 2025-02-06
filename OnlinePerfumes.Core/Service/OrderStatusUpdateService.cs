using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.DataAccess.Repository;
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
        private readonly IRepository<OrderStatusUpdate> _repo;
        public OrderStatusUpdateService(IRepository<OrderStatusUpdate> repo)
        {
            _repo = repo;
        }

        public async Task Add(OrderStatusUpdate orderStatusUpdate)
        {
            await _repo.Add(orderStatusUpdate);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task<List<OrderStatusUpdate>> Find(Expression<Func<OrderStatusUpdate, bool>> filter)
        {
            return await _repo.Find(filter);
        }

        public IQueryable<OrderStatusUpdate> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task<OrderStatusUpdate> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task Update(OrderStatusUpdate orderStatusUpdate)
        {
            await _repo.Update(orderStatusUpdate);
        }
    }
}
