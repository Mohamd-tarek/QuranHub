using MimeKit;
using MailKit.Net.Smtp;

namespace QuranHub.Web.Services;

public class SMTPEmailSenderUsingMailKit: IEmailSender {

    private IConfiguration _configuration;
    public SMTPEmailSenderUsingMailKit(IConfiguration configuration) 
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task SendEmailAsync(string emailAddress, string subject, string htmlMessage)
    {
        try{
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_configuration["EmailService:Account"], _configuration["EmailService:Account"]));

            emailMessage.To.Add(new MailboxAddress(emailAddress, emailAddress));

            emailMessage.Subject = subject;

            emailMessage.Body = new TextPart("plain") { Text = htmlMessage };


            using (var client = new SmtpClient())
            {
                Console.WriteLine("start to send email ...");

                await client.ConnectAsync(_configuration["EmailService:Server"], int.Parse(_configuration["EmailService:Port"]), false);

                await client.AuthenticateAsync(_configuration["EmailService:Account"], _configuration["EmailService:Password"]);

                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);

                Console.WriteLine("email was sent successfully!");
            }
        }
        catch (Exception ep)
        {
            Console.WriteLine("failed to send email with the following error:");

            Console.WriteLine(ep.Message);
        }
        
    }
}
