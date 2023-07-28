namespace QuranHub.Domain.Models;

public class PostShare : Share
{
    public int PostId { get; set; }
    public ShareablePost Post { get; set; }
    public SharedPost SharedPost { get; set; }
    public PostShareNotification PostShareNotification { get; set; }

    public PostShare():base() { }

    public PostShare(string quranHubUserId, int postId):base(quranHubUserId)
    {
        PostId = postId;
    }
}
