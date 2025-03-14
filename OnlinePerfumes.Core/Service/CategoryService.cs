using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Core.Validators;
using OnlinePerfumes.DataAccess.Repository;
using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.Service
{
    public class CategoryService:ICategoryService
    {
        private readonly IRepository<Category> _repo;
        private readonly IRepository<Product> _productrepo;

        public CategoryService(IRepository<Category> repo, IRepository<Product> productrepo)
        {
            _repo = repo;
            _productrepo = productrepo;
        }

        public async Task AddAsync(Category category)
        {
           await _repo.AddAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            var category=await _repo.GetByIdAsync(id);
            if (_productrepo == null)
            {
                throw new Exception("_productrepo е NULL! Увери се, че е правилно инжектиран в конструктора.");
            }
            var products = await _productrepo.Find(p => p.CategoryId == id);
            foreach (var product in products)
            {
                product.CategoryId = null; // Позволява продуктите да останат без категория
                await _productrepo.UpdateAsync(product);
            }

            await _repo.DeleteAsync(category);
        }

        public async Task<List<Category>> Find(Expression<Func<Category, bool>> filter)
        {
           return await _repo.Find(filter);
        }

        public IQueryable<Category> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public Category GetById(int id)
        {
           return _repo.GetById(id);
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Category> GetByIdAsync(int? id)
        {

            return await _repo.GetByIdAsync(id ?? default(int));
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            IEnumerable<Category> all = await _repo.GetAllAsync();
            Category category = all.Where(x => x.Name == name).FirstOrDefault();
            return category;
        }

        public async Task UpdateAsync(Category category)
        {
            await _repo.UpdateAsync(category);
        }
    }
}
