

namespace QuranHub.Web.Models;

public class PostViewModel 
{
    public int PostId { get; set; }
    public PostPrivacy Privacy {get; set;} = PostPrivacy.Public;
    public DateTime DateTime {get; set;}
    public PostUserViewModel QuranHubUser { get; set;}
    public bool ReactedTo {get; set;}
    public VerseViewModel Verse { get; set; }
    public string Text { get; set; }
    public int ReactsCount {get; set;}
    public int CommentsCount { get; set;}
    public IEnumerable<CommentViewModel> Comments { get; set; }
}
