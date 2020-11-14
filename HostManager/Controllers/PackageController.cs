using HostManager.Contracts;
using HostManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HostManagerMvc.Controllers
{

    [Authorize]
    public class PackageController : Controller
    {
        private readonly IRepository<Package> _packageRepo;
        private readonly IRepository<Account> _accountRepo;

        public PackageController(IRepository<Package> packageRepo, IRepository<Account> accountRepo)
        {
            _packageRepo = packageRepo;
            _accountRepo = accountRepo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var _model = _packageRepo.GetAll();
            return View(_model);
        }

        [HttpGet]
        public IActionResult AddPackage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPackage(Package package)
        {
            var existed = _packageRepo.Find(package);
            if(existed != null)
            {
                ModelState.AddModelError("Name", $"{package.Name} უკვე არსებობს");
                return View();
            }
            _packageRepo.Add(package);
            return View();
        }
    }
}
