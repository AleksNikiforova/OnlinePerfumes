﻿using Microsoft.AspNetCore.Authorization;
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
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> GetAllCategories()
        {
            return View(await _categoryService.GetAllAsync());
          
        }
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
            await _categoryService.DeleteAsync(id);
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
