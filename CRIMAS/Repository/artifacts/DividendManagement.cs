using CRIMAS.Models;
using CRIMAS.SupportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

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

        /// <summary>
        /// posts dividends
        /// </summary>
        /// <param name="percentage"></param>
        public bool postDividend(decimal percentage)
        {
            //posts dividend to all customers 
            decimal percentage_dividend = percentage / 100;
            var accounts = db.Customers.ToList();

            decimal initial_dividend_amount = 0;
            int _dividend_id = new Random().Next();

            //get all emails for notification
            var admin_emails = db.UserProfiles.Where(x => x.role == "Admin").Select(x => x.UserName).FirstOrDefault();
            

            if (accounts != null && admin_emails != null)
            {
                foreach (var account in accounts)
                {
                    //var customerName = db.Customers.Where(x => x.AccountNo == account.AccountNo).Select(x => x.Name).FirstOrDefault();
                    var credits = db.CustomerSavings.Where(x => x.AccountNo == account.AccountNo).ToList().Select(x => x.Credit).Sum();
                    var debits = db.CustomerSavings.Where(x => x.AccountNo == account.AccountNo).ToList().Select(x => x.Debit).Sum();

                    var balance = credits - debits;

                    var dividends = (balance * percentage_dividend);

                   
                    //update dividends table 
                   
                    db.Dividends.Add(new Dividends
                    {
                        dividend_id = _dividend_id,
                        accountNo = account.AccountNo,
                        amount = dividends,
                        customerName = account.Name,
                        percentage = percentage_dividend,
                        dateCreated = DateTime.Now
                    });

                    //sum all dividends issued
                    initial_dividend_amount = initial_dividend_amount + dividends;

                  

                    //notify admin
                    foreach (var email in admin_emails)
                    {
                        EmailHelper.SendMail(email.ToString(), "Dividend for " + DateTime.Now.ToShortDateString() + " Payment Successful!",
                            "This is to notify you that Dividends for " + DateTime.Now.ToShortDateString() +
                            " has been disbursed to customers. " + "<br><br><br>" +
                            "<b>Crmpcs Portal Admin</b>"
                           );
                    }
                  

                }
                //update dividends summary table
                db.DividendSummary.Add(new DividendSummary
                {
                    dividend_id = _dividend_id,
                    dateCreated = DateTime.Now,
                    total_amount = initial_dividend_amount,
                    percentage = percentage
                });
                db.SaveChanges();

                return true;
                
            }
            return false;

        }

        /// <summary>
        /// Recurrent task to notify admin every month of dividends to be paid
        /// </summary>
        public void notify()
        {
            //send email every month to notify admin that customers are due for dividend
            var admin_emails = db.UserProfiles.Where(x => x.role == "Admin").Select(x => x.UserName).FirstOrDefault();
            foreach(var email in admin_emails)
            {
                EmailHelper.SendMail(email.ToString(), "Dividend for " + DateTime.Now.ToShortDateString()+" is due!", 
                    "Dividends for "+ DateTime.Now.ToShortDateString()+
                    " is due to be disbursed. Please follow this link : "+"http://crmpcs.com"+
                    " to login and credit customer dividends.");
            }

            //send sms every month to notify admin that customers are due for dividend

        }

        /// <summary>
        /// returns summaries of all dividends issued out
        /// </summary>
        /// <returns></returns>
        public List<DividendSummary> getSummaries()
        {
            return db.DividendSummary.ToList();
        }

    }
}