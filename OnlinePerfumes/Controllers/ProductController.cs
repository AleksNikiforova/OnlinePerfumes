using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlinePerfumes.Core;
using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Models;
using OnlinePerfumes.Models.ViewModels;

namespace OnlinePerfumes.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IService<Order> _orderService;
        public ProductController(IProductService productService,ICategoryService categoryService,IService<Order> orderService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _orderService = orderService;
        }
        
        public async Task<IActionResult> Index()
        {
           
            var products=await _productService.GetAll().ToListAsync();
            var viewModel=products.Select(x=>new ProductViewModel
            {
                Id=x.Id,
                Name=x.Name,
                Price=x.Price
                
            }).ToList();
            return View(viewModel);
            /*var products =  _productService.GetAll();
            var query = products.AsQueryable();
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
            return View(model);*/
        }


        public IActionResult Add()
        {
            var categories =  _categoryService.GetAll();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Add(ProductViewModel model)
        {
            if(ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    StockQuantity = model.Quantity,
                    CategoryId=model.CategoryId
                };
                await _productService.Add(product);
                return RedirectToAction("Index");
            }
            var categories=await _categoryService.GetAll().ToListAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            return View(model);
        }
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Quantity = product.StockQuantity,
                CategoryId=product.CategoryId
            };
            var categories = await _categoryService.GetAll().ToListAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name", product.CategoryId);

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult>Update(ProductViewModel model)
        {
            if(ModelState.IsValid)
            {
                var product=await _productService.GetById(model.Id);
                if(product == null)
                {
                    return NotFound();
                }
                product.Name = model.Name;
                product.Price = model.Price;
                product.Description = model.Description;
                product.StockQuantity = model.Quantity;
                await _productService.Update(product);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult>Delete(int id)
        {
            var product = await _productService.GetById(id);
            if(product == null)
            {
                return NotFound();
            }
            await _productService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
