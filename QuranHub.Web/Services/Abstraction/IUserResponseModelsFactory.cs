namespace QuranHub.Web.Services;

public interface IUserResponseModelsFactory 
{
    public UserResponseModel BuildUserResponseModel(QuranHubUser user );
    public UserBasicInfoResponseModel BuildUserBasicInfoResponseModel(QuranHubUser user);
    public PostUserResponseModel BuildPostUserResponseModel(QuranHubUser user);
    public ProfileResponseModel BuildProfileResponseModel(QuranHubUser user);
    public List<UserResponseModel> BuildUsersResponseModel(List<QuranHubUser> users);
    public List<UserBasicInfoResponseModel> BuildUsersBasicInfoResponseModel(List<QuranHubUser> users);
    public AboutInfoRequestModel BuildAboutInfoResponseModel(QuranHubUser user);

}
