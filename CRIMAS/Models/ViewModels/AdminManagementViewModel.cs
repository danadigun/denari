using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRIMAS.Models.ViewModels
{
    /// <summary>
    /// Admin Management View model - per year
    /// </summary>
    public class AdminManagementViewModel
    {
        /*Business year*/
        public int year { get; set; }

        /*Loan Management*/
        public decimal total_loanApplications { get; set; }
        public decimal total_OutstandingLoans { get; set; }
        public decimal total_CompletedLoans { get; set; }
        public decimal total_loanRepayment { get; set; }
        

        /*Customer Management*/
        public int total_customers { get; set; }
        public decimal total_savingsBalance { get; set; }
        public decimal total_emcumbered { get; set; }

        /*Dividend Management*/
        public decimal total_percentageDividends { get; set; }
        public decimal total_DividendsPaid { get; set; }

        /*Profit Management*/
        public decimal total_profit { get; set; }
        public decimal total_profit_q1 { get; set; }
        public decimal total_profit_q2 { get; set; }
        public decimal total_profit_q3 { get; set; }

        /*Profit monthly*/
        public decimal profit_jan { get; set; }
        public decimal profit_feb { get; set; }
        public decimal profit_mar { get; set; }
        public decimal profit_apr { get; set; }
        public decimal profit_may { get; set; }
        public decimal profit_jun { get; set; }
        public decimal profit_jul { get; set; }
        public decimal profit_aug { get; set; }
        public decimal profit_sept { get; set; }
        public decimal profit_oct { get; set; }
        public decimal profit_nov { get; set; }
        public decimal profit_dec { get; set; }

        /*top 5 outstanding loans*/
        public List<Customer> outstanding_LoanApplicants { get; set; }

    }
}