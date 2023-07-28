
namespace QuranHub.Domain.Models;

public class ShareablePost :Post
{
    public int SharesCount {get; set;}
    public List<PostShare> Shares { get; set; } = new();

    public List<PostShareNotification> PostShareNotifications { get; set; } = new();
    public ShareablePost():base()
    {  }

    public ShareablePost(PostPrivacy privacy, string quranHubUserId, string text, int verseId ): this()
    {

        Privacy = privacy;

        QuranHubUserId = quranHubUserId;

        Text = text;

        VerseId = verseId;

        DateTime = DateTime.Now;
    }

    public PostShare AddShare(string quranHubUserId)
    {
        var share = new PostShare(quranHubUserId, PostId);

        Shares.Add(share);

        SharesCount++;

        return share;
    }

    public PostShareNotification AddShareNotification(QuranHubUser quranHubUser, int shareId )
    {
        string message = quranHubUser.UserName + " shared  your post "
                + "\"" + ( this.Text.Length < 40 ? this.Text : this.Text.Substring(0, 40 ) + "...") + "\"";

        var ShareNotification = new PostShareNotification(quranHubUser.Id, this.QuranHubUserId, message, shareId, this.PostId);

        PostShareNotifications.Add(ShareNotification);

        return ShareNotification;
    }

    public void RemoveShare(int shareId)
    {
        Shares.Remove(new PostShare(){ ShareId = shareId});

        SharesCount--;
    }

}
