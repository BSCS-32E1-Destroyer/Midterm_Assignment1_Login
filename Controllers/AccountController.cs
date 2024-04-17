using Midterm_Assignment1_Login.Providers.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Midterm_Assignment1_Login.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(
            IUserRepository userRepository)
        {
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

            if (user == null) return View(model);

            return LocalRedirect("~/Home/Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _userRepository.Register(model);

            return LocalRedirect("~/Home/Index");
        }

        public async Task<IActionResult> LogoutAsync()
        {
            return RedirectPermanent("~/Home/Index");
        }
    }
}