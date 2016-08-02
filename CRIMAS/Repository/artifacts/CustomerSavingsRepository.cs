using CRIMAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRIMAS.Repository
{
    public class CustomerSavingsRepository : ICustomerSavings
    {
        public CrimasDb _context;
        public CustomerSavingsRepository(CrimasDb context)
        {
            _context = context;
        }
        public void Add(Models.CustomerSavings savings)
        {
            _context.CustomerSavings.Add(savings);
        }

        public Models.CustomerSavings get(int id)
        {
            return _context.CustomerSavings.Find(id);
        }

        public IQueryable<Models.CustomerSavings> getAll()
        {
            return _context.CustomerSavings.AsQueryable<CustomerSavings>();
        }

        public bool remove(int id)
        {
            var savings = _context.CustomerSavings.Find(id);

            if (savings != null)
            {
                _context.CustomerSavings.Remove(savings);

                return true;
            }
            return false;
        }
    }
}