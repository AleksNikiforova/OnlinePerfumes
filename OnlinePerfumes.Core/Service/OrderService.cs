using Microsoft.EntityFrameworkCore;
using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Core.Validators;
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
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _repo;
        //private readonly IRepository<OrderProduct> _productRepository;
        public OrderService(IRepository<Order> repo)
        {
            this._repo = repo;
            
        }

        public async Task AddAsync(Order order)
        {
            await _repo.AddAsync(order);
        }

        public async Task DeleteAsync(int id)
        {
           await _repo.DeleteAsync(await _repo.GetByIdAsync(id));
        }

        public async Task<List<Order>> Find(Expression<Func<Order, bool>> filter)
        {
            return await _repo.Find(filter);
         
        }

        public async Task<List<Order>> FindWithIncludesAsync(Expression<Func<Order, bool>> filter)
        {
            return await _repo.GetAll()
           .Where(filter)
           .Include(o => o.OrderProducts)
               .ThenInclude(op => op.Product)
                   .ThenInclude(p => p.Category)
           .ToListAsync();
        }

        public IQueryable<Order> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public Order GetById(int id)
        {
            return _repo.GetById(id);
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            if (id == null)
            {
                return null;
            }

            var order = await _repo
                              .GetAll() // Get IQueryable from the repository
                              .Where(o => o.Id == id)
                              .Include(o => o.OrderProducts) // Eagerly load OrderProducts
                              .ThenInclude(op => op.Product) // Eagerly load Product details
                              .ThenInclude(p => p.Category)
                              .FirstOrDefaultAsync();

            return order;
        }

        public async Task<Order> GetOrderById(int? id)
        {
            var query = _repo
                      .GetAll() // Get IQueryable from the repository
                      .Where(o => o.Id == id)
                      .Include(o => o.OrderProducts) // Eagerly load OrderProducts
                      .ThenInclude(op => op.Product) // Eagerly load Product details
                      .ThenInclude(p => p.Category);
            return await query.FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Order order)
        {
           await _repo.UpdateAsync(order);
            
        }

       
    }
}
