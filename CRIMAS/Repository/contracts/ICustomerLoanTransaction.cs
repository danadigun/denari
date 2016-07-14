using CRIMAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRIMAS.Repository
{
    public interface ICustomerLoanTransaction
    {
        void Add(CustomerLoanTransaction tx);

        CustomerLoanTransaction get(int transactionId);

        IQueryable<CustomerLoanTransaction> getAll();
        bool remove(int transactionId);


    }
}
