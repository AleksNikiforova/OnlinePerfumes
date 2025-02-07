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
        private readonly IRepository<OrderProduct> _productRepository;
        public OrderService(IRepository<Order> repo, IRepository<OrderProduct> productRepository)
        {
            this._repo = repo;
            _productRepository = productRepository;
        }

        /* public bool ValidateOrder(Order order)
         {
             foreach (var item in order.Products)
             {
                 if (!ProductValidator.ProductExists(item.Id))
                 {
                     return false;
                 }
             }
             if (!OrderValidator.ValidateInput(order.OrederDate))
             {
                 return false;
             }
             else
             {
                 return true;
             }
         }*/

        public async Task Add(Order order)
        {
            await _repo.Add(order);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task Update(Order order)
        {
            await _repo.Update(order);
        }

        

        public async Task<Order> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public Task<List<Order>> Find(Expression<Func<Order, bool>> filter)
        {
            return _repo.Find(filter);  
        }

        public IQueryable<Order> GetAll()
        {
           return _repo.GetAll();
        }

        public async Task AddProductToOrder(int orderId, int productId, int quantity)
        {
            var orderProduct = new OrderProduct
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity
            };
            await _productRepository.Add(orderProduct);
        }

        public async Task<Order> GetOrderWithProductsById(int orderId)
        {
            return await _repo.GetAll().Where(x=>x.Id== orderId).Include(x=>x.OrderProducts).ThenInclude(x=>x.Product).FirstOrDefaultAsync();
        }
    }
}
