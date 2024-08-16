
namespace QuranHub.Core.Dtos.Response;

public class ShareResponseModel : Response
{
    public int ShareId { get; set; }
    public DateTime DateTime {get; set;}
    public UserBasicInfoResponseModel QuranHubUser{ get; set;}
}
