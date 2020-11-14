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
            var existed = _company.Find(company);
            if(company != null)
            {
                ModelState.AddModelError("Name", $"კომპანია {company.Name} არსებობს");
                return View();
            }
            _company.Add(company);
            return View();
        }
    }
}
