﻿using OnlinePerfumes.Core.IServices;
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

        public async Task AddAsync(Category category)
        {
           await _repo.AddAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(await _repo.GetByIdAsync(id));
        }

        public IQueryable<Category> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public Category GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
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
