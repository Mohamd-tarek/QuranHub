

namespace QuranHub.Domain.Models;

public class PostComment :Comment
{
    public int PostId { get; set; }
    public Post Post { get; set; }
    public List<PostCommentReact> PostCommentReacts { get; set; } = new();
    public PostCommentNotification  PostCommentNotification { get; set; }
    public List<PostCommentReactNotification> PostCommentReactNotifications { get; set; } = new();


    public PostComment():base()
    { }

    public PostComment(string quranHubUserId, int postId, string text, int? verseId):base(quranHubUserId, text, verseId)
    {
        PostId = postId;
    }

    public PostCommentReact AddPostCommentReact(string quranHubUserId, int type = 1)
    {
        try
        { 
            var PostCommentReact = new PostCommentReact(quranHubUserId, CommentId, PostId, type);

            PostCommentReacts.Add(PostCommentReact);

            ReactsCount++;

            return PostCommentReact;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public PostCommentReactNotification AddPostCommentReactNotifiaction(QuranHubUser quranHubUser, int reactId)
    {
        try
        {
            string message = quranHubUser.UserName + " reacted to your comment "
               + "\"" + ( this.Text.Length < 40 ? this.Text : this.Text.Substring(0, 40 ) + "...") + "\"";

            var CommentReactNotification = new PostCommentReactNotification(quranHubUser.Id, this.QuranHubUserId, message, this.CommentId, reactId, this.PostId);

            PostCommentReactNotifications.Add(CommentReactNotification);
            return CommentReactNotification;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public void RemovePostCommentReact(int PostCommentReactId)
    {
        try
        {
            PostCommentReacts.Remove(new PostCommentReact() {ReactId = PostCommentReactId});

            this.RemoveCommentReact(PostCommentReactId);
        }
        catch (Exception ex)
        {
            return;
        }

    }

}

