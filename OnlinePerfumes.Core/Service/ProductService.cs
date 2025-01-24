using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private bool ValidateProduct(Product Product)
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
        }
        public void Add(Product product)
        {
            if (!ValidateProduct(product))
            {
                throw new ArgumentException("The product is not valid");
            }
            else
            {
                _repo.Add(product);
            }
        }
        public void Delete(int id)
        {
            if (ProductValidator.ProductExists(id))
            {
                _repo.Delete(id);
            }
        }
        public void Update(Product product)
        {
            if (!ValidateProduct(product))
            {
                throw new ArgumentException("The product is not valid");
            }
            else
            {
                _repo.Update(product);
            }
        }
        public IEnumerable<Product> GetAll()
        {
            return _repo.GetAll();
        }
        public Product Get(int id)
        {
            return _repo.Get(id);
        }
    }
}
