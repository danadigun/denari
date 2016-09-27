using CRIMAS.Models;

using OneApi.Client.Impl;
using OneApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace CRIMAS.Controllers.api
{
    public class Notification
    {
        public static bool SendMail(string emailTo, string emailSubj, string emailBody)
        {
            try
            {
                MailAddress toAddress = new MailAddress(emailTo);
                using (SmtpClient smtp = new SmtpClient())
                {
                    MailMessage message = new MailMessage();
                    message.To.Add(toAddress);
                    message.Subject = emailSubj;
                    message.Body = emailBody;
                    message.IsBodyHtml = true;
                    //smtp.EnableSsl = true; //For gmail account
                    smtp.Send(message);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public static void sendSms(string message, string[] numbers)
        {
            //var api = new PushServerWS();

            //api.sendmsg(new Random().ToString(), 3623992, "digitalforte", "psalm!!9", numbers, "Denari", message, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, 0, null, null, null, 0);
            var configuration = new OneApi.Config.Configuration("digiForte1", "Test123!");
            SMSClient smsClient = new SMSClient(configuration);

            //prepare message
            SMSRequest smsRequest = new SMSRequest("Denari", message, numbers);

            // Store request id because we can later query for the delivery status with it:
            var requestId = smsClient.SmsMessagingClient.SendSMS(smsRequest);


        }

        public static void sendDemoRequestSms(DenariCustomer customer)
        {
            string[] phoneNo = { "2349071987951" };

            string message = "Demo request from: " + customer.name +
                ", " + customer.CompanyName + ", " + customer.Location +
                ", " + customer.email+ ", "+customer.phone;

            sendSms(message, phoneNo);
        }

        public static void sendDemoRequestEmail(DenariCustomer customer)
        {
            SendMail("daniel.adigun@digitalforte.ng",
                       "Demo Request[" + customer.email + "]",
                       "Demo Request made by : " + customer.name + " " +
                       "<br><br>" + "Details" + "<br>" +
                       "================" + "<br><br>" +
                       "Name: " + customer.name + "<br>" +
                       "Company: " + customer.CompanyName + "<br>" +
                       "Phone: " + customer.phone + "<br>" +
                       "Email: " + customer.email + "<br>" +
                       "Location: " + customer.Location + "<br>"
                       );

        }


        public static void sendThankYouSms(DenariCustomer customer)
        {
            string[] phoneNo = { customer.phone };

            string message = "Thanks for being our awesome customer!"+
                " We are grateful you reached out to us."+
                " Cant wait to do great things together!. Denari Team. ";

            sendSms(message, phoneNo);
        }
    }
}