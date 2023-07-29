
namespace QuranHub.Domain.Models;

public class PostCommentNotification : CommentNotification
{
    public int PostId { get; set; }
    public Post Post { get; set; }

    public int PostCommentId { get; set; }
    public PostComment PostComment { get; set; }

    public PostCommentNotification():base()
    { }
    public PostCommentNotification(string sourceUserId, string targetUserId, string message, int commentId, int postId)
    : base(sourceUserId, targetUserId, message, commentId)
    {
        Type = "PostCommentNotification";
        PostId = postId;
        PostCommentId = commentId;
    }
}
