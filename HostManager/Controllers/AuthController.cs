using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HostManager.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HostManager.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel request)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel request)
        {
            
            return View();
        }
    }
}
