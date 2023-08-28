using EASendMail;


namespace QuranHub.Web.Services;

public class SMTPEmailSenderUsingEASendEmail: IEmailSender {

    private IConfiguration _configuration;
    public SMTPEmailSenderUsingEASendEmail(IConfiguration configuration) 
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<bool> SendEmailAsync(MailData mailData, CancellationToken ct = default)
    {
       throw new NotImplementedException();
    }
}
