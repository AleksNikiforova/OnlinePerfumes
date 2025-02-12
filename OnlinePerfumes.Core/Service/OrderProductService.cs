using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.DataAccess.Repository;
using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.Service
{
    public class OrderProductService : IOrderProductService
    {
        private readonly IRepository<OrderProduct> _repo;
        public async Task AddAsync(OrderProduct orderProduct)
        {
            await _repo.AddAsync(orderProduct);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(await _repo.GetByIdAsync(id));
        }

        public IQueryable<OrderProduct> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderProduct>> GetAllAsync()
        {
          return await _repo.GetAllAsync();
        }

        public OrderProduct GetById(int id)
        {
            throw new NotImplementedException();
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
