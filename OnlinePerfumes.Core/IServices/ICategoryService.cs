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
        void Add(Category category);
        void Delete(int id);
        void Update(Category category);
        IEnumerable<Category> GetAll();
        Category Get(int id);
    }
}
