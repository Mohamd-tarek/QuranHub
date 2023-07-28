
namespace QuranHub.Domain.Models;

public class PostShareNotification : ShareNotification
{
    public int PostId { get; set; }
    public ShareablePost Post { get; set; }

    public int PostShareId { get; set; }
    public PostShare PostShare { get; set; }
    public PostShareNotification():base()
    { }
    public PostShareNotification(string sourceUserId, string targetUserId, string message, int shareId, int postId)
    : base(sourceUserId, targetUserId, message, shareId)
    {
        Type = "PostShareNotification";
        PostId = postId;
        PostShareId = shareId;
    }
}
