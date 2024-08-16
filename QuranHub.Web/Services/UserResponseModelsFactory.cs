namespace QuranHub.Web.Services;

public class UserResponseModelsFactory :IUserResponseModelsFactory
{
    private IdentityDataContext _identityDataContext;
    public UserResponseModelsFactory(IdentityDataContext identityDataContext )
    {
       _identityDataContext = identityDataContext ?? throw new ArgumentNullException(nameof(identityDataContext));
    }

    public UserResponseModel BuildUserResponseModel(QuranHubUser user ) 
    {
        UserResponseModel userResponseModel = new UserResponseModel()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            ProfilePicture = user.ProfilePicture,
            NumberOfFollower = _identityDataContext.Follows.Where(f => f.FollowedId == user.Id).Count(),
            NumberOfFollowed = _identityDataContext.Follows.Where(f => f.FollowerId == user.Id).Count()

        };

        return userResponseModel;   
    }

    public UserBasicInfoResponseModel BuildUserBasicInfoResponseModel(QuranHubUser user)
    {
        UserBasicInfoResponseModel userBasicInfoResponseModel = new UserBasicInfoResponseModel()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            ProfilePicture = user.ProfilePicture
        };

        return userBasicInfoResponseModel;
    }

    public PostUserResponseModel BuildPostUserResponseModel(QuranHubUser user)
    {
        PostUserResponseModel postUserResponseModel = new PostUserResponseModel()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            ProfilePicture = user.ProfilePicture
        };

        return postUserResponseModel;
    }

    public ProfileResponseModel BuildProfileResponseModel(QuranHubUser user)
    {
        ProfileResponseModel profileResponseModel = new ProfileResponseModel()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            NumberOfFollower = _identityDataContext.Follows.Where(f => f.FollowedId == user.Id).Count(),
            NumberOfFollowed = _identityDataContext.Follows.Where(f => f.FollowerId == user.Id).Count(),
            ProfilePicture = user.ProfilePicture,
            CoverPicture = user.CoverPicture
        };

        return profileResponseModel;
    }

    public List<UserResponseModel> BuildUsersResponseModel(List<QuranHubUser> users)
    {
        List<UserResponseModel> usersModels = new List<UserResponseModel>();

        foreach(var user in users)
        {
            usersModels.Add(BuildUserResponseModel(user));
        }

        return usersModels;
    }

    public List<UserBasicInfoResponseModel> BuildUsersBasicInfoResponseModel(List<QuranHubUser> users)
    {
        List<UserBasicInfoResponseModel> userBasicInfoResponseModel = new List<UserBasicInfoResponseModel>();

        foreach (var user in users)
        {
            userBasicInfoResponseModel.Add(BuildUserBasicInfoResponseModel(user));
        }

        return userBasicInfoResponseModel;
    }

    public AboutInfoRequestModel BuildAboutInfoResponseModel(QuranHubUser user)
    {
        AboutInfoRequestModel aboutInfoResponseModel = new AboutInfoRequestModel()
        {
            DateOfBirth = user.DateOfBirth,
            Gender = user.Gender,
            Religion = user.Religion,
            AboutMe = user.AboutMe
        };
        return aboutInfoResponseModel;
    }

}
