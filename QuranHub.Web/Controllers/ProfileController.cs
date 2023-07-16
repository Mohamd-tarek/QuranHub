
namespace QuranHub.Web.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private ILogger<ProfileController> _logger; 
    private IProfileService _profileService;
    private IUserViewModelsFactory _userViewModelsFactory;
    private IHubContext<NotificationHub> _notificationHubContext;
    private INotificationViewModelsFactory _notificationViewModelsFactory;
    private IViewModelsService _viewModelsService;
    private UserManager<QuranHubUser> _userManager;
    private HttpContext _httpContext;
    private QuranHubUser _currentUser;

    public ProfileController(
        UserManager<QuranHubUser> userManager,
        ILogger<ProfileController> logger, 
        IProfileService profileService,
        IUserViewModelsFactory userViewModelsFactor,
        IHubContext<NotificationHub> notificationHubContext,
        INotificationViewModelsFactory notificationViewModelsFactory,
        IViewModelsService viewModelsService,
        IHttpContextAccessor httpContextAccessor)
    {
       _logger = logger ?? throw new ArgumentNullException(nameof(logger));
       _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
       _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));
       _userViewModelsFactory = userViewModelsFactor ?? throw new ArgumentNullException(nameof(userViewModelsFactor));
       _notificationHubContext = notificationHubContext ?? throw new ArgumentNullException(nameof(notificationHubContext));
       _viewModelsService = viewModelsService ?? throw new ArgumentNullException(nameof(viewModelsService));
       _notificationViewModelsFactory = notificationViewModelsFactory ?? throw new ArgumentNullException(nameof(notificationViewModelsFactory));
       _httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor));

       ClaimsPrincipal claimsPrincipal = this._httpContext.User;

        if (claimsPrincipal.Identity.IsAuthenticated)
        {
            _currentUser = _userManager.GetUserAsync(this._httpContext.User).Result;
        }
    }

    [HttpGet("UserPosts/{UserId}")]
    public async Task<IEnumerable<object>> GetUserPostsAsync(string userId) 
    {
        List<ShareablePost> posts = await _profileService.GetUserShareablePostsAsync(userId);

        List<SharedPost> sharedPosts = await _profileService.GetUserSharedPostsAsync(userId);

        List<object> mergedPosts = await _viewModelsService.MergePostsAsync(posts, sharedPosts);

        return mergedPosts;
    }

    [HttpGet("UserFollowers/{UserId}")]
    public async Task<IEnumerable<UserBasicInfoViewModel>> GetUserFollowersAsync(string userId) 
    {
        List <QuranHubUser> users = await _profileService.GetUserFollowersAsync(userId);

        List<UserBasicInfoViewModel> usersViewModels = _userViewModelsFactory.BuildUsersBasicInfoViewModel(users);

        return usersViewModels;
    }

    [HttpGet("UserFollowings/{UserId}")]
    public async Task<IEnumerable<UserBasicInfoViewModel>> GetUserFollowingsAsync(string userId)
    {
        List<QuranHubUser> users = await _profileService.GetUserFollowingsAsync(userId);

        List<UserBasicInfoViewModel> usersViewModels = _userViewModelsFactory.BuildUsersBasicInfoViewModel(users);

        return usersViewModels;
    }

    [HttpGet("UserFollowers/{UserId}/{KeyWord}")]
    public async Task<IEnumerable<UserBasicInfoViewModel>> GetUserFollowersAsync(string userId, string KeyWord) 
    {
        List<QuranHubUser> users = await _profileService.GetUserFollowersAsync(userId, KeyWord);

        List<UserBasicInfoViewModel> usersViewModels = _userViewModelsFactory.BuildUsersBasicInfoViewModel(users);

        return usersViewModels;
    }

    [HttpGet("UserFollowings/{UserId}/{KeyWord}")]
    public async Task<IEnumerable<UserBasicInfoViewModel>> GetUserFollowingsAsync(string userId, string KeyWord)
    {
        List<QuranHubUser> users = await _profileService.GetUserFollowingsAsync(userId, KeyWord);

        List<UserBasicInfoViewModel> usersViewModels = _userViewModelsFactory.BuildUsersBasicInfoViewModel(users);

        return usersViewModels;
    }      

    [HttpGet("UserProfile/{UserId}")]
    public async Task<ProfileViewModel> GetUserProfileAsync(string userId)
    {
        QuranHubUser user = await _userManager.FindByIdAsync(userId);

        ProfileViewModel profileModel = _userViewModelsFactory.BuildProfileViewModel(user);

        return profileModel;
    }

    [HttpPost("editCoverPicture")]
    public async Task<byte[]> PostEditCoverPicture([FromForm] CoverPictureModel coverPictureModel) 
    {
        IFormFile formFile = coverPictureModel.CoverPictureFile;

        byte[] coverPicture = _viewModelsService.ReadFileIntoArray(formFile);

        return await _profileService.EditCoverPictureAsync(coverPicture, _currentUser);
    }

    [HttpPost("editProfilePicture")]
    public async Task<byte[]> PostEditProfilePicture([FromForm] ProfilePictureModel profilePictureModel)
    {
        QuranHubUser user = await _userManager.GetUserAsync(User);

        IFormFile formFile = profilePictureModel.ProfilePictureFile;

        byte[] profilePicture = _viewModelsService.ReadFileIntoArray(formFile);

        return await _profileService.EditProfilePictureAsync(profilePicture,_currentUser);

    }

    [HttpGet("CheckFollowing/{UserId}")]
    public async Task<bool> GetCheckFollowingAsync(string userId)
    {
        return await _profileService.CheckFollowingAsync(_currentUser.Id, userId);   
    }

    [HttpPost("FollowUser")]
    public async Task<bool> FollowUser([FromBody] Follow follow) 
    {
        Tuple<bool, FollowNotification> result = await _profileService.AddFollowAsync(follow, _currentUser);

        if(result.Item1)
        {
            var user = await _userManager.FindByIdAsync(result.Item2.TargetUserId);

            if (user.Online)
            {
                FollowNotificationViewModel notification = this._notificationViewModelsFactory.BuildFollowNotificationViewModel(result.Item2);

                await this._notificationHubContext.Clients.Client(user.ConnectionId).SendAsync("RecieveNotification", notification);
            }

            return true;

        }
        return false;

    }

    [HttpPost("UnfollowUser")]
    public async Task<bool> UnfollowUser([FromBody] Follow follow) 
    {
        return await _profileService.RemoveFollowAsync(follow);
    }

    [HttpGet("AboutInfo/{UserId}")]
    public async Task<AboutInfoViewModel> GetAboutInfoAsync(string userId)
    {
        QuranHubUser user =  await _userManager.FindByIdAsync(userId);

        return _userViewModelsFactory.BuildAboutInfoViewModel(user);
    }
}
