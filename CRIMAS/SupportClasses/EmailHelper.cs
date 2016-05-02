using System.Net.Mail;

namespace CRIMAS.SupportClasses
{
    public class EmailHelper
    {
        public static bool SendMail(string emailTo, string emailFrom, string emailSubj, string emailBody)
        {
            try
            {
                MailAddress toAddress = new MailAddress(emailTo);
                MailAddress fromAddress = new MailAddress(emailFrom);
                using (SmtpClient smtp = new SmtpClient())
                {
                    MailMessage message = new MailMessage();
                    message.To.Add(toAddress);
                    message.From = fromAddress;
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

    }
}