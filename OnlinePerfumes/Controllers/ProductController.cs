using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlinePerfumes.Core;
using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Models;

namespace OnlinePerfumes.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IService<Category>_categoryService;
        public ProductController(IProductService productService,ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        
        public async Task<IActionResult> Index(ProductFilterViewModel? filter)
        {
            var prods = _productService.Find(filter).AsQuerable();
            if (filter == null)
            {
                var prodList = prods.AsQueryable();
                return View(prods);
            }

            //var prods = await _productService.GetAll();
            var query = prods.AsQueryable();
            //var query = await _productService.GetAll().AsQueryable();
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
                query=query.Where(p=>p.SearchName.Contains(filter.SearchName));
            }
            var productList = await query.ToListAsync(); 

            return View(productList);
            // var list=await _productService.GetAll();
            // return View(list);  
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
