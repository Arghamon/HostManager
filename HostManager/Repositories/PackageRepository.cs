using HostManager.Contracts;
using HostManager.Data;
using HostManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HostManager.Repositories
{
    public class PackageRepository : IRepository<Package>
    {

        private readonly ApplicationDbContext _context;

        public PackageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Package Package)
        {
            var item = new Package
            {
                Capacity = Package.Capacity,
                Description = Package.Description,
                Name = Package.Name,
            };

            try
            {
                _context.Packages.Add(item);
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
            throw new NotImplementedException();
        }

        public bool Edit(Package Item)
        {
            throw new NotImplementedException();
        }

        public Package Find(Package Item)
        {
            return _context.Packages.FirstOrDefault(x => x.Name == Item.Name);
        }

        public Package FindById(int Id)
        {
            throw new NotImplementedException();
        }

        public Package Get(int Id)
        {
            var package = _context.Packages.FirstOrDefault(pack => pack.Id == Id);
            return package;
        }

        public IEnumerable<Package> GetAll()
        {
            return _context.Packages.ToList();
        }
    }
}
