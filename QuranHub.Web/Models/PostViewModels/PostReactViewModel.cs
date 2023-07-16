
namespace QuranHub.Web.Models;

public class PostReactViewModel 
{
    public int PostReactId { get; set; }
    public DateTime DateTime {get; set;}
    public int Type { get; set; }
    public UserBasicInfoViewModel QuranHubUser { get; set;}
}

