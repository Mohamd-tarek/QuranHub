
namespace QuranHub.Core.Dtos.Response;
public class NotificationResponseModel : Response
{
    public int NotificationId { get; set; }
    public DateTime DateTime {get; set;}
    public UserBasicInfoResponseModel SourceUser { get; set; }
    public string Message { get; set; }
    public bool Seen { get; set; } 
    public string Type { get; set; } 
}

