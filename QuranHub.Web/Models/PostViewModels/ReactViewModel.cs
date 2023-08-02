
namespace QuranHub.Web.Models;

public class ReactViewModel 
{
    public int ReactId { get; set; }
    public DateTime DateTime {get; set;}
    public int Type { get; set; }
    public UserBasicInfoViewModel QuranHubUser { get; set;}
}

