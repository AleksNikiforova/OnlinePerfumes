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
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IRepository<OrderStatus> _repo;
        public OrderStatusService(IRepository<OrderStatus> repo)
        {
            _repo = repo;
        }

        public async Task Add(OrderStatus orderStatus)
        {
            await _repo.Add(orderStatus);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task<List<OrderStatus>> Find(Expression<Func<OrderStatus, bool>> filter)
        {
          return await _repo.Find(filter);
        }

        public IQueryable<OrderStatus> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task<OrderStatus> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task Update(OrderStatus orderStatus)
        {
            await _repo.Update(orderStatus);
        }
    }
}
