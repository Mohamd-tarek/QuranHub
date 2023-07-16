
namespace QuranHub.Domain.Models;

public class CommentNotification : PostNotification
{
    public int CommentId { get; set; }
    public Comment Comment { get; set; }

    public CommentNotification():base()
    { }
    public CommentNotification(string sourceUserId, string targetUserId, string message, int postId, int commentId)
    : base(sourceUserId, targetUserId, message, postId)
    {
        Type = "CommentNotification";
        CommentId = commentId;
    }
}
