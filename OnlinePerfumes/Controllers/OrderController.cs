using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlinePerfumes.Core;
using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Core.Service;
using OnlinePerfumes.Models;
using OnlinePerfumes.Models.ViewModels;

namespace OnlinePerfumes.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderservice;
        private readonly IProductService _productservice;

        public OrderController(IOrderService orderservice,IProductService productservice)
        {
            _orderservice = orderservice;
            _productservice = productservice;
        }
        /*[HttpGet]
        public IActionResult Details(int id)
        {
           

        }*/
        public async Task<IActionResult> Add()
        {
            var products = await _productservice.GetAll().ToListAsync();
            if (products == null || !products.Any())
            {
                throw new ArgumentException("No found products");
            }

            ViewBag.Products = products;
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult>Update(Order order)
        {
            await _orderservice.UpdateAsync(order);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult>Delete(int id)
        {
            await _orderservice.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Index()
        {
            var list = _orderservice.GetAll();
            return View(list);
        }
    }
}
