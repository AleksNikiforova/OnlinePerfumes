using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlinePerfumes.Core;
using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Core.Service;
using OnlinePerfumes.Models;
using OnlinePerfumes.Models.ViewModels.Product;

namespace OnlinePerfumes.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly CloudinaryService _cloudinaryService;
        public ProductController(IProductService productService,ICategoryService categoryService, CloudinaryService cloudinaryService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<IActionResult> Index()
        {
           
            var products=await _productService.GetAllAsync();
            var productViewModel=new List<ProductAllViewModel>();
            foreach (var product in products)
            {
                var category=await _categoryService.GetByIdAsync(product.CategoryId);
                var viewModel = new ProductAllViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Aroma = product.Aroma,
                    Price = product.Price,
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                    StockQuantity = product.StockQuantity,
                    ImagePath = product.ImagePath
                };
                productViewModel.Add(viewModel);
            }
            return View(productViewModel);
        }

        [HttpGet]
    
        public async Task<IActionResult> CreateProduct()
        {

            var categories = await _categoryService.GetAllAsync();
            var viewModel = new ProductViewModel
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
       
        public async Task<IActionResult>CreateProduct(ProductViewModel model,string ImageURL)
        {
            // Ако има качено изображение, ще се качи в Cloudinary 
            if (model.ImageFile != null)
            {
                var uploadedImageUrl = await _cloudinaryService.UploadImageAsync(model.ImageFile);
                if (!string.IsNullOrEmpty(uploadedImageUrl))
                {
                    model.ImagePath = uploadedImageUrl;
                }
            }
            else if (!string.IsNullOrEmpty(model.ImagePath))
            // Проверяваме дали е въведен URL 
            {
                model.ImagePath = model.ImagePath;
                // Запазваме URL 
            }
            else
            { 
                ModelState.AddModelError("ImageFile", "Please upload an image or provide an image URL.");
                var categories = await _categoryService.GetAllAsync();
                model.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
                return View(model); // Връщаме формата с грешка 
            }
            if (!string.IsNullOrEmpty(ImageURL))
            {
                    model.ImagePath = ImageURL;
            }
                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Aroma = model.Aroma,
                    CategoryId = model.CategoryId,
                    StockQuantity= model.StockQuantity,
                    ImagePath = model.ImagePath
                };
                await _productService.AddAsync(product);
                TempData["Success"] = "Парфюма е добавен успешно";
                return RedirectToAction("Index");
        }
       
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var categories = await _categoryService.GetAllAsync();
            var viewModel = new ProductViewModel
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        
        public async Task<IActionResult>Update(ProductViewModel model)
        {
            if(ModelState.IsValid)
            {
                var product=await _productService.GetByIdAsync(model.Id);
                if(product == null)
                {
                    return NotFound();
                }
                product.Name = model.Name;
                product.Price = model.Price;
                await _productService.UpdateAsync(product);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
       
        public async Task<IActionResult>Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if(product == null)
            {
                return NotFound();
            }
            await _productService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
