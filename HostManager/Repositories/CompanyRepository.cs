using HostManager.Contracts;
using HostManager.Data;
using HostManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HostManager.Repositories
{
    public class CompanyRepository : IRepository<Company>
    {

        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Company company)
        {
            try
            {
                _context.Companies.Add(company);
                var updated = _context.SaveChanges();

                return updated > 0;
            }
            catch
            {
                Console.WriteLine("asdasdasd");
                return false;
            }
        }

        public bool Delete(int Id)
        {
            var item = _context.Companies.FirstOrDefault(x => x.Id == Id);

            if (item == null)
                return false;

            _context.Companies.Remove(item);
            var deleted = _context.SaveChanges();

            return deleted > 0;
        }

        public bool Edit(Company Item)
        {
            _context.Companies.Update(Item);
            var updated = _context.SaveChanges();
            return updated > 0;
        }

        public Company Find(Company Item)
        {
            return _context.Companies.FirstOrDefault(x => x.Name == Item.Name);
        }

        public Company FindById(int Id)
        {
            return _context.Companies.FirstOrDefault(x => x.Id == Id);
        }

        public Company Get(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Company> GetAll()
        {
            return _context.Companies.ToList();
        }
    }
}
