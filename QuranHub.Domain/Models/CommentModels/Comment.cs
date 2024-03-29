﻿

namespace QuranHub.Domain.Models;

public class Comment :IEquatable<Comment>
{
    public int CommentId { get; set; }
    public DateTime DateTime {get; set;}
    public string? QuranHubUserId { get; set; }
    public QuranHubUser QuranHubUser { get; set;}

    public int? VerseId { get; set; }
    public Verse Verse { get; set; }
    public string Text { get; set; }
    public int ReactsCount {get; set;}
    public List<CommentReact> CommentReacts { get; set; } = new();
    public CommentNotification CommentNotification { get; set; }
    public List<CommentReactNotification> CommentReactNotifications { get; set; } = new();


    public Comment()
    { }

    public Comment(string quranHubUserId, string text, int? verseId): this()
    {
        QuranHubUserId = quranHubUserId;

        Text = text;

        DateTime = DateTime.Now;

        VerseId = verseId;

    }

    public CommentReact AddCommentReact(string quranHubUserId, int type = 0)
    {
        try
        {
            var CommentReact = new CommentReact(quranHubUserId, CommentId, type);

            CommentReacts.Add(CommentReact);

            ReactsCount++;

            return CommentReact;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public CommentReactNotification AddCommentReactNotifiaction(QuranHubUser quranHubUser, int reactId)
    {
        try
        { 
            string message = quranHubUser.UserName + " reacted to your comment "
               + "\"" + ( this.Text.Length < 40 ? this.Text : this.Text.Substring(0, 40 ) + "...") + "\"";

            var CommentReactNotification = new CommentReactNotification(quranHubUser.Id, this.QuranHubUserId, message, this.CommentId, reactId);

            CommentReactNotifications.Add(CommentReactNotification);
            return CommentReactNotification;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public void RemoveCommentReact(int CommentReactId)
    {
        try
        { 
            CommentReacts.Remove(new CommentReact() {ReactId = CommentReactId});

            ReactsCount--;
        }
        catch (Exception ex)
        {
            return;
        }
    }

    public override bool Equals(object obj)
    {
        try
        { 
            if (obj == null) return false;

            Comment objAsPostComment = obj as Comment;

            if (objAsPostComment == null) return false;

            else return Equals(objAsPostComment);
        }
        catch (Exception ex)
        {
            return false;
        }
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

