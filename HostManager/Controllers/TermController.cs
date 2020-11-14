using HostManager.Contracts;
using HostManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HostManagerMvc.Controllers
{

    [Authorize]
    public class TermController : Controller
    {

        private readonly IRepository<Term> _term;

        public TermController(IRepository<Term> term)
        {
            _term = term;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var _model = _term.GetAll();
            return View(_model);
        }

        [HttpGet]
        public IActionResult AddTerm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTerm(Term term)
        {
            var existed = _term.Find(term);

            if(existed != null)
            {
                ModelState.AddModelError("Value", $"{term.Value} უკვე არსებობს");
                return View();
            }
            _term.Add(term);
            return View();
        }
    }
}
