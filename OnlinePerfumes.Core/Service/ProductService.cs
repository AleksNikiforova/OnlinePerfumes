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
    public class ProductService:IProductService 
    {
        private readonly IRepository<Product> _repo;
        public ProductService(IRepository<Product> repo)
        {
            this._repo = repo;
        }
        /*private bool ValidateProduct(Product Product)
        {
            if (!ProductValidator.ValidateInput(Product.Name, Product.Price))
            {
                return false;
            }
            else if (!CategoryValidator.CategoryExist(Product.CategoryId))
            {
                return false;
            }
            else
            {
                return true;
            }
        }*/


        public async Task Add(Product product)
        {
            await _repo.Add(product);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task Update(Product product)
        {
            await _repo.Update(product);
        }

        public async Task<List<Product>> GetAll()
        {
           return await _repo.GetAll();
        }


        public async Task<Product> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task<List<Product>> Find(Expression<Func<Product, bool>> filter)
        {
            return await _repo.Find(filter);
        }
    }
}
