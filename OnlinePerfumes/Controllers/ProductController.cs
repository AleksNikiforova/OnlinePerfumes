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
        private readonly IOrderProductService _orderProductService;
        public ProductController(IProductService productService,ICategoryService categoryService, CloudinaryService cloudinaryService, IOrderProductService orderProductService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _cloudinaryService = cloudinaryService;
            _orderProductService = orderProductService; 
        }

        public async Task<IActionResult> Index()
        {
           
            var products=await _productService.GetAllAsync();
            var productViewModel=new List<ProductAllViewModel>();
            foreach (var product in products)
            {
                var category=await _categoryService.GetByIdAsync(product.CategoryId);
                /*if (category == null)
                {
                    throw new Exception($"Category with ID {product.CategoryId} not found.");
                }*/
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var categories = await _categoryService.GetAllAsync();
            var viewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Aroma = product.Aroma,
                CategoryId = product.CategoryId,
                StockQuantity = product.StockQuantity,
                ImagePath = product.ImagePath,
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>Edit(ProductViewModel model)
        {
            var product = await _productService.GetByIdAsync(model.Id);
            if (product == null)
            {
                return NotFound();
            }

            // Ако потребителят е качил ново изображение, качваме го в Cloudinary
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadResult = await _cloudinaryService.UploadImageAsync(model.ImageFile);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    product.ImagePath = uploadResult; // Запазваме новото URL
                }
            }

            // Актуализираме останалите полета
            product.Name = model.Name;
            product.Price = model.Price;
            product.Aroma = model.Aroma;
            product.CategoryId = model.CategoryId;
            product.StockQuantity = model.StockQuantity;

            await _productService.UpdateAsync(product);
            TempData["Success"] = "Промените са запазени успешно";
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>Delete(int id)
        {

            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var hasOrders = await _orderProductService.Find(op => op.ProductId == id);

            if (hasOrders.Any())
            {
                TempData["ErrorMessage"] = "Не може да изтриеш този продукт, защото има свързани поръчки.";
                return RedirectToAction("Index");
            }
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteAsync(id); // Истинско изтриване
            return RedirectToAction("Index");
        }
    }
}
