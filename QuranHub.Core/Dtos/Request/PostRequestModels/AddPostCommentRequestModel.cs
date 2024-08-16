
namespace QuranHub.Core.Dtos.Request;

public class AddPostCommentRequestModel : Request
{
    public int PostId { get; set; }
    public int? VerseId { get; set; }
    public string Text { get; set; }
    public string QuranHubUserId { get; set; }
}

