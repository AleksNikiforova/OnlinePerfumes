using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.IServices
{
    public interface ICategoryService:IService<Category>
    {
        Task Add(Category category);
        Task Delete(int id);
        Task Update(Category category);
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(int id);
    }
}
