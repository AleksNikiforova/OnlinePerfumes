using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Core.Validators;
using OnlinePerfumes.DataAccess.Repository;
using OnlinePerfumes.Models;
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
        
        public async Task Add(Category category)
        {
            await _repo.Add(category);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task Update(Category category)
        {
            await _repo.Update(category);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Category> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        
    }
}
