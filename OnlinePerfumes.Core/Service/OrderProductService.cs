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
    public class OrderProductService : IOrderProductService
    {
        private readonly IRepository<OrderProduct> _repo;

        public OrderProductService(IRepository<OrderProduct> repo)
        {
            _repo = repo;
        }

        public async Task AddAsync(OrderProduct orderProduct)
        {
            await _repo.AddAsync(orderProduct);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(await _repo.GetByIdAsync(id));
        }

        public async Task<List<OrderProduct>> Find(Expression<Func<OrderProduct, bool>> filter)
        {
          return  await _repo.Find(filter);
        }

        public IQueryable<OrderProduct> GetAll()
        {
           return _repo.GetAll();
        }

        public async Task<IEnumerable<OrderProduct>> GetAllAsync()
        {
          return await _repo.GetAllAsync();
        }

        public OrderProduct GetById(int id)
        {
            return _repo.GetById(id);
        }

        public async Task<OrderProduct> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task UpdateAsync(OrderProduct orderProduct)
        {
            await _repo.UpdateAsync(orderProduct);
        }
    }
}
