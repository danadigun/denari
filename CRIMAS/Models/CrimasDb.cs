using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CRIMAS.Models
{
    public class CrimasDb : DbContext
    {
         public CrimasDb()
                : base("name=DefaultConnection")
            {
            }
            public DbSet<UserProfile> UserProfiles { get; set; } //user profiles
            public DbSet<Customer> Customers { get; set; } //customers
            public DbSet<CustomerSavings> CustomerSavings { get; set; } //customer savings
            public DbSet<CustomerLoanTransaction> CustomerLoanTransactions { get; set; } //Loan amount per customer
            public DbSet<Loan> Loans { get; set; } //all loans
            public DbSet<Borrowed> Borrows { get; set; } //amount customer borrowed
            public DbSet<LoanTransaction> LoanTransactions { get; set; } 
            public DbSet<LoanInterest> LoanInterests { get; set; } //10% of loan paid upfront
            public DbSet<BankReconciliation> BankReconciliations { get; set; }
            public DbSet<ReconciliationProperties> ReconciliationProperties { get; set; }
    }
}