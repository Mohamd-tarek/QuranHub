
namespace QuranHub.Web.Services;

public interface IEmailService {

 Task SendPasswordRecoveryEmail(QuranHubUser user,  string confirmationURL);

 Task SendAccountConfirmEmail(QuranHubUser user, string confirmationURL); 

 Task SendChangeEmailConfirmEmail(QuranHubUser user, string newEmail, string confirmationURL);
}
