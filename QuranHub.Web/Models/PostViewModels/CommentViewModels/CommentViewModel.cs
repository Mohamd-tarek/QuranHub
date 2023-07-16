
namespace QuranHub.Web.Models;

public class CommentViewModel 
{
    public int CommentId { get; set; }
    public DateTime DateTime {get; set;}
    public UserBasicInfoViewModel QuranHubUser { get; set;}
    public VerseViewModel? Verse { get; set; }
    public bool ReactedTo {get; set;}
    public int ReactsCount {get; set;}
    public string Text { get; set; }
}

