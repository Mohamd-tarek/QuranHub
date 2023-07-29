
namespace QuranHub.Domain.Models;

public class ShareNotification : Notification
{
    public int? ShareId { get; set; }
    public Share Share { get; set; }

    public ShareNotification():base()
    { }
    public ShareNotification(string sourceUserId, string targetUserId, string message, int shareId)
    : base(sourceUserId, targetUserId, message)
    {
        Type = "ShareNotification";
        ShareId = shareId;
    }
}
