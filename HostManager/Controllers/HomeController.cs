using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using HostManager.Contracts;
using HostManager.Models;
using HostManager.ViewModels;
using System.Linq;

namespace HostManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Account> _account;

        public HomeController(ILogger<HomeController> logger, IRepository<Account> account)
        {
            _logger = logger;
            _account = account;
        }

        public IActionResult Index()
        {
            IndexViewModel _model = new IndexViewModel()
            {
                Accounts = _account.GetAll().ToList(),
            };
            return View(_model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
