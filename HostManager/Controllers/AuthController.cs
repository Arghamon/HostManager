using HostManager.Contracts;
using HostManager.Models;
using HostManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HostManager.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthRepository _auth;

        private static string _returnUrl { get; set; }

        public AuthController(
            ILogger<AuthController> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAuthRepository auth
            )
        {
            _logger = logger;
            _auth = auth;
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _auth.LogOutAsync();
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            _returnUrl = returnUrl;
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _auth.LoginAsync(model);

            if (!result)
            {
                _logger.LogError($"{model.Email} not found");
                ModelState.AddModelError("ErrorMessage", "პაროლი/ელ-ფოსტა არასწორია");

                return View();
            }

            if (_returnUrl != null)
            {
                return Redirect(_returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _auth.CreateUserAsync(model);

            if (result.Succeeded)
            {
                _logger.LogInformation("Registration was succesfull");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogError($"Registration Failed - {error.Description}, ");
                    ModelState.TryAddModelError("Email", error.Description);
                }
                return View();
            }
        }
    }
}
