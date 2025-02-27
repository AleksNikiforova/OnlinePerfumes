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
        Task AddAsync(Product product);
        Task DeleteAsync(int id);
        Task UpdateAsync(Product product);
        IQueryable<Product> GetAll();
        Product GetById(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product>GetByIdAsync(int id);
        Task<List<Product>> Find(Expression<Func<Product, bool>> filter);

        // Task NullifyCategories(int id);
    }
}
