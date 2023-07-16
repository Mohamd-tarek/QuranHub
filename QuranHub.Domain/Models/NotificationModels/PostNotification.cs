
namespace QuranHub.Domain.Models;

public class PostNotification : Notification 
{
    public int PostId { get; set; }
    public Post Post { get; set; }

    public PostNotification (): base()
    { }
    public PostNotification(string sourceUserId, string targetUserId, string message, int postId)
    : base(sourceUserId, targetUserId, message)
    {
        PostId = postId;
    }   
}

