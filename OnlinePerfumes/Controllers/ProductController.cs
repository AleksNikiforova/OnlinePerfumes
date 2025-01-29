using Microsoft.AspNetCore.Mvc;
using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Models;

namespace OnlinePerfumes.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult<Product>>Add(Product product)
        {
           if(ModelState.IsValid)
           {
                _productService.Add(product);
           }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
