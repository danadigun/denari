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

    }
}