
namespace QuranHub.Domain.Models;

public class CommentReactNotification : ReactNotification
{
    public int CommentId { get; set; } 
    public Comment Comment { get; set; }
    public int CommentReactId { get; set; }
    public CommentReact CommentReact { get; set; }
    public CommentReactNotification():base()
    { }
    public CommentReactNotification(string sourceUserId, string targetUserId, string message, int commentId, int commentReactId)
    : base(sourceUserId, targetUserId, message, commentReactId)
    {
        Type = "CommentReactNotification";
        CommentId = commentId;
        CommentReactId = commentReactId;
    }
}
