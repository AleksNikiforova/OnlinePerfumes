using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Models;
using OnlinePerfumes.Models.ViewModels;
using OnlinePerfumes.Utility;

namespace OnlinePerfumes.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;    
        private readonly ICustomerService _customerService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ICustomerService customerService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _customerService = customerService;
        }
        public IActionResult LogIn()=>View();

        [HttpPost]
        public async Task<IActionResult> Login (LogInViewModel logInViewModel)
        {
            //if(ModelState.IsValid)
            //{
                var user=await _userManager.FindByEmailAsync(logInViewModel.Email);
                if (user==null)
                {
                    TempData["Error"] = "User not found";
                    return View(logInViewModel);
                }
                var result = await _signInManager.PasswordSignInAsync(user.UserName, logInViewModel.Password, logInViewModel.RememberMe, false);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Влизането е успешно";
                    return RedirectToAction("Index", "Home");
                }
                TempData["Error"] = "Влизането не е успешно";
                ModelState.AddModelError("", "Влизането не е успешно");
            //}
            return View(logInViewModel);

           

        }
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            //if (ModelState.IsValid)
            //{
                var user = new IdentityUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                
                var customer = new Customer
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        UserId = user.Id  
                    };
                
                await _customerService.AddAsync(customer);
                    await _userManager.AddToRoleAsync(user, SD.UserRole); 
                    await _signInManager.SignInAsync(user, isPersistent: true);

                    
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            //}
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
