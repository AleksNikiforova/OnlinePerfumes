using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.Service
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _repo;
        public OrderService(IRepository<Order> repo)
        {
            this._repo = repo;
        }
        public bool ValidateOrder(Order order)
        {
            foreach (var item in order.Products)
            {
                if (!ProductValidator.ProductExists(item.Id))
                {
                    return false;
                }
            }
            if (!OrderValidator.ValidateInput(order.OrderDate))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Add(Order order)
        {
            if (ValidateOrder(order) == true)
            {
                _repo.Add(order);
            }
            else
            {
                throw new ArgumentException("Validation didn't pass!");
            }
        }
        public void Delete(int id)
        {
            if (OrderValidator.OrderExist(id))
            {
                _repo.Delete(id);
            }
            else
            {
                throw new ArgumentException("This order doesn't exist.");
            }
        }
        public void Update(Order order)
        {
            if (!ValidateOrder(order))
            {
                throw new ArgumentException("Validation didn't pass!");
            }
            if (!OrderValidator.OrderExist(order.Id))
            {
                throw new ArgumentException("Order doesn't exist.");
            }
            else
            {
                _repo.Update(order);
            }
        }
        public IEnumerable<Order> GetAll()
        {
            return _repo.GetAll();

        }
        public Order Get(int id)
        {
            return _repo.Get(id);
        }
    }
}
