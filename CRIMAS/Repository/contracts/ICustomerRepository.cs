using CRIMAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRIMAS.Repository
{
    public interface ICustomerRepository
    {
 
        void Add(Customer customer);
        bool Remove(int id);
        bool update(int id);
        Customer get(int id);
        IQueryable<Customer> getAll();
                     
    }
}
