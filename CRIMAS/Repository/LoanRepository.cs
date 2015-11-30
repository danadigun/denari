using CRIMAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRIMAS.Repository
{
    public class LoanRepository : ILoan
    {
        public CrimasDb _context;
        public LoanRepository(CrimasDb context)
        {
            _context = context;
        }
        public void Add(Models.Loan loan)
        {
            _context.Loans.Add(loan);
        }

        public Models.Loan get(int id)
        {
            return _context.Loans.Find(id);
        }

        public IQueryable<Models.Loan> getAll()
        {
            //return _context.Loans.AsQueryable<Loan>();
            return _context.Loans;
        }

        public bool remove(int id)
        {
            var loan = _context.Loans.Find(id);

            if (loan != null)
            {
                _context.Loans.Remove(loan);

                return true;
            }
            return false;
        }
    }
}