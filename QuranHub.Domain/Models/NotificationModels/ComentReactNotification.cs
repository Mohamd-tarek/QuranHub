
namespace QuranHub.Domain.Models;

public class CommentReactNotification : CommentNotification
{
    public int CommentReactId { get; set; } 
    public CommentReact CommentReact { get; set; }
    public CommentReactNotification():base()
    { }
    public CommentReactNotification(string sourceUserId, string targetUserId, string message, int postId, int commentId, int commentReactId)
    : base(sourceUserId, targetUserId, message, postId, commentId)
    {
        Type = "CommentReactNotification";
        CommentReactId = commentReactId;
    }
}
