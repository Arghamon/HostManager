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
            throw new NotImplementedException();
        }

        public bool Edit(Term Item)
        {
            throw new NotImplementedException();
        }

        public Term Find(Term Item)
        {
            return _context.Terms.FirstOrDefault(term => term.Value == Item.Value);
        }

        public Term FindById(int Id)
        {
            throw new NotImplementedException();
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
