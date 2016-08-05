using CRIMAS.Models;
using CRIMAS.SupportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRIMAS.Repository.artifacts
{
    public class DividendManagement
    {
        private Models.CrimasDb db;
        private UserProfile users;
        public DividendManagement()
        {
            db = new CRIMAS.Models.CrimasDb();
            users = new UserProfile();

        }
        public void postDividend(decimal percentage)
        {
            //posts dividend to all customers 
            decimal percentage_dividend = percentage / 100;
            var accounts = db.CustomerSavings.ToList();
            decimal initial_dividend_amount = 0;
            if (accounts != null)
            {
                foreach (var account in accounts)
                {
                    var customerName = db.Customers.Where(x => x.AccountNo == account.AccountNo).Select(x => x.Name).FirstOrDefault();
                    var credits = db.CustomerSavings.Where(x => x.AccountNo == account.AccountNo).Select(x => x.Credit).Sum();
                    var debits = db.CustomerSavings.Where(x => x.AccountNo == account.AccountNo).Select(x => x.Debit).Sum();

                    var balance = credits - debits;

                    var dividends = (balance * percentage_dividend);

                    //credit customer account
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

                    //update dividends table
                    db.Dividends.Add(new Dividends
                    {
                        accountNo = account.AccountNo,
                        amount = dividends,
                        customerName = account.Name,
                        percentage = percentage_dividend,
                        dateCreated = DateTime.Now
                    });

                    //sum all dividends issued
                    initial_dividend_amount = initial_dividend_amount + dividends; 

                }
              
                //update dividends summary table
                db.DividendSummary.Add(new DividendSummary
                {
                    dateCreated = DateTime.Now,
                    total_amount = initial_dividend_amount,
                    percentage = percentage
                });
                db.SaveChanges();
            }

        }

        public void notify()
        {
            //send email every month to notify admin that customers are due for dividend
            var admin_emails = db.UserProfiles.Where(x => x.role == "Admin").Select(x => x.email).FirstOrDefault();
            foreach(var email in admin_emails)
            {
                EmailHelper.SendMail(email.ToString(), "Dividend for " + DateTime.Now.ToShortDateString()+" is due!", 
                    "Dividends for "+ DateTime.Now.ToShortDateString()+
                    " is due to be disbursed. Please follow this link : "+"http://crmpcs.com"+
                    " to login and credit customer dividends.");
            }

            //send sms every month to notify admin that customers are due for dividend

        }

    }
}