using CRIMAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRIMAS.Repository
{
    public interface ILoan
    {
        void Add(Loan loan);
        Loan get(int id);
        IQueryable<Loan> getAll();
        bool remove(int id);
    }
}
