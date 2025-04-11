using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Core.Service;
using OnlinePerfumes.Models;

namespace OnlinePerfumes.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _prodService;
        public CategoryController(ICategoryService categoryService, IProductService prodService)
        {
            _categoryService = categoryService;
            _prodService = prodService;
        }
        public async Task<IActionResult> GetAllCategories()
        {
            return View(await _categoryService.GetAllAsync());
          
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            await _categoryService.AddAsync(category);
            return RedirectToAction("GetAllCategories");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            return View(await _categoryService.GetByIdAsync(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            await _categoryService.UpdateAsync(category);
            return RedirectToAction("GetAllCategories");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var products = await _prodService.Find(p => p.CategoryId == id); // Check if products exist
            if (products.Any())
            {
               
                TempData["Error"] = "Category cannot be deleted because it has associated products.";
                return RedirectToAction("GetAllCategories"); // Prevent deletion
            }

            await _categoryService.DeleteAsync(id); // Only delete if no products exist
            TempData["Success"] = "Category deleted successfully.";
            return RedirectToAction("GetAllCategories");
        }

        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return View(null);
            }

            var category = await _categoryService.GetCategoryByName(name);
            if (category == null)
            {
                ViewData["Error"] = $"Category with name '{name}' not found.";
                return View(null);
            }

            return View(category);

        }

    }
}
