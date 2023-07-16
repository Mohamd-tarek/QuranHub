
namespace QuranHub.Domain.Models;

public class Notification :IEquatable<Notification>
{
    public int NotificationId { get; set; }
    public DateTime DateTime {get; set;}
    public string? SourceUserId { get; set; }
    public QuranHubUser SourceUser { get; set; }
    public string? TargetUserId { get; set; }
    public QuranHubUser TargetUser { get; set; }
    public string Message { get; set; }
    public bool Seen { get; set; } = false;
    public string Type { get; set; }

    public Notification (){}
    public Notification(string sourceUserId, string targetUserId, string message)
    { 
        SourceUserId = sourceUserId;
        TargetUserId = targetUserId;
        Message = message;
        DateTime = DateTime.Now;
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
        return NotificationId;
    }

    public bool Equals(Notification notification)
    {
        if (notification == null) return false;

        return (this.NotificationId.Equals(notification.NotificationId));
    } 
}

