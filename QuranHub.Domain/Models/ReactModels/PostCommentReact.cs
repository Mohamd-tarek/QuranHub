
namespace QuranHub.Domain.Models;

public class PostCommentReact : CommentReact
{
    public int PostId { get; set; }
    public Post Post { get; set; }

    public PostCommentReactNotification PostCommentReactNotification { get; set; }

    public PostCommentReact(): base()
    {}
    public PostCommentReact(string quranHubUserId, int commentId, int postId ,int type = 0):base(quranHubUserId, commentId, type)
    {
        PostId = postId;
    }

}

