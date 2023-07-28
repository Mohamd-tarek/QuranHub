namespace QuranHub.Domain.Models;

public class Share : IEquatable<Share>
{
    public int ShareId { get; set; }
    public DateTime DateTime { get; set; }
    public string? QuranHubUserId { get; set; }
    public QuranHubUser QuranHubUser { get; set; }
    public ShareNotification ShareNotification { get; set; }

    public Share() { }

    public Share(string quranHubUserId)
    {
        QuranHubUserId = quranHubUserId;
        DateTime = DateTime.Now;
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        Share objAsShare = obj as Share;
        if (objAsShare == null) return false;
        else return Equals(objAsShare);
    }

    public override int GetHashCode()
    {
        return ShareId;
    }

    public bool Equals(Share other)
    {
        if (other == null) return false;
        return ShareId.Equals(other.ShareId);
    }
}
