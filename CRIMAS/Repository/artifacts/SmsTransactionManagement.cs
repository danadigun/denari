using CRIMAS.com.clickatell.api;
using CRIMAS.Models;
using OneApi.Client.Impl;
using OneApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRIMAS.Repository.artifacts
{
    public class SmsTransactionManagement
    {
        private CrimasDb _db;

        public SmsTransactionManagement()
        {
            _db = new CrimasDb();
        }

        public void sendMessage(string message, string[]numbers)
        {
            //var api = new PushServerWS();

            //api.sendmsg(new Random().ToString(), 3623992, "digitalforte", "psalm!!9", numbers, "Denari", message, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, 0, null, null, null, 0);
            var configuration = new OneApi.Config.Configuration("digiForte1", "Test123!");
            SMSClient smsClient = new SMSClient(configuration);

            //prepare message
            SMSRequest smsRequest = new SMSRequest("CrMPCS", message, numbers);

            // Store request id because we can later query for the delivery status with it:
            var requestId = smsClient.SmsMessagingClient.SendSMS(smsRequest);

            
        }

        public void sendCreditAlert(string accountNo, decimal creditAmount, string transaction_msg)
        {
            var customer = _db.Customers.Where(x => x.AccountNo == accountNo).FirstOrDefault();
            if(customer.phone != null)
            {
                string[] phoneNo = { customer.phone.ToString() };
                var credit = _db.CustomerSavings.Where(x => x.AccountNo == accountNo).ToList().Select(x => x.Credit).Sum();
                var debit = _db.CustomerSavings.Where(x => x.AccountNo == accountNo).ToList().Select(x => x.Debit).Sum();
                var balance = credit - debit;

                sendMessage("A credit of NGN" + creditAmount.ToString()
                    + " occurred on your account. Date: "
                    + DateTime.Now.ToShortDateString()
                    + " "+transaction_msg +
                    "Balance: NGN" + balance, phoneNo);
            }
           
        }

        public void sendDebitAlert(string accountNo, decimal debitAmount, string transaction_msg)
        {
            var customer = _db.Customers.Where(x => x.AccountNo == accountNo).FirstOrDefault();
            if(customer.phone != null)
            {
                string[] phoneNo = { customer.phone.ToString() };
                var credit = _db.CustomerSavings.Where(x => x.AccountNo == accountNo).ToList().Select(x => x.Credit).Sum();
                var debit = _db.CustomerSavings.Where(x => x.AccountNo == accountNo).ToList().Select(x => x.Debit).Sum();
                var balance = credit - debit;

                sendMessage("A debit of NGN" + debitAmount
                    + " occurred on your account. Date: "
                    + DateTime.Now.ToShortDateString()
                    + "Details: " + transaction_msg +
                    "Balance: NGN" + balance, phoneNo);
            }
        }
    }
}