using HostManager.Contracts;
using HostManager.Data;
using HostManager.Models;
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
            var item = _context.Packages.FirstOrDefault(x => x.Id == Id);

            if (item == null)
                return false;

            _context.Packages.Remove(item);
            var deleted = _context.SaveChanges();

            return deleted > 0;
        }

        public bool Edit(Package Item)
        {
            _context.Packages.Update(Item);
            var updated = _context.SaveChanges();
            return updated > 0;
        }

        public Package Find(Package Item)
        {
            return _context.Packages.FirstOrDefault(x => x.Name == Item.Name);
        }

        public Package FindById(int Id)
        {
            return _context.Packages.FirstOrDefault(x => x.Id == Id);
        }

        public Package Get(int Id)
        {
            var package = _context.Packages.FirstOrDefault(pack => pack.Id == Id);
            return package;
        }

        public IEnumerable<Package> GetAll()
        {
            return _context.Packages.OrderBy(x => x.Id).ToList();
        }
    }
}
