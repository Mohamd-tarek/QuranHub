
namespace QuranHub.Domain.Models;

public class FollowNotification :Notification
{
    public int FollowId { get; set; }
    public Follow Follow { get; set; }

    public FollowNotification():base()
    {}
    public FollowNotification(string sourceUserId, string targetUserId, string message, int followId)
    : base(sourceUserId, targetUserId, message)
    {
        Type = "FollowNotification";
        FollowId = followId;
    }   
}
