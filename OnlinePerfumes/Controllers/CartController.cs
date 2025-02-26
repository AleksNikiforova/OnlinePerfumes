using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlinePerfumes.Core;
using OnlinePerfumes.Models;
using OnlinePerfumes.Models.ViewModels.Cart;

namespace OnlinePerfumes.Controllers
{
    public class CartController : Controller
    {
        private readonly IService<Product> _productService;
        private readonly IService<CartItem> _cartItemService;
        private readonly IService<Order> _orderService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IService<Customer> _customerService;

        public CartController(IService<Product> productService, IService<CartItem> cartItemService, IService<Order> orderService, UserManager<IdentityUser> userManager, IService<Customer> customerService)
        {
            _productService = productService;
            _cartItemService = cartItemService;
            _orderService = orderService;
            _userManager = userManager;
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var customer = (await _customerService.GetAllAsync())
                .FirstOrDefault(c => c.UserId == user.Id);

            if (customer == null)
            {
                return View(new List<CartItem>()); // Ако няма клиент, върни празна количка
            }

            var cartItems = _cartItemService.GetAll().Include(p => p.Product)
                .Where(ci => ci.CustomerId == customer.Id)
                .ToList();
            CartViewModel result = new CartViewModel { CartItems = cartItems };

            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {

            var user = await _userManager.GetUserAsync(User);
            var customer = (await _customerService.GetAllAsync())
                .FirstOrDefault(c => c.UserId == user.Id);

            if (customer == null)
            {
                return BadRequest("Не е намерен клиентски профил.");
            }

            var product = await _productService.GetByIdAsync(productId);
            if (product == null)
            {
                return NotFound("Продуктът не съществува.");
            }

            var cartItem = (await _cartItemService.GetAllAsync())
                .FirstOrDefault(ci => ci.CustomerId == customer.Id && ci.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
                await _cartItemService.UpdateAsync(cartItem); // Актуализиране на количката
            }
            else
            {
                cartItem = new CartItem
                {
                    CustomerId = customer.Id,
                    ProductId = productId,
                    Quantity = quantity
                };

                await _cartItemService.AddAsync(cartItem); // Добавяне на нов елемент в количката

            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            var cartItem = await _cartItemService.GetByIdAsync(cartItemId);
            if (cartItem == null)
            {
                return NotFound();
            }

            cartItem.Quantity = quantity;
            await _cartItemService.UpdateAsync(cartItem); // Актуализиране на cartItem

            return RedirectToAction("Index");
        }

        // 4. Премахване на продукт от количката
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var cartItem = _cartItemService.GetAll()
                .Include(ci => ci.Customer) // Включване на клиента (ако е необходимо)
                .FirstOrDefault(ci => ci.Id == cartItemId); // Намиране на конкретния запис

            if (cartItem != null)
            {
                await _cartItemService.DeleteAsync(cartItemId);
            }

            return RedirectToAction("Index");
        }
    }
}
