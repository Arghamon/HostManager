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
            if (existed != null)
            {
                ModelState.AddModelError("Name", $"{package.Name} უკვე არსებობს");
                return View();
            }
            _packageRepo.Add(package);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditPackage(int Id)
        {
            var package = _packageRepo.FindById(Id);
            if (package == null)
            {
                return RedirectToAction("Error404", "Error");
            }
            return View(package);
        }

        [HttpPost]
        public IActionResult UpdatePackage(Package package)
        {
            bool updated = _packageRepo.Edit(package);
            if (updated)
            {
                System.Console.WriteLine(package.Id + " - ID");
                return RedirectToAction("Index");
            }
            System.Console.WriteLine(package.Id + " - ID");
            return EditPackage(package.Id);
        }

        [HttpGet]
        public IActionResult DeletePackage(int Id)
        {
            _packageRepo.Delete(Id);

            return RedirectToAction("Index");
        }
    }
}
