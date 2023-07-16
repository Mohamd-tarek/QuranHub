
namespace QuranHub.Web.Models;

public class ShareViewModel 
{
    public int ShareId { get; set; }
    public ShareablePostViewModel Post {get; set; }
    public DateTime DateTime {get; set;}
    public UserBasicInfoViewModel QuranHubUser{ get; set;}
}
