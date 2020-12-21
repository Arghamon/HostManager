using System;
using System.Linq;
using HostManager.Contracts;
using HostManager.Models;
using HostManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HostManager.Controllers
{
    
    [Authorize] 
    public class AccountController : Controller
    {
        private readonly IRepository<Account> _accountRepo;
        private readonly IRepository<Package> _packageRepo;
        private readonly IRepository<Term> _termRepo;
        private readonly IRepository<Company> _companyRepo;

        public AccountController(IRepository<Account> accountRepo, IRepository<Package> packageRepo, IRepository<Term> termRepo, IRepository<Company> companyRepo)
        {
            _accountRepo = accountRepo;
            _packageRepo = packageRepo;
            _termRepo = termRepo;
            _companyRepo = companyRepo;
        }

        [HttpGet]
        public IActionResult AddAccount()
        {
            var model = new AccountViewModel
            {
                Packages = _packageRepo.GetAll().ToList(),
                Terms = _termRepo.GetAll().ToList(),
                Companies = _companyRepo.GetAll().ToList(),
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddAccount(Account account)
        {
            var existed = _accountRepo.Find(account);
            if(existed != null)
            {
                ModelState.AddModelError("ErrorMessage", "დომენი უკვე არსებობს");
                return AddAccount();
            }
            var created = _accountRepo.Add(account);

            if (created)
            {
                return Redirect("/home/index");
            }
            ModelState.AddModelError("ErrorMessage", "დომენი უკვე არსებობს");
            return RedirectToAction("AddAccount");
        }

        [HttpGet]
        public IActionResult GetAccount([FromRoute] int id)
        {
            var account = _accountRepo.Get(id);

            var model = new AccountViewModel
            {
                Account = account
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteAccount([FromRoute] int id)
        {
            _accountRepo.Delete(id);

            return Redirect("/home/index");
        }

        [HttpGet]
        public IActionResult EditAccount(int Id)
        {
            var account = _accountRepo.Get(Id);
            if(account == null)
            {
                return RedirectToAction("Error404", "Error");
            }
            AccountViewModel _model = new AccountViewModel
            {
                Packages = _packageRepo.GetAll().ToList(),
                Terms = _termRepo.GetAll().ToList(),
                Companies = _companyRepo.GetAll().ToList(),
                Account = account,
            };
            return View(_model);
        }

        [HttpPost] 
        public IActionResult UpdateAccount(Account account)
        {
            bool updated = _accountRepo.Edit(account);
            if(updated)
            {
                return RedirectToAction("Index", "Home");
            }
            return EditAccount(account.Id);
        }
    }
}
