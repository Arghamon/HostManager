using HostManager.Contracts;
using HostManager.Data;
using HostManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HostManager.Repositories
{
    public class AccountRepository : IRepository<Account>
    {

        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Account account)
        {
            try
            {
                _context.Add(account);
                var created = _context.SaveChanges();
                return created > 0;
            }
            catch
            {
                return false;
            }

        }

        public bool Delete(int Id)
        {
            var item = _context.Accounts.FirstOrDefault(x => x.Id == Id);

            if (item == null)
                return false;

            _context.Accounts.Remove(item);
            var deleted = _context.SaveChanges();

            return deleted > 0;
        }

        public bool Edit(Account Item)
        {
            _context.Accounts.Update(Item);
            var updated = _context.SaveChanges();
            return updated > 0;
        }

        public Account Get(int Id)
        {
            var account = _context.Accounts
                .Include(account => account.Package)
                .Include(account => account.Company)
                .Include(account => account.Term)
                .FirstOrDefault(account => account.Id == Id);

            return account;
        }

        public Account Find(Account account)
        {
            return _context.Accounts.FirstOrDefault(a => a.DomainName == account.DomainName);
        }

        public IEnumerable<Account> GetAll()
        {
            var accounts = _context.Accounts
                .Include(account => account.Package)
                .Include(account => account.Company)
                .Include(account => account.Term)
                .ToList();

            return accounts;
        }

        public Account FindById(int Id)
        {
            return _context.Accounts
                .Include(account => account.Term)
                .FirstOrDefault(x => x.CompanyId == Id);
        }
    }
}
