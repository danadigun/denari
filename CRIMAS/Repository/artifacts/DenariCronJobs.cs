using CRIMAS.Models;
using HangFire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRIMAS.Repository.artifacts
{
    public class DenariCronJobs : IDenariCron
    {
        private Models.CrimasDb db;
        public DenariCronJobs()
        {
            db = new CRIMAS.Models.CrimasDb();

        }

        /// <summary>
        /// Initiates and credits each customer's account with dividends
        /// </summary>
        public void initateDividends()
        {
            //RecurringJob.AddOrUpdate(() => getDividend(accountNo), Cron.Monthly); 
            RecurringJob.AddOrUpdate(() => getDividend(), Cron.Monthly);
        }

        public void getDividend()
        {
            var accounts = db.CustomerSavings.ToList();
            if (accounts != null)
            {
                foreach (var account in accounts)
                {
                    var customerName = db.Customers.Where(x => x.AccountNo == account.AccountNo).Select(x => x.Name).FirstOrDefault();
                    var credits = db.CustomerSavings.Where(x => x.AccountNo == account.AccountNo).Select(x => x.Credit).Sum();
                    var debits = db.CustomerSavings.Where(x => x.AccountNo == account.AccountNo).Select(x => x.Debit).Sum();

                    var balance = credits - debits;

                    var dividends = (balance * 0.02m);

                    db.CustomerSavings.Add(new CustomerSavings
                    {
                        AccountNo = account.AccountNo,
                        Credit = dividends,
                        Debit = 0,
                        Name = account.Name,
                        DateCreated = DateTime.Now,
                        Transactionby = "auto generated",
                        TransactionMsg = "Dividends",
                    });
                    db.SaveChanges();

                }
            }


        }

        /// <summary>
        ///  Implement generate reconciliation
        /// </summary>
       
        public void generateReconciliation()
        {
            //RecurringJob.AddOrUpdate(() => getReconciliation(), Cron.Monthly);
        }

        public void getReconciliation()
        {
            var accounts = db.Customers.ToList();
            if (accounts != null)
            {
                foreach(var account in accounts){
                    //get name
                    string name = account.Name;

                    //get total deposits for the month
                    var deposits = db.CustomerSavings.Where(x => x.AccountNo == account.AccountNo).Where(x=>x.DateCreated.Month==DateTime.Now.Month).Select(x => x.Credit).DefaultIfEmpty().Sum();

                    //get int_cap from reconciliation table
                    var countOfRows = db.ReconciliationProperties.Count();

                    //var properties = db.ReconciliationProperties.OrderBy(x => x.Id).Skip(countOfRows - 1).FirstOrDefault();
                    var int_cap = db.ReconciliationProperties.OrderBy(x => x.Id).Skip(countOfRows - 1).Select(x=>x.int_cap_value).FirstOrDefault();

                    //get monthly interest payable on each Loan repayment & get monthly loan amount payed
                    //ONLY FOR ACTIVE LOANS
                    var interest_payable = 0m;
                    var monthly_loan_amount = 0m;

                    var customer_loan = db.Loans.Where(x => x.AccountNo == account.AccountNo).Where(x=>x.LoanStatus=="active").ToList();
                    if (customer_loan.Count() != 0)
                    {
                        var interest = customer_loan.Select(x => x.InterestRate).DefaultIfEmpty().Sum() / 100;
                        interest_payable = customer_loan.Select(x => x.amount).DefaultIfEmpty().Sum() * interest;

                        var loan_rows = db.Loans.Count();
                        var loan_duration = customer_loan.OrderBy(x => x.Id).Skip(loan_rows - 1).Select(x => x.Duration).FirstOrDefault();
                        monthly_loan_amount = customer_loan.Select(x => x.amount).DefaultIfEmpty().Sum() / Convert.ToDecimal(loan_duration);
                    }
                    

                    //get transfer_fee
                    var transfer_fee = db.ReconciliationProperties.OrderBy(x => x.Id).Skip(countOfRows - 1).Select(x => x.transfer_fee).FirstOrDefault();

                    //get vat = vat/100 * transfer_fee
                    var vat = db.ReconciliationProperties.OrderBy(x => x.Id).Skip(countOfRows - 1).Select(x => x.transfer_fee).FirstOrDefault() * (db.ReconciliationProperties.OrderBy(x => x.Id).Skip(countOfRows - 1).Select(x => x.vat).FirstOrDefault() / 100);

                    //get sms_charge
                    var sms_charges = db.ReconciliationProperties.OrderBy(x => x.Id).Skip(countOfRows - 1).Select(x => x.sms_charge).FirstOrDefault();

                    //get withholding_tax
                    var withholding_tax = 0m; //TODO:Get the right amount

                    //get other_charges
                    var other_charges = 0m;

                    //get per_cap_com
                    var per_cap_com = db.ReconciliationProperties.OrderBy(x => x.Id).Skip(countOfRows - 1).Select(x => x.per_capital_commission).FirstOrDefault();

                    //sum total credits: deposits + int_cap + recovery_loan
                    var total_credits = deposits + int_cap + interest_payable + monthly_loan_amount;

                    //sum total debits: loans+transfer_fee+VAT+sms_charge+witholding_tax+per_cap_com;
                    var total_debits = monthly_loan_amount + transfer_fee + sms_charges + withholding_tax + per_cap_com;

                    //update the BankReconciliationTable
                    db.BankReconciliations.Add(new BankReconciliation
                    {
                        beneficiary = name,
                        VAT = vat,
                        credits = total_credits,
                        debits = total_debits,
                        deposit = deposits,
                        int_cap = int_cap,
                        dateCreated = DateTime.Now,
                        loans = monthly_loan_amount,
                        other_charges = other_charges,
                        per_cap_com = per_cap_com,
                        recovery_loan = interest_payable + monthly_loan_amount,
                        sms_charge = sms_charges,
                        transfer_fee = transfer_fee,
                        with_holding_tax = withholding_tax
                    });
                    db.SaveChanges();

                }
            }
        }
    }
}