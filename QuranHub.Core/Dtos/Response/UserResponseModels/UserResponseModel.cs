namespace QuranHub.Core.Dtos.Response;

public class UserResponseModel :UserBasicInfoResponseModel
{
    public int NumberOfFollower{ get; set;}
    public int NumberOfFollowed{ get; set;}
}