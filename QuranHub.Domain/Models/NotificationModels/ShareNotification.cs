
namespace QuranHub.Domain.Models;

public class ShareNotification : PostNotification
{
    public int ShareId { get; set; }
    public Share Share { get; set; }

    public ShareNotification():base()
    { }
    public ShareNotification(string sourceUserId, string targetUserId, string message, int postId, int shareId)
    : base(sourceUserId, targetUserId, message, postId)
    {
        Type = "ShareNotification";
        ShareId = shareId;
    }
}
