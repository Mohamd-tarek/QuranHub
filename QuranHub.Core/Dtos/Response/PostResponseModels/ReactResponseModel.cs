
namespace QuranHub.Core.Dtos.Response;

public class ReactResponseModel : Response
{
    public int ReactId { get; set; }
    public DateTime DateTime {get; set;}
    public int Type { get; set; }
    public UserBasicInfoResponseModel QuranHubUser { get; set;}
}

