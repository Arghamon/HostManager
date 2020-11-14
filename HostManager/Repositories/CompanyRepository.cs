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
            var item = new Company
            {
                Code = company.Code,
                ContactPerson = company.ContactPerson,
                Email = company.Email,
                Name = company.Name,
                Phone = company.Phone,
            };

            try
            {
                _context.Companies.Add(item);
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
            throw new NotImplementedException();
        }

        public bool Edit(Company Item)
        {
            throw new NotImplementedException();
        }

        public Company Find(Company Item)
        {
            return _context.Companies.FirstOrDefault(x => x.Name == Item.Name);
        }

        public Company FindById(int Id)
        {
            throw new NotImplementedException();
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
