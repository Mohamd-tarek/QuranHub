using System;
using System.Threading.Tasks;
using System.Web;
using EASendMail;
using System.Collections.Generic;
using System.Text;

namespace ServerApp.Services {
    public class SMTPEmailSender {

        public  Task SendEmailAsync(string emailAddress,
                string subject, string htmlMessage) {
            try
            {
                SmtpMail oMail = new SmtpMail("experimental");

                // Your  email address
                oMail.From = "your email account";

                // Set recipient email address
                oMail.To = emailAddress;

                // Set email subject
                oMail.Subject = subject;

                // Set email body
                oMail.TextBody = htmlMessage;

                //  SMTP server address
                SmtpServer oServer = new SmtpServer("email host");

                // For example: your email is "myid@yahoo.com", then the user should be "myid@yahoo.com"
                oServer.User = "your email account";
                oServer.Password = "your password";


                
                // the the SMTP  Email Server listen on 
                oServer.Port = 465;

                // detect SSL type automatically
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                Console.WriteLine("start to send email over SSL ...");

                SmtpClient oSmtp = new SmtpClient();
                oSmtp.SendMail(oServer, oMail);

                Console.WriteLine("email was sent successfully!");
            }
            catch (Exception ep)
            {
                Console.WriteLine("failed to send email with the following error:");
                Console.WriteLine(ep.Message);
            }
            return Task.CompletedTask;
        }
    }
}
