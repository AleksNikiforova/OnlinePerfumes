using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Models;

namespace OnlinePerfumes.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService,ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var list=await _productService.GetAll();
            return View(list);  
        }
        public async Task<IActionResult> Add()
        {
            var categories = await _categoryService.GetAll();
            ViewBag.Categories = new SelectList(categories,"Id","CategoryName");
            return View();

        }
        [HttpPost]
        public async Task<IActionResult>Add(Product product)
        {
           
            //if (ModelState.IsValid)
            //{
                await _productService.Add(product);
                return RedirectToAction("Index");
            //}
            //return View();
           
        }
        public async Task<IActionResult> Update(int id)
        {
            var entity = await _productService.GetById(id);
            return View(entity);
        }
        [HttpPost]
        public async Task<IActionResult>Update(Product product)
        {
            if(ModelState.IsValid)
            {
                await _productService.Update(product);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Delete(int id)
        {
            await _productService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
