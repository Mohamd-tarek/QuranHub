
namespace QuranHub.Core.Dtos.Request;

public class SharePostRequestModel : Request
{
    public PostPrivacy Privacy { get; set; }
    public int VerseId { get; set; }
    public string Text { get; set; }
    public string QuranHubUserId { get; set; }
     public int PostId { get; set; }
}
