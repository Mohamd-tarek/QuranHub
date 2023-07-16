using EASendMail;


namespace QuranHub.Web.Services;

public class SMTPEmailSenderUsingEASendEmail: IEmailSender {

    private IConfiguration _configuration;
    public SMTPEmailSenderUsingEASendEmail(IConfiguration configuration) 
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task SendEmailAsync(string emailAddress, string subject, string htmlMessage)
    {
        try{
            SmtpMail oMail = new SmtpMail("experimental");

            // Your  email address
            oMail.From = _configuration["EmailService:Account"];

            // Set recipient email address
            oMail.To = emailAddress;

            // Set email subject
            oMail.Subject = subject;

            // Set email body
            oMail.TextBody = htmlMessage;

            //  SMTP server address
            SmtpServer oServer = new SmtpServer("email host");

            // For example: your email is "myid@yahoo.com", then the user should be "myid@yahoo.com"
            oServer.User = _configuration["EmailService:Account"];
            oServer.Password = _configuration["EmailService:Password"];



            // the the SMTP  Email Server listen on 
            oServer.Port = int.Parse(_configuration["EmailService:Port"]);

            // detect SSL type automatically
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            Console.WriteLine("start to send email over SSL ...");

            SmtpClient oSmtp = new SmtpClient();
            await oSmtp.SendMailAsync(oServer, oMail);

            Console.WriteLine("email was sent successfully!");
        }
        catch (Exception ep)
        {
            Console.WriteLine("failed to send email with the following error:");

            Console.WriteLine(ep.Message);
        }
        
    }
}
