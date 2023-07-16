namespace QuranHub.Web.Services;

public interface IUserViewModelsFactory 
{
    public UserViewModel BuildUserViewModel(QuranHubUser user );
    public UserBasicInfoViewModel BuildUserBasicInfoViewModel(QuranHubUser user);
    public PostUserViewModel BuildPostUserViewModel(QuranHubUser user);
    public ProfileViewModel BuildProfileViewModel(QuranHubUser user);
    public List<UserViewModel> BuildUsersViewModel(List<QuranHubUser> users);
    public List<UserBasicInfoViewModel> BuildUsersBasicInfoViewModel(List<QuranHubUser> users);
    public AboutInfoViewModel BuildAboutInfoViewModel(QuranHubUser user);

}
