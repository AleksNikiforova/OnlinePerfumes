using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.IServices
{
    public interface IProductService:IService<Product>
    {
        void Add(Product product);
        void Delete(int id);
        void Update(Product product);
        IEnumerable<Product> GetAll();
        Product Get(int id);
    }
}
