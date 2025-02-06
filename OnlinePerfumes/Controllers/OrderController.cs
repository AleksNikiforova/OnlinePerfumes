using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlinePerfumes.Core;
using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Core.Service;
using OnlinePerfumes.Models;

namespace OnlinePerfumes.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderservice;
        private readonly IService<Product> _productservice;

        public OrderController(IOrderService orderservice,IService<Product> productservice)
        {
            _orderservice = orderservice;
            _productservice = productservice;
        }
        public async Task<IActionResult> Add()
        {
            var products =  _orderservice.GetAll();
            var model = new OrderViewModel()
            {
                Products = products.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    //Text=x.Name
                    
                }).ToList()
            };
            return View(model);

        }
        /*[HttpPost]
        public async Task<IActionResult> Add(OrderViewModel model)
        {
            
        }*/

   
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
