
namespace QuranHub.Domain.Models;

public class ReactNotification : Notification
{
    public int? ReactId { get; set; }
    public React React { get; set; }
    public ReactNotification() :base()
    { }
    public ReactNotification(string sourceUserId, string targetUserId, string message, int reactId) 
    :base(sourceUserId, targetUserId, message)
    {
        Type = "ReactNotification";
        ReactId = reactId;
    }
}

