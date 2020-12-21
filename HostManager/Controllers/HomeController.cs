using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using HostManager.Contracts;
using HostManager.Models;
using HostManager.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using HostManager.Services;

namespace HostManager.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IRepository<Account> _account;
        private readonly ICheckExpirationService _check;

        public HomeController(IRepository<Account> account, ICheckExpirationService check)
        {
            _account = account;
            _check = check;
        }

        [Authorize]
        public IActionResult Index()
        {
            IndexViewModel _model = new IndexViewModel()
            {
                Accounts = _account.GetAll().ToList(),
            };
            return View(_model);
        }

        public async Task<IActionResult> Privacy()
        {
            //await _check.CheckExpiration();
            return View();
        }
    }
}
