using CRIMAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRIMAS.Repository
{
    public class CustomerLoanTransactionRepository : ICustomerLoanTransaction
    {
        public CrimasDb _context;
        public CustomerLoanTransactionRepository(CrimasDb context)
        {
            _context = context;
        }
        public void Add(Models.CustomerLoanTransaction tx)
        {
            _context.CustomerLoanTransactions.Add(tx);
        }

        public Models.CustomerLoanTransaction get(int transactionId)
        {
            return _context.CustomerLoanTransactions.Find(transactionId);
        }

        public IQueryable<Models.CustomerLoanTransaction> getAll()
        {
            return _context.CustomerLoanTransactions.AsQueryable<CustomerLoanTransaction>();
        }

        public bool remove(int transactionId)
        {
            var transaction = _context.CustomerLoanTransactions.Find(transactionId);

            if (transaction != null)
            {
                _context.CustomerLoanTransactions.Remove(transaction);

                return true;
            }
            return false;
        }
    }
}