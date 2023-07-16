namespace QuranHub.Domain.Models;

public class SharedPost : Post
{
    public int? ShareId { get; set; }
    public Share Share { get; set; }
    public SharedPost() : base()
    {}
    public SharedPost(PostPrivacy privacy, string quranHubUserId, string text, int verseId) : this()
    {
        Privacy = privacy;

        QuranHubUserId = quranHubUserId;

        Text = text;

        VerseId = verseId;

        DateTime = DateTime.Now;
    }
}

