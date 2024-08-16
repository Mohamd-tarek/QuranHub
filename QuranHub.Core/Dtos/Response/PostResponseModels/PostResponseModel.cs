

namespace QuranHub.Core.Dtos.Response;

public class PostResponseModel : Response
{
    public int PostId { get; set; }
    public PostPrivacy Privacy {get; set;} = PostPrivacy.Public;
    public DateTime DateTime {get; set;}
    public PostUserResponseModel QuranHubUser { get; set;}
    public bool ReactedTo {get; set;}
    public VerseResponseModel Verse { get; set; }
    public string Text { get; set; }
    public int ReactsCount {get; set;}
    public int CommentsCount { get; set;}
    public IEnumerable<CommentResponseModel> Comments { get; set; }
}
