using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Core.Service;
using OnlinePerfumes.Models;
using OnlinePerfumes.Models.ViewModels;
using OnlinePerfumes.Models.ViewModels.Customer;
using OnlinePerfumes.Models.ViewModels.OrderProduct;
using OnlinePerfumes.Utility;

namespace OnlinePerfumes.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(ICustomerService customerService, IOrderService orderService, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _customerService = customerService;
            _orderService = orderService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult AdminPanel()
        {
            return View();
        }
        public async Task<IActionResult> ListCustomers()
        {
            var list=await _customerService.GetAll().Include(x=>x.User).ToListAsync();
            var customerWithRoles=new List<CustomerForAdminViewModel>();
            foreach (var customer in list)
            {
                var roles = await _userManager.GetRolesAsync(customer.User);
                var role=roles.FirstOrDefault() ?? "No Role";

                customerWithRoles.Add(new CustomerForAdminViewModel
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.User.PhoneNumber,
                    Email = customer.User.Email,
                    Role = role
                });

            }
            return View(customerWithRoles);
        }

        public async Task<IActionResult> CustomerOrder()
        {
            /*var list=await _customerService.GetAll().Include(x=>x.Orders).ToListAsync();
            var customerOrderList = new List<CustomerOrderViewModel>();
            foreach (var customer in list)
            {
                foreach (var order in customer.Orders)
                {
                    // Добавяме всяка поръчка към CustomerOrderList
                    customerOrderList.Add(new CustomerOrderViewModel
                    {
                        OrderId = order.Id,
                        CustomerName = $"{customer.FirstName} {customer.LastName}",
                        Email = customer.User?.Email,
                        OrderDate = order.OrderDate,
                        Status = "Очаква потвърждение"
                    });
                }
            }
            return View(customerOrderList);*/
            var list = await _customerService.GetAll()
                                             .Include(x => x.Orders)
                                             .ThenInclude(o => o.OrderProducts)
                                             .ThenInclude(op => op.Product)
                                             .ThenInclude(p => p.Category)
                                             .ToListAsync();

            var customerOrderList = list
                .SelectMany(customer => customer.Orders, (customer, order) => new CustomerOrderViewModel
                {
                    OrderId = order.Id,
                    CustomerName = $"{customer.FirstName} {customer.LastName}",
                    Email = customer.User?.Email,
                    OrderDate = order.OrderDate,
                    Status = order.Status.ToString(),
                    OrderProducts = order.OrderProducts.Select(op => new OrderProductViewModel
                    {
                        ProductName = op.Product.Name,
                        Aroma = op.Product.Aroma,
                        CategoryName = op.Product.Category.Name,
                        Price = op.Product.Price,
                        Quantity = op.Quantity
                    }).ToList()
                })
                .ToList();

            return View(customerOrderList);
        }

        public async Task<IActionResult> MakeAdmin(string customerEmail)
        {
            if (string.IsNullOrEmpty(customerEmail))
            {
                TempData["Error"] = "Невалиден имеайл";
                return RedirectToAction("ListCustomers");
            }
            var customer=await _customerService.GetAll().Include(x=>x.User).FirstOrDefaultAsync(c=>c.User.Email==customerEmail);
            if(customer==null)
            {
                TempData["Error"] = "Customer не е намерен";
                return RedirectToAction("ListCustomer");
            }
            var user = await _userManager.FindByEmailAsync(customer.User.Email);
            if (user == null)
            {
                TempData["Error"] = "Потребителят не е намерен.";
                return RedirectToAction("ListCustomers");
            }
            var removeResult = await _userManager.RemoveFromRoleAsync(user, SD.UserRole);
            if (!removeResult.Succeeded)
            {
                TempData["Error"] = "Грешка при премахване на старата роля: " + string.Join(", ", removeResult.Errors.Select(e => e.Description));
                return RedirectToAction("ListCustomers");
            }
            var addResult = await _userManager.AddToRoleAsync(user, SD.AdminRole);
            if (!addResult.Succeeded)
            {
                TempData["Error"] = "Грешка при добавяне на новата роля: " + string.Join(", ", addResult.Errors.Select(e => e.Description));
                return RedirectToAction("ListCustomers");
            }
            TempData["Success"] = "Ролята е променена успешно.";
            return RedirectToAction("ListCustomers");
        }
        public async Task<IActionResult>MakeUser(string customerEmail)
        {

            if (string.IsNullOrEmpty(customerEmail))
            {
                TempData["Error"] = "Невалиден email.";
                return RedirectToAction("ListCustomers");
            }

            var customer = await _customerService.GetAll()
                .Include(x => x.User)
                .FirstOrDefaultAsync(c => c.User.Email == customerEmail);

            if (customer == null)
            {
                TempData["Error"] = "Customer не е намерен.";
                return RedirectToAction("ListCustomers");
            }
            var user = await _userManager.FindByEmailAsync(customer.User.Email);
            if (user == null)
            {
                TempData["Error"] = "Потребителят не е намерен.";
                return RedirectToAction("ListCustomers");
            }

            var removeResult = await _userManager.RemoveFromRoleAsync(user, SD.AdminRole);
            if (!removeResult.Succeeded)
            {
                TempData["Error"] = "Грешка при премахване на старата роля: " + string.Join(", ", removeResult.Errors.Select(e => e.Description));
                return RedirectToAction("ListCustomers");
            }
            var addResult = await _userManager.AddToRoleAsync(user, SD.UserRole);
            if (!addResult.Succeeded)
            {
                TempData["Error"] = "Грешка при добавяне на новата роля: " + string.Join(", ", addResult.Errors.Select(e => e.Description));
                return RedirectToAction("ListCustomers");
            }

            TempData["Success"] = "Ролята е променена успешно.";
            return RedirectToAction("ListCustomers");
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmOrder(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
            {
                TempData["ErrorMessage"] = "Поръчката не беше намерена!";
                return RedirectToAction("CustomerOrder");
            }

            order.Status = OrderStatus.Потвърдена;
            await _orderService.UpdateAsync(order);


            TempData["SuccessMessage"] = "Поръчката беше потвърдена успешно!";
            return RedirectToAction("CustomerOrder");
        }
        [HttpPost]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
            {
                TempData["ErrorMessage"] = "Поръчката не беше намерена!";
                return RedirectToAction("CustomerOrder");
            }

            // ✅ Променяме статуса на "Отказана"
            order.Status = OrderStatus.Отказана;
            await _orderService.UpdateAsync(order);

            TempData["SuccessMessage"] = "Поръчката беше отказана успешно!";
            return RedirectToAction("CustomerOrder");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
