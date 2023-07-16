
namespace QuranHub.Web.Services;

public class UserViewModelsFactory :IUserViewModelsFactory
{
    private IdentityDataContext _identityDataContext;
    public UserViewModelsFactory(IdentityDataContext identityDataContext )
    {
       _identityDataContext = identityDataContext ?? throw new ArgumentNullException(nameof(identityDataContext));
    }

    public UserViewModel BuildUserViewModel(QuranHubUser user ) 
    {
        UserViewModel userViewModel = new UserViewModel()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            ProfilePicture = user.ProfilePicture,
            NumberOfFollower = _identityDataContext.Follows.Where(f => f.FollowedId == user.Id).Count(),
            NumberOfFollowed = _identityDataContext.Follows.Where(f => f.FollowerId == user.Id).Count()

        };

        return userViewModel;   
    }

    public UserBasicInfoViewModel BuildUserBasicInfoViewModel(QuranHubUser user)
    {
        UserBasicInfoViewModel userBasicInfoViewModel = new UserBasicInfoViewModel()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            ProfilePicture = user.ProfilePicture
        };

        return userBasicInfoViewModel;
    }

    public PostUserViewModel BuildPostUserViewModel(QuranHubUser user)
    {
        PostUserViewModel postUserViewModel = new PostUserViewModel()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            ProfilePicture = user.ProfilePicture
        };

        return postUserViewModel;
    }

    public ProfileViewModel BuildProfileViewModel(QuranHubUser user)
    {
        ProfileViewModel profileViewModel = new ProfileViewModel()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            NumberOfFollower = _identityDataContext.Follows.Where(f => f.FollowedId == user.Id).Count(),
            NumberOfFollowed = _identityDataContext.Follows.Where(f => f.FollowerId == user.Id).Count(),
            ProfilePicture = user.ProfilePicture,
            CoverPicture = user.CoverPicture
        };

        return profileViewModel;
    }

    public List<UserViewModel> BuildUsersViewModel(List<QuranHubUser> users)
    {
        List<UserViewModel> usersModels = new List<UserViewModel>();

        foreach(var user in users)
        {
            usersModels.Add(BuildUserViewModel(user));
        }

        return usersModels;
    }

    public List<UserBasicInfoViewModel> BuildUsersBasicInfoViewModel(List<QuranHubUser> users)
    {
        List<UserBasicInfoViewModel> userBasicInfoViewModel = new List<UserBasicInfoViewModel>();

        foreach (var user in users)
        {
            userBasicInfoViewModel.Add(BuildUserBasicInfoViewModel(user));
        }

        return userBasicInfoViewModel;
    }

    public AboutInfoViewModel BuildAboutInfoViewModel(QuranHubUser user)
    {
        AboutInfoViewModel aboutInfoViewModel = new AboutInfoViewModel()
        {
            DateOfBirth = user.DateOfBirth,
            Gender = user.Gender,
            Religion = user.Religion,
            AboutMe = user.AboutMe
        };
        return aboutInfoViewModel;
    }

}
