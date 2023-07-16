
namespace QuranHub.Domain.Models;

public class CommentReact : React
{
    public int CommentId {get; set; }
    public Comment Comment {get; set; }

    public CommentReactNotification ReactNotification { get; set; }

    public CommentReact(): base()
    {}
    public CommentReact(string quranHubUserId, int CommentId, int type = 0):base(type, quranHubUserId)
    {
        CommentId = CommentId;
    }

}

