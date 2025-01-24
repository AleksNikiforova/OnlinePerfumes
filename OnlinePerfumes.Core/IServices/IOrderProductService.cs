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
        void Add(OrderProduct orderProduct);
        void Delete(int id);
        void Update(OrderProduct orderProduct);
        IEnumerable<OrderProduct> GetAll();
        OrderProduct Get(int id);
    }
}
