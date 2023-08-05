
namespace QuranHub.Domain.Models;

public class CommentReact : React
{
    public int? CommentId {get; set; }
    public Comment Comment {get; set; }

    public CommentReactNotification CommentReactNotification { get; set; }

    public CommentReact(): base()
    {}
    public CommentReact(string quranHubUserId, int commentId, int type = 0):base(quranHubUserId, type)
    {
        CommentId = commentId;
    }

}

