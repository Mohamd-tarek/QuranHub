
namespace QuranHub.Web.Models;

public class CommentReactViewModel 
{
    public int CommentReactId { get; set; }
    public DateTime DateTime {get; set;}
    public int Type { get; set; }
    public UserBasicInfoViewModel QuranHubUser { get; set;}
}

