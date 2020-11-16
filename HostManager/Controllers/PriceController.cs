using System.Linq;
using HostManager.Contracts;
using HostManager.Models;
using HostManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HostManagerMvc.Controllers
{

    [Authorize]
    public class PriceController : Controller
    {

        private readonly IPriceRepository _price;
        private readonly IRepository<Package> _package;
        private readonly IRepository<Term> _term;

        public PriceController(IPriceRepository price, IRepository<Package> package, IRepository<Term> term)
        {
            _price = price;
            _package = package;
            _term = term;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var _model = _price.GetAll();
            return View(_model);
        }

        [HttpGet]
        public IActionResult AddPrice()
        {
            var model = new PriceViewModel
            {
                Packages = _package.GetAll().ToList(),
                Terms = _term.GetAll().OrderBy(term => term.Value).ToList(),
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddPrice(Price price)
        {
            _price.Add(price);

            var model = new PriceViewModel
            {
                Packages = _package.GetAll().ToList(),
                Terms = _term.GetAll().OrderBy(term => term.Value).ToList(),
            };

            return RedirectToAction("Index");
        }

        [HttpPost("api/price")]
        public IActionResult GetPriceByTerm([FromBody] PriceRequest request)
        {
            double priceValue = _price.GetPriceByTerm(request);
            return Ok(new { Price = priceValue });
        }

        [HttpGet]
        public IActionResult EditPrice(int Id)
        {
            var price = _price.FindById(Id);

            if (price == null)
            {
                return RedirectToAction("Error404", "Error");
            }
            var model = new PriceViewModel
            {
                Packages = _package.GetAll().ToList(),
                Terms = _term.GetAll().OrderBy(term => term.Value).ToList(),
                Price = price
            };
          
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdatePrice(Price price)
        {
            bool updated = _price.Edit(price);
            if (updated)
            {
                return RedirectToAction("Index");
            }
            return EditPrice(price.Id);
        }

        [HttpGet]
        public IActionResult DeletePrice(int Id)
        {
            _price.Delete(Id);

            return RedirectToAction("Index");
        }
    }
}
