

namespace QuranHub.Domain.Models;

public class Comment :IEquatable<Comment>
{
    public int CommentId { get; set; }
    public DateTime DateTime {get; set;}
    public string? QuranHubUserId { get; set; }
    public QuranHubUser QuranHubUser { get; set;}

    public int? VerseId { get; set; }
    public Verse Verse { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
    public string Text { get; set; }
    public int ReactsCount {get; set;}
    public List<CommentReact> Reacts { get; set; }
    public List<CommentNotification> CommentNotifications { get; set; }


    public Comment()
    {
        Reacts = new List<CommentReact>();

        CommentNotifications = new List<CommentNotification>();
    }

    public Comment(string quranHubUserId, int postId, string text, int? verseId): this()
    {
        QuranHubUserId = quranHubUserId;

        PostId = postId;

        Text = text;

        DateTime = DateTime.Now;

        VerseId = verseId;

    }

    public CommentReact AddCommentReact(string quranHubUserId, int type = 0)
    {
        var CommentReact = new CommentReact(quranHubUserId, type, CommentId);

        Reacts.Add(CommentReact);

        ReactsCount++;

        return CommentReact;
    }

    public CommentReactNotification AddCommentReactNotifiaction(QuranHubUser quranHubUser, int reactId)
    {
        string message = quranHubUser.UserName + " reacted to your comment "
           + "\"" + ( this.Text.Length < 40 ? this.Text : this.Text.Substring(0, 40 ) + "...") + "\"";

        var CommentReactNotification = new CommentReactNotification(quranHubUser.Id, this.QuranHubUserId, message, this.Post.PostId, this.CommentId, reactId);

        CommentNotifications.Add(CommentReactNotification);
        return CommentReactNotification;
    }

    public void RemoveCommentReact(int CommentReactId)
    {
        Reacts.Remove(new CommentReact() {ReactId = CommentReactId});

        ReactsCount--;
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;

        Comment objAsPostComment = obj as Comment;

        if (objAsPostComment == null) return false;

        else return Equals(objAsPostComment);
    }

    public override int GetHashCode()
    {
        return CommentId;
    }

    public bool Equals(Comment other)
    {
        if (other == null) return false;

        return (this.CommentId.Equals(other.CommentId));
    }
}

