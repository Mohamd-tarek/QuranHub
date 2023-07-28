

namespace QuranHub.Domain.Models;

public class PostComment :Comment
{
    public int PostId { get; set; }
    public Post Post { get; set; }
    public List<PostCommentReact> Reacts { get; set; }
    public PostCommentNotification  PostCommentNotification { get; set; }
    public List<PostCommentReactNotification> PostCommentReactNotifications { get; set; }


    public PostComment():base()
    {
        Reacts = new List<PostCommentReact>();

        PostCommentReactNotifications = new List<PostCommentReactNotification>();
    }

    public PostComment(string quranHubUserId, int postId, string text, int? verseId):base(quranHubUserId, text, verseId)
    {
        PostId = postId;
    }

    public PostCommentReact AddPostCommentReact(string quranHubUserId, int type = 0)
    {
        var PostCommentReact = new PostCommentReact(quranHubUserId, type, PostId, CommentId);

        Reacts.Add(PostCommentReact);

        ReactsCount++;

        return PostCommentReact;
    }

    public PostCommentReactNotification AddPostCommentReactNotifiaction(QuranHubUser quranHubUser, int reactId)
    {
        string message = quranHubUser.UserName + " reacted to your comment "
           + "\"" + ( this.Text.Length < 40 ? this.Text : this.Text.Substring(0, 40 ) + "...") + "\"";

        var CommentReactNotification = new PostCommentReactNotification(quranHubUser.Id, this.QuranHubUserId, message, this.Post.PostId, this.CommentId, reactId);

        PostCommentReactNotifications.Add(CommentReactNotification);
        return CommentReactNotification;
    }

    public void RemovePostCommentReact(int PostCommentReactId)
    {
        Reacts.Remove(new PostCommentReact() {ReactId = PostCommentReactId});

        ReactsCount--;
    }

}

