using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.Service
{
    public class CategoryService:ICategoryService
    {
        private readonly IRepository<Category> _repo;
        public CategoryService(IRepository<Category> repo)
        {
            this._repo = repo;
        }
        public void Add(Category category)
        {
            if (CategoryValidator.ValidateInput(category.Name))
            {
                _repo.Add(category);
            }
            else
            {
                throw new ArgumentException("The category already exists!");
            }
        }
        public void Delete(int id)
        {
            if (CategoryValidator.CategoryExist(id))
            {
                _repo.Delete(id);
            }
            else
            {
                throw new ArgumentException("The category already exists!");
            }
        }
        public void Update(Category category)
        {
            if (!CategoryValidator.CategoryExist(category.Id))
            {
                throw new ArgumentException("The category is not valid!");
            }
            _repo.Update(category);
        }
        public IEnumerable<Category> GetAll()
        {
            return _repo.GetAll();
        }
       public  Category Get(int id)
        {
            return _repo.Get(id);
        }
    }
}
