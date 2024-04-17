using Midterm_Assignment1_Login.Providers;
using Midterm_Assignment1_Login.Providers.Repositories;
using Microsoft.AspNetCore.Authorization;
using Midterm_Assignment1_Login.Models;
using Microsoft.AspNetCore.Mvc;
using Midterm_Assignment1_Login.Models.ViewModels;

namespace Midterm_Assignment1_Login.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IUserRepository _userRepository;

        public AccountController(IUserManager userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View(
                this.User.Claims.ToDictionary(
                    x => x.Type, x => x.Value));
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _userRepository.Validate(model);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password");
                return View(model);
            }

            await _userManager.SignIn(HttpContext, user, isPersistent: false);

            return LocalRedirect("~/Home/Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check if username is already taken (within current session)
            if (HttpContext.Session.Keys.Contains(model.EmailAddress))
            {
                ModelState.AddModelError("Username", "Username is already taken.");
                return View(model);
            }

            // Store username in session to ensure uniqueness
            HttpContext.Session.SetString(model.EmailAddress, "registered");

            // Optional: Add registration confirmation logic
            TempData["Message"] = "Registration successful! Please login with your new credentials.";

            return RedirectToAction("Login");
        }


        //[HttpPost]
        //public async Task<IActionResult> RegisterAsync(RegisterVm model)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    var user = _userRepository.Register(model);

        //    await _userManager.SignIn(HttpContext, user, isPersistent: false);

        //    return LocalRedirect("~/Home/Index");
        //}

        [HttpPost]
        public async Task<IActionResult> LogoutAsync()
        {
            await _userManager.SignOut(HttpContext);

            return RedirectToAction("Index", "Home");
        }



    }
}