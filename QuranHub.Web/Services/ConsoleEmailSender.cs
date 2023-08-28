
namespace QuranHub.Web.Services;

public class ConsoleEmailSender :IEmailSender
{

    public Task<bool> SendEmailAsync(MailData mailData, CancellationToken ct = default)
    {
        System.Console.WriteLine("---New Email----");
        System.Console.WriteLine($"To: {mailData.To[0]}");
        System.Console.WriteLine($"Subject: {mailData.Subject}");
        System.Console.WriteLine(HttpUtility.HtmlDecode(mailData.Body));
        System.Console.WriteLine("-------");
        return  Task.FromResult<bool>(true);
    }
}
