using HostManager.Contracts;
using HostManager.Models;
using HostManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
                Accounts = _account.GetAll()
                .OrderBy(x => x.PayDate.AddMonths(x.Term.Value))
                .ToList(),
            };
            return View(_model);
        }

        //public async Task<IActionResult> Privacy()
        //{
        //    //await _check.CheckExpiration();
        //    return View();
        //}
    }
}
