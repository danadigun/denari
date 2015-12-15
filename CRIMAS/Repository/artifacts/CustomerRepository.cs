using CRIMAS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CRIMAS.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public CrimasDb _context;
        public CustomerRepository(CrimasDb context)
        {
            _context = context;
        }
        public void Add(Models.Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public bool Remove(int id)
        {
            var customer = _context.Customers.Find(id);

            if (customer != null)
            {
                _context.Customers.Remove(customer);

                return true;
            }

            return false;
        }

        public bool update(int id)
        {
            throw new NotImplementedException();
        }

        public Models.Customer get(int id)
        {
            return _context.Customers.Find(id);
        }

        public IQueryable<Models.Customer> getAll()
        {
            return _context.Customers.AsQueryable<Customer>();
        }
    }
}