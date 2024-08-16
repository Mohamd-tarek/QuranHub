

namespace QuranHub.Core.Dtos.Request;

public class EditPostRequestModel : Request
{
    public int PostId { get; set; }
    public PostPrivacy Privacy { get; set; }
    public int VerseId { get; set; }
    public string Text { get; set; }
}
