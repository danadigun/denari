using CRIMAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRIMAS.Repository
{
    public interface ICustomerSavings
    {
        void Add(CustomerSavings savings);

        CustomerSavings get(int id);

        IQueryable<CustomerSavings> getAll();

        bool remove(int id);
    }
}
