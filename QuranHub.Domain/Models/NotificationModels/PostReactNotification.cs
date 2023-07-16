
namespace QuranHub.Domain.Models;

public class PostReactNotification : PostNotification
{
    public int ReactId { get; set; }
    public PostReact React { get; set; }
    public PostReactNotification() :base()
    { }
    public PostReactNotification(string sourceUserId, string targetUserId, string message, int postId, int postReactId) 
    :base(sourceUserId, targetUserId, message, postId)
    {
        Type = "PostReactNotification";
        ReactId = postReactId;
    }
}

