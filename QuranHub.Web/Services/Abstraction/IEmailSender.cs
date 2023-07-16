
namespace QuranHub.Web.Services; 
public interface IEmailSender {
    Task SendEmailAsync(string emailAddress, string subject, string htmlMessage);        
}
