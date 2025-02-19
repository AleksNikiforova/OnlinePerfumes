using Microsoft.AspNetCore.Mvc;
using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Models;
using OnlinePerfumes.Models.ViewModels.Customer;

namespace OnlinePerfumes.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpPost]

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> All()
        {

            var customers = _customerService.GetAllAsync();
            return View(customers);
        }

        [HttpPost]
        [Route("Customer/Delete/{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            await _customerService.DeleteAsync(id);
            return RedirectToAction("All");
        }
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customers = _customerService.GetByIdAsync(id);
            return View(customers);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new CustomerViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(CustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", model);
            }

            Customer customer = new Customer()
            {

                FirstName = model.FirstName,
                LastName = model.LastName,


            };

            await _customerService.AddAsync(customer);
            return RedirectToAction("All");

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cust = await _customerService.GetByIdAsync(id);

            CustomerViewModel model = new CustomerViewModel()
            {
                FirstName = cust.FirstName,
                LastName = cust.LastName,
                Email = cust.User.Email,
                PhoneNumber = cust.User.PhoneNumber
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Customer customer = await _customerService.GetByIdAsync(id);

            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.User.Email = model.Email;
            customer.User.PhoneNumber = model.PhoneNumber;

            await _customerService.UpdateAsync(customer);
            return RedirectToAction("All");
        }
    }
}
