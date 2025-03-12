using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlinePerfumes.Core;
using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Core.Service;
using OnlinePerfumes.Models;
using OnlinePerfumes.Models.ViewModels;
using OnlinePerfumes.Models.ViewModels.Customer;
using OnlinePerfumes.Models.ViewModels.Order;
using OnlinePerfumes.Models.ViewModels.OrderProduct;

namespace OnlinePerfumes.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderservice;
        private readonly IProductService _productservice;
        private readonly ICustomerService _customerservice;
        private readonly IOrderProductService _orderProductservice;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(IOrderService orderservice,IProductService productservice, ICustomerService customerservice, IOrderProductService orderproductservice,UserManager<IdentityUser> userManager)
        {
            _orderservice = orderservice;
            _productservice = productservice;
            _customerservice = customerservice;
            _orderProductservice = orderproductservice;
            _userManager = userManager;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task< IActionResult> All()
        {
            var model = _orderProductservice.GetAll().Include(x => x.Product).Include(x => x.Order).ThenInclude(x => x.Customer).Select(x => new OrderAllViewModel()
            {
                CustomerName = x.Order.Customer.FirstName,
                OrderDate = x.Order.OrderDate.ToString(),
                Price = x.Product.Price,
                ProductName = x.Product.Name,
                Id = x.Order.Id
            }).ToList();
            return View(model);

        }
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddOrder()
        {
            var model = new OrderAddViewModel();
            model.ProductsList =new SelectList(_productservice.GetAll(),"Id","Name");
            model.CustomerList = new SelectList(_customerservice.GetAll(), "Id", "FirstName");
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult>AddOrder(OrderAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ProductsList = new SelectList(_productservice.GetAll(), "Id", "Name");
                model.CustomerList = new SelectList(_customerservice.GetAll(), "Id", "FirstName");
                return View(model);
            }
            Order order = new Order()
            {
                OrderDate = DateTime.Now,
                CustomerId = model.CustomerId
            };
            await _orderservice.AddAsync(order);

            OrderProduct orderProduct = new OrderProduct()
            {
                OrderId = order.Id,
                ProductId = model.ProductId,
            };
            await _orderProductservice.AddAsync(orderProduct);
            return RedirectToAction("All", "Order");
        }
        [HttpGet]
        public async Task<IActionResult>Update(int id)
        {
           var model=_orderProductservice.GetAll().Where(x=>x.OrderId==id).Include(x=>x.Order).Include(x=>x.Product).Select(x=>new OrderEditViewModel()
           {
               OrderDate=x.Order.OrderDate.ToString("yyyy-MM-dd"),
               ProductsList=new SelectList(_productservice.GetAll(),"Id","Name"),
               ProductId=x.ProductId,
           })
                .FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult>Update(int id,OrderEditViewModel model)
        {
            if(ModelState.IsValid)
            {
                model.ProductsList=new SelectList(_productservice.GetAll(),"Id","Name");
                return View(model);
            }
            Order order = _orderservice.GetAll().Where(x => x.Id == id).FirstOrDefault();
            int customerId = order.CustomerId;
            await _orderservice.DeleteAsync(order.Id);

            Order poruchka = new Order()
            {
                OrderDate = DateTime.Now,
                CustomerId = customerId
            };

            await _orderservice.AddAsync(poruchka);

            OrderProduct order1 = new OrderProduct();
            order1.OrderId = poruchka.Id;
            order1.ProductId = model.ProductId;
            await _orderProductservice.AddAsync(order1);
            return RedirectToAction("All", "Order");
        }
        [HttpGet]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Намираме записите за изтриване с всички свързани данни
            var model = await _orderProductservice.GetAll()
                .Where(x => x.OrderId == id)
                .Include(x => x.Order).ThenInclude(x => x.Customer)
                .Include(x => x.Product).ThenInclude(x => x.Category)
                .Select(x => new OrderDeleteConfrimedViewModel()
                {
                    Id = id,
                    CategoryName = x.Product.Category.Name,
                    OrderDate = x.Order.OrderDate.ToString("yyyy-MM-dd"),
                    CustomerName = x.Order.Customer.FirstName,
                    Price = x.Product.Price,
                    ProductName = x.Product.Name,
                })
                .FirstOrDefaultAsync(); // Асинхронно!

            // Ако не е намерен запис с това id, връщаме грешка
            if (model == null)
            {
                return NotFound("Поръчката не е намерена.");
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderservice.GetAll()
         .Where(x => x.Id == id)
         .FirstOrDefaultAsync();

            if (order == null)
            {
                return NotFound("Поръчката не е намерена.");
            }

            // Изтриваме свързаните записи в OrdersProducts
            await _orderProductservice.DeleteAsync(id);

            // Now delete the order
            var order1 = await _orderservice.GetByIdAsync(id);
            await _orderservice.DeleteAsync(order1.Id);

            return RedirectToAction("All", "Order");
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            var model=_orderProductservice.GetAll().Where(x=>x.OrderId == id).Include(x=>x.Order).ThenInclude(x => x.Customer).Include(x => x.Product).ThenInclude(p => p.Category).Select(x => new OrderDetailsViewModel()
            {
                Aroma = x.Product.Aroma,
                CategoryName = x.Product.Category.Name,
                OrderDate = x.Order.OrderDate.ToString(),
                Price = x.Product.Price,
                Email = x.Order.Customer.User.Email,
                CustomerName = x.Order.Customer.FirstName,
                LastName = x.Order.Customer.LastName,
                ProductName = x.Product.Name
            }).FirstOrDefault();
            return View(model);
        }
       
        public async Task<IActionResult> MyDetails(int orderId)
        {
            
            var order = await _orderservice.GetOrderById(orderId);
            if (order == null)
            {
                return NotFound();
            }
            // Map Order entity to ViewModel in the Controller
            var viewModel = new CustomerOrderDetailsViewModel
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                Status = order.Status ,// Assuming OrderStatus has a Name
                OrderProducts = order.OrderProducts.Select(op => new OrderProductViewModel
                {
                    ProductName = op.Product.Name,
                    Aroma = op.Product.Aroma,
                    CategoryName = op.Product.Category.Name,
                    Price = op.Product?.Price ?? 0,
                    Quantity = op.Quantity
                }).ToList() ?? new List<OrderProductViewModel>()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
