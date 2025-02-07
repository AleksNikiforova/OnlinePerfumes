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
        public async Task<IActionResult> Details(int id)
        {
            var order=await _orderservice.GetOrderWithProductsById(id);
            if(order == null)
            {
                return NotFound();
            }
            var viewModel = new OrderViewModel
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Products = order.OrderProducts.Select(op => new ProductViewModel
                {
                    Id = op.ProductId,
                    Name = op.Product.Name,
                    Price = op.Product.Price,
                    Quantity = op.Quantity
                }).ToList()
            };
            return View(viewModel);

        }
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
        public async Task<IActionResult> AddProduct(int orderid,int productid,int quantity)
        {
            await _orderservice.AddProductToOrder(orderid, productid, quantity);
            return RedirectToAction("Details", new { id = orderid, });
        }
        public async Task<IActionResult> Update(int id)
        {
            var order = await _orderservice.GetById(id);
            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult>Update(Order order)
        {
            await _orderservice.Update(order);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult>Delete(int id)
        {
            await _orderservice.Delete(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Index()
        {
            var list = _orderservice.GetAll();
            return View(list);
        }
    }
}
