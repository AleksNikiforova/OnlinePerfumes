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
        public async Task<IActionResult> Index()
        {
            var list=await _productService.GetAll();
            return View(list);  
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Add(Product product)
        {
            if(ModelState.IsValid)
            {
                await _productService.Add(product);
                return RedirectToAction("Index");
            }
            return View();
           
        }
        public async Task<IActionResult>Update(int id)
        {
            var product=await _productService.Get(id);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult>Update(Product product)
        {
            if(ModelState.IsValid)
            {
                await _productService.Update(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult>Delete(int id)
        {
            await _productService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
