using System.Threading.Tasks;
using System.Web;

namespace ServerApp.Services {
    public class ConsoleEmailSender {

        public Task SendEmailAsync(string emailAddress,
                string subject, string htmlMessage) {
            System.Console.WriteLine("---New Email----");
            System.Console.WriteLine($"To: {emailAddress}");
            System.Console.WriteLine($"Subject: {subject}");
            System.Console.WriteLine(HttpUtility.HtmlDecode(htmlMessage));
            System.Console.WriteLine("-------");
            return Task.CompletedTask;
        }
    }
}
