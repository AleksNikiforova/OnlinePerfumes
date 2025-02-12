using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.IServices
{
    public interface ICategoryService : IService<Category>
    {
        Task AddAsync(Category category);
        Task DeleteAsync(int id);
        Task UpdateAsync(Category category);
        IQueryable<Category> GetAll();
        Category GetById(int id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task<Category> GetCategoryByName(string name);
    }
}
