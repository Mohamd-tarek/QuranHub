
namespace QuranHub.Domain.Models;

public class PostReactNotification : ReactNotification
{
    public int PostId { get; set; }
    public Post Post { get; set; }
    public int PostReactId { get; set; }
    public PostReact PostReact { get; set; }
    public PostReactNotification() :base()
    { }
    public PostReactNotification(string sourceUserId, string targetUserId, string message,  int postReactId, int postId) 
    :base(sourceUserId, targetUserId, message, postReactId)
    {
        Type = "PostReactNotification";
        PostReactId = postReactId;
        PostId = postId;
    }
}

