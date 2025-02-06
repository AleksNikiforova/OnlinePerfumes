using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.IServices
{
    public interface IProductService:IService<Product>
    {
        Task Add(Product product);
        Task Delete(int id);
        Task Update(Product product);
        IQueryable<Product> GetAll();
        Task<Product> GetById(int id);
        Task<List<Product>> Find(Expression<Func<Product, bool>> filter );
    }
}
