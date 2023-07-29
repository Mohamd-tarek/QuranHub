
namespace QuranHub.Domain.Models;

public class CommentNotification : Notification
{
    public int? CommentId { get; set; }
    public Comment Comment { get; set; }

    public CommentNotification():base()
    { }
    public CommentNotification(string sourceUserId, string targetUserId, string message, int commentId)
    : base(sourceUserId, targetUserId, message)
    {
        Type = "CommentNotification";
        CommentId = commentId;
    }
}
