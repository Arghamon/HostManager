using HostManager.Contracts;
using HostManager.Data;
using System.Collections.Generic;
using System.Linq;
using HostManager.Models;
using System;

namespace HostManager.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {

        private readonly ApplicationDbContext _context;

        public InvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Invoice Item)
        {
            _context.Invoices.Add(Item);
            var added = _context.SaveChanges();

            return added > 0;
        }

        public bool Delete(int Id)
        {
            var item = _context.Invoices.FirstOrDefault(x => x.Id == Id);

            if (item == null)
                return false;

            _context.Invoices.Remove(item);
            var deleted = _context.SaveChanges();

            return deleted > 0;
        }

        public bool Edit(Invoice Item)
        {
            _context.Invoices.Update(Item);
            var updated = _context.SaveChanges();
            return updated > 0;
        }

        public Invoice Find(Invoice Item)
        {
            return _context.Invoices.FirstOrDefault(x => x.Id == Item.Id);
        }

        public Invoice FindByCompany(int CompanyId, DateTime date)
        {
            return _context.Invoices
                .Where(x => x.ExpireDate == date)
                .FirstOrDefault(x => x.CompanyId == CompanyId);
        }

        public Invoice FindById(int Id)
        {
            return _context.Invoices.FirstOrDefault(x => x.CompanyId == Id);
        }

        public Invoice Get(int Id)
        {
            return _context.Invoices.FirstOrDefault(x => x.Id == Id);
        }

        public IEnumerable<Invoice> GetAll()
        {
            return _context.Invoices.ToList();
        }
    }
}
