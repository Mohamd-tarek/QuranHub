
namespace QuranHub.Web.Models;
public class NotificationViewModel
{
    public int NotificationId { get; set; }
    public DateTime DateTime {get; set;}
    public UserBasicInfoViewModel SourceUser { get; set; }
    public string Message { get; set; }
    public bool Seen { get; set; } 
    public string Type { get; set; } 
}

