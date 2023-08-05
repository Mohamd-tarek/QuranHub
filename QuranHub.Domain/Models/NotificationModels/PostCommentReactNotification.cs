
namespace QuranHub.Domain.Models;

public class PostCommentReactNotification : CommentReactNotification
{
    public int PostId { get; set; }
    public Post Post { get; set; }
    public int PostCommentReactId { get; set; }
    public PostCommentReact PostCommentReact { get; set; }
    public PostCommentReactNotification():base()
    { }
    public PostCommentReactNotification(string sourceUserId, string targetUserId, string message, int commentId, int commentReactId, int postId)
    : base(sourceUserId, targetUserId, message, commentId, commentReactId)
    {
        Type = "PostCommentReactNotification";
        PostId = postId;
        PostCommentReactId = commentReactId;
    }
}
