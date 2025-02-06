using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlinePerfumes.Core;
using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Models;

namespace OnlinePerfumes.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IService<Category>_categoryService;
        public ProductController(IProductService productService,IService<Category> categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        
        public async Task<IActionResult> Index(ProductFilterViewModel? filter)
        {
           
            var query = await _productService.GetAll().AsQueryable();
            if (filter.CategoryId != null)
            {
                query=query.Where(p=>p.CategoryId == filter.CategoryId.Value);
            }
            if(filter.MinPrice != null)
            {
                query=query.Where(p=>p.Price>=filter.MinPrice.Value);
            }
            if(filter.MaxPrice != null)
            {
                query=query.Where(p=>p.Price<=filter.MaxPrice.Value);
            }
            if (!string.IsNullOrEmpty(filter.SearchName))
            {
                query=query.Where(p=>p.Name.Contains(filter.SearchName));
            }
            var productList =  query.ToList();

            // return View(productList);
            var model = new ProductFilterViewModel
            {
                CategoryId = filter.CategoryId,
                MinPrice = filter.MinPrice,
                MaxPrice = filter.MaxPrice,
                SearchName = filter.SearchName,
                Categories = new SelectList((System.Collections.IEnumerable)_categoryService.GetAll(), "CategoryId", "Name"),
                Products = query.Include(p => p.Category).ToList()
            };
            return View(model);
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
