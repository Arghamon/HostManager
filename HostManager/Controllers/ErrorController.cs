﻿
using Microsoft.AspNetCore.Mvc;

namespace HostManager.Controllers
{
    public class ErrorController : Controller
    {

        [HttpGet("/Error/404")]
        public IActionResult Error404()
        {
            return View();
        }
    }
}
