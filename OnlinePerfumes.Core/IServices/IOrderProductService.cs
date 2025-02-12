using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.IServices
{
    public interface IOrderProductService:IService<OrderProduct>
    {
        Task AddAsync(OrderProduct orderProduct);
        Task DeleteAsync(int id);
        Task UpdateAsync(OrderProduct orderProduct);
        IQueryable<OrderProduct> GetAll();
        OrderProduct GetById(int id);
        Task<IEnumerable<OrderProduct>> GetAllAsync();
        Task<OrderProduct> GetByIdAsync(int id);
    }
}
