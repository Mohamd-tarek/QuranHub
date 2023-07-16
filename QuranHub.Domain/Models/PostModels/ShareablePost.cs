
namespace QuranHub.Domain.Models;

public class ShareablePost :Post
{
    public int SharesCount {get; set;}
    public List<Share> Shares { get; set; }
    public ShareablePost():base()
    {
        Shares = new  List<Share>();
    }

    public ShareablePost(PostPrivacy privacy, string quranHubUserId, string text, int verseId ): this()
    {

        Privacy = privacy;

        QuranHubUserId = quranHubUserId;

        Text = text;

        VerseId = verseId;

        DateTime = DateTime.Now;
    }

    public Share AddShare(string quranHubUserId)
    {
        var share = new Share(quranHubUserId, PostId);

        Shares.Add(share);

        SharesCount++;

        return share;
    }

    public ShareNotification AddShareNotification(QuranHubUser quranHubUser, int shareId )
    {
        string message = quranHubUser.UserName + " shared  your post "
                + "\"" + ( this.Text.Length < 40 ? this.Text : this.Text.Substring(0, 40 ) + "...") + "\"";

        var ShareNotification = new ShareNotification(quranHubUser.Id, this.QuranHubUserId, message, this.PostId, shareId);

        PostNotifications.Add(ShareNotification);

        return ShareNotification;
    }

    public void RemoveShare(int shareId)
    {
        Shares.Remove(new Share(){ ShareId = shareId});

        SharesCount--;
    }

}
