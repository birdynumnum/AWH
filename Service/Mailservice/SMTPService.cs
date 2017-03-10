using Providers.MailProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Diagnostics;

namespace Service.Mailservice
{
    public class SMTPService : IMailer
    {
        public void SendMail(string from, string to, string subject, string username, string pw)
        {
            StringBuilder sbmailbody = new StringBuilder();
            sbmailbody.AppendLine("Hi There " + username);
            sbmailbody.AppendLine(Environment.NewLine);
            sbmailbody.Append("\n");
            sbmailbody.AppendLine("You can pick a present (or presents) for Ava from the website  www.avawilhebben.nl");
            sbmailbody.AppendLine(Environment.NewLine);
            sbmailbody.Append("\n");
            sbmailbody.AppendLine("Your inlogname is : " + username + "  ");

            sbmailbody.AppendLine("and your password is :  " + pw);
            sbmailbody.AppendLine(Environment.NewLine);
            sbmailbody.Append("\n");
            sbmailbody.AppendLine("Enjoy!");
            sbmailbody.AppendLine(Environment.NewLine);
            sbmailbody.Append("\n");
            sbmailbody.AppendLine(from);

            var mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(to);
            mail.Subject = "Hi there";
            mail.IsBodyHtml = false;
            mail.Body = sbmailbody.ToString();


            var SmtpServer = new SmtpClient("smtp.ziggo.nl");
            SmtpServer.Port = 587;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.EnableSsl = true;

            try
            {
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.ToString());
            }
        }
    }
}
