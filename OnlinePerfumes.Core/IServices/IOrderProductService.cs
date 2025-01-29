using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.IServices
{
    public interface IOrderProductService:IService<OrderProduct>
    {
        Task Add(OrderProduct orderProduct);
        Task Delete(int id);
        Task Update(OrderProduct orderProduct);
        Task<IEnumerable<OrderProduct>> GetAll();
        Task<OrderProduct> GetById(int id);
    }
}
