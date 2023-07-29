
namespace QuranHub.Domain.Models;

public class Post :IEquatable<Post>
{
    public int PostId { get; set; }
    public PostPrivacy Privacy {get; set;} = PostPrivacy.Public;
    public DateTime DateTime {get; set;}
    public string? QuranHubUserId { get; set; } 
    public QuranHubUser QuranHubUser { get; set;}
    public int VerseId { get; set; }
    public Verse Verse { get; set; }
    public string Text { get; set; }
    public  int ReactsCount {get; set;}
    public  int CommentsCount { get; set;}
    public List<PostReact> PostReacts { get; set; } = new();
    public List<PostReactNotification> PostReactNotifications { get; set; } = new();
    public List<PostCommentNotification> PostCommentNotifications { get; set; } = new();
    public List<PostCommentReactNotification> PostCommentReactNotifications { get; set; } = new();
    public List<PostComment> PostComments { get; set; } = new();

    public Post()
    { }

    public PostReact AddPostReact(string quranHubUserId, int type = 0)
    {
        var React = new PostReact(quranHubUserId, PostId, type);

        PostReacts.Add(React);

        ReactsCount++;

        return React;
    }

    public PostReactNotification AddPostReactNotifiaction(QuranHubUser quranHubUser, int ReactId )
    {
        string message = quranHubUser.UserName + " reacted to your post " 
                          + "\"" + ( this.Text.Length < 40 ? this.Text : this.Text.Substring(0, 40 ) + "...") + "\"";

        var ReactNotification = new PostReactNotification(quranHubUser.Id, this.QuranHubUserId, message, ReactId, this.PostId);

        PostReactNotifications.Add(ReactNotification);

        return ReactNotification;
    }

    public void RemovePostReact(int PostReactId)
    {
        PostReacts.Remove(new PostReact() {ReactId = PostReactId});

        ReactsCount--;
    }

    public PostComment AddPostComment(string quranHubUserId, string text, int? verseId)
    {
        var Comment = new PostComment(quranHubUserId, PostId, text, verseId);

        PostComments.Add(Comment);

        CommentsCount++;

        return Comment;
    }
    public PostCommentNotification AddPostCommentNotifiaction(QuranHubUser quranHubUser, int CommentId)
    {
        string message = quranHubUser.UserName + " commented on your post "
                 + "\"" + ( this.Text.Length < 40 ? this.Text : this.Text.Substring(0, 40 ) + "...") + "\"";;

        var CommentNotification = new PostCommentNotification(quranHubUser.Id, this.QuranHubUserId, message, CommentId, this.PostId);

        PostCommentNotifications.Add(CommentNotification);

        return CommentNotification;
    }

    public void RemovePostComment(int CommentId)
    {
        PostComments.Remove(new PostComment() {CommentId = CommentId});

        CommentsCount--;
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;

        Post objAsPost = obj as Post;

        if (objAsPost == null) return false;

        else return Equals(objAsPost);
    }

    public override int GetHashCode()
    {
        return PostId;
    }

    public bool Equals(Post post)
    {
        if (post == null) return false;

        return (this.PostId.Equals(post.PostId));
    }
}
