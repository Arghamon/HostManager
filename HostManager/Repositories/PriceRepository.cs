using HostManager.Contracts;
using HostManager.Data;
using HostManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HostManager.Repositories
{
    public class PriceRepository : IPriceRepository
    {
        private readonly ApplicationDbContext _context;

        public PriceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Price price)
        {
            var item = new Price
            {
                PackageId = price.PackageId,
                PriceValue = price.PriceValue,
                TermId = price.TermId
            };

            try
            {
                _context.Prices.Add(item);
                var updated = _context.SaveChanges();

                return updated > 0;
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

        public bool Edit(Price Item)
        {
            throw new NotImplementedException();
        }

        public Price Find(Price Item)
        {
            throw new NotImplementedException();
        }

        public Price FindById(int Id)
        {
            throw new NotImplementedException();
        }

        public Price Get(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Price> GetAll()
        {
            return _context.Prices
                .Include(x => x.Term)
                .Include(x => x.Package)
                .ToList();
        }

        public double GetPriceByTerm(PriceRequest request)
        {
            var priceValue = _context.Prices
                .Where(p => p.PackageId == request.PackageId)
                .Where(p => p.TermId == request.TermId)
                .Select(p => p.PriceValue)
                .FirstOrDefault();

            return priceValue;
        }
    }
}
