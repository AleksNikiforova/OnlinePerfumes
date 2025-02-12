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
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repo;
        public ProductService(IRepository<Product> repo)
        {
            this._repo = repo;
        }

        public async Task AddAsync(Product product)
        {
           await _repo.AddAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(await _repo.GetByIdAsync(id));
        }

        

        public IQueryable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Product product)
        {
            await _repo.UpdateAsync(product);
        }
    }
}
