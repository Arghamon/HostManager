using HostManager.Contracts;
using HostManager.Data;
using HostManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HostManager.Repositories
{
    public class TermRepository : IRepository<Term>
    {

        private readonly ApplicationDbContext _context;

        public TermRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Term term)
        {
            try
            {
                _context.Terms.Add(term);
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
            var item = _context.Terms.FirstOrDefault(x => x.Id == Id);

            if (item == null)
                return false;

            _context.Terms.Remove(item);
            var deleted = _context.SaveChanges();

            return deleted > 0;
        }

        public bool Edit(Term Item)
        {
            _context.Terms.Update(Item);
            var updated = _context.SaveChanges();
            return updated > 0;
        }

        public Term Find(Term Item)
        {
            return _context.Terms.FirstOrDefault(term => term.Value == Item.Value);
        }

        public Term FindById(int Id)
        {
            return _context.Terms.FirstOrDefault(x => x.Id == Id);
        }

        public Term Get(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Term> GetAll()
        {
            return _context.Terms.ToList();
        }
    }
}
