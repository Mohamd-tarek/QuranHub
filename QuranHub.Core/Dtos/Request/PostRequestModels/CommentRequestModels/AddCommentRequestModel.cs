
namespace QuranHub.Core.Dtos.Request;

public class AddCommentRequestModel : Request
{
    public int CommentId { get; set; }
    public DateTime DateTime {get; set;}
    public UserBasicInfoResponseModel QuranHubUser { get; set;}
    public VerseResponseModel? Verse { get; set; }
    public bool ReactedTo {get; set;}
    public int ReactsCount {get; set;}
    public string Text { get; set; }
}

