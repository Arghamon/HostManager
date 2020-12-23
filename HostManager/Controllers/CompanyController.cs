using HostManager.Contracts;
using HostManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HostManager.Controllers
{

    [Authorize]
    public class CompanyController : Controller
    {

        private readonly IRepository<Company> _company;

        public CompanyController(IRepository<Company> company)
        {
            _company = company;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var _model = _company.GetAll();
            return View(_model);
        }

        [HttpGet]
        public IActionResult AddCompany()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCompany(Company company)
        {
            if (string.IsNullOrEmpty(company.Name))
            {
                return View();
            }
            var existed = _company.Find(company);
           
            if(existed != null)
            {
                ModelState.AddModelError("Name", $"კომპანია {company.Name} არსებობს");
                return View();
            }
            
            var result = _company.Add(company);
            if (!result)
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditCompany(int Id)
        {
            var company = _company.FindById(Id);
            if (company == null)
            {
                return RedirectToAction("Error404", "Error");
            }
            return View(company);
        }

        [HttpPost]
        public IActionResult UpdateCompany(Company company)
        {
            bool updated = _company.Edit(company);
            if (updated)
            {
                return RedirectToAction("Index", "Company");
            }
            return EditCompany(company.Id);
        }

        [HttpGet]
        public IActionResult DeleteCompany(int Id)
        {
            _company.Delete(Id);

            return RedirectToAction("Index");
        }
    }
}
