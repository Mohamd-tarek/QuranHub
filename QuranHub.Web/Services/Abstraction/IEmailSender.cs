
namespace QuranHub.Web.Services; 
public interface IEmailSender {
    Task<bool> SendEmailAsync(MailData mailData, CancellationToken ct = default);        
}
