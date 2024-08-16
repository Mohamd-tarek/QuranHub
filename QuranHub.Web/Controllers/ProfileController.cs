namespace QuranHub.Web.Controllers;


[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

public class ProfileController : ControllerBase
{
    private readonly Serilog.ILogger _logger;
    private IProfileService _profileService;
    private IUserResponseModelsFactory _userResponseModelsFactory;
    private IHubContext<NotificationHub> _notificationHubContext;
    private INotificationResponseModelsFactory _notificationResponseModelsFactory;
    private IResponseModelsService _responseModelsService;
    private UserManager<QuranHubUser> _userManager;
    private HttpContext _httpContext;
    private QuranHubUser _currentUser;

    public ProfileController(
        UserManager<QuranHubUser> userManager,
        Serilog.ILogger logger,
        IProfileService profileService,
        IUserResponseModelsFactory userResponseModelsFactor,
        IHubContext<NotificationHub> notificationHubContext,
        INotificationResponseModelsFactory notificationResponseModelsFactory,
        IResponseModelsService responseModelsService,
        IHttpContextAccessor httpContextAccessor)
    {
       _logger = logger ?? throw new ArgumentNullException(nameof(logger));
       _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
       _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));
       _userResponseModelsFactory = userResponseModelsFactor ?? throw new ArgumentNullException(nameof(userResponseModelsFactor));
       _notificationHubContext = notificationHubContext ?? throw new ArgumentNullException(nameof(notificationHubContext));
       _responseModelsService = responseModelsService ?? throw new ArgumentNullException(nameof(responseModelsService));
       _notificationResponseModelsFactory = notificationResponseModelsFactory ?? throw new ArgumentNullException(nameof(notificationResponseModelsFactory));
       _httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor));

       ClaimsPrincipal claimsPrincipal = this._httpContext.User;

        if (claimsPrincipal.Identity.IsAuthenticated)
        {
            _currentUser = _userManager.GetUserAsync(this._httpContext.User).Result;
        }
    }

    [HttpGet(Router.Profile.UserPosts)]
    public async Task<ActionResult<IEnumerable<object>>> GetUserPostsAsync(string userId) 
    {
        try
        {
            List<ShareablePost> posts = await _profileService.GetUserShareablePostsAsync(userId);

            List<SharedPost> sharedPosts = await _profileService.GetUserSharedPostsAsync(userId);

            List<object> mergedPosts = await _responseModelsService.MergePostsAsync(posts, sharedPosts);

            return Ok(mergedPosts);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Profile.UserFollowers)]
    public async Task<ActionResult<IEnumerable<UserBasicInfoResponseModel>>> GetUserFollowersAsync(string userId) 
    {
        try
        {
            List <QuranHubUser> users = await _profileService.GetUserFollowersAsync(userId);

            List<UserBasicInfoResponseModel> usersResponseModels = _userResponseModelsFactory.BuildUsersBasicInfoResponseModel(users);

            return Ok(usersResponseModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Profile.UserFollowings)]
    public async Task<ActionResult<IEnumerable<UserBasicInfoResponseModel>>> GetUserFollowingsAsync(string userId)
    {
        try
        {
            List<QuranHubUser> users = await _profileService.GetUserFollowingsAsync(userId);

            List<UserBasicInfoResponseModel> usersResponseModels = _userResponseModelsFactory.BuildUsersBasicInfoResponseModel(users);

            return Ok(usersResponseModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Profile.SearchUserFollowers)]
    public async Task<ActionResult<IEnumerable<UserBasicInfoResponseModel>>> GetUserFollowersAsync(string userId, string KeyWord) 
    {
        try
        {
            List<QuranHubUser> users = await _profileService.GetUserFollowersAsync(userId, KeyWord);

            List<UserBasicInfoResponseModel> usersResponseModels = _userResponseModelsFactory.BuildUsersBasicInfoResponseModel(users);

            return Ok(usersResponseModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Profile.SearchUserFollowings)]
    public async Task<ActionResult<IEnumerable<UserBasicInfoResponseModel>>> GetUserFollowingsAsync(string userId, string KeyWord)
    {
        try
        {
            List<QuranHubUser> users = await _profileService.GetUserFollowingsAsync(userId, KeyWord);

            List<UserBasicInfoResponseModel> usersResponseModels = _userResponseModelsFactory.BuildUsersBasicInfoResponseModel(users);

            return Ok(usersResponseModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }      

    [HttpGet(Router.Profile.UserProfile)]
    public async Task<ActionResult<ProfileResponseModel>> GetUserProfileAsync(string userId)
    {
        try
        {
            QuranHubUser user = await _userManager.FindByIdAsync(userId);

            ProfileResponseModel profileModel = _userResponseModelsFactory.BuildProfileResponseModel(user);

            return Ok(profileModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost(Router.Profile.EditCoverPicture)]
    public async Task<ActionResult<byte[]>> PostEditCoverPicture([FromForm] CoverPictureRequestModel coverPictureModel) 
    {
        try
        {
            IFormFile formFile = coverPictureModel.CoverPictureFile;

            byte[] coverPicture = _responseModelsService.ReadFileIntoArray(formFile);

            return Ok(await _profileService.EditCoverPictureAsync(coverPicture, _currentUser));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost(Router.Profile.EditProfilePicture)]
    public async Task<ActionResult<byte[]>> PostEditProfilePicture([FromForm] ProfilePictureRequestModel profilePictureModel)
    {
        try
        {
            QuranHubUser user = await _userManager.GetUserAsync(User);

            IFormFile formFile = profilePictureModel.ProfilePictureFile;

            byte[] profilePicture = _responseModelsService.ReadFileIntoArray(formFile);

            return Ok(await _profileService.EditProfilePictureAsync(profilePicture,_currentUser));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }

    }

    [HttpGet(Router.Profile.CheckFollowing)]
    public async Task<ActionResult<bool>> GetCheckFollowingAsync(string userId)
    {
        try
        {
            return Ok(await _profileService.CheckFollowingAsync(_currentUser.Id, userId));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost(Router.Profile.FollowUser)]
    public async Task<ActionResult<bool>> FollowUser([FromBody] FollowRequestModel follow) 
    {
        try
        {
            Tuple<bool, FollowNotification> result = await _profileService.AddFollowAsync(follow, _currentUser);

            if(result.Item1)
            {
                var user = await _userManager.FindByIdAsync(result.Item2.TargetUserId);

                if (user.Online)
                {
                    FollowNotificationResponseModel notification = this._notificationResponseModelsFactory.BuildFollowNotificationResponseModel(result.Item2);

                    await this._notificationHubContext.Clients.Client(user.ConnectionId).SendAsync("RecieveNotification", notification);
                }

                return Ok(true);

            }
            return Ok(false);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }

    }

    [HttpPost(Router.Profile.UnfollowUser)]
    public async Task<ActionResult<bool>> UnfollowUser([FromBody] FollowRequestModel follow) 
    {
        try
        {
            return Ok(await _profileService.RemoveFollowAsync(follow));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Profile.AboutInfo)]
    public async Task<ActionResult<AboutInfoRequestModel>> GetAboutInfoAsync(string userId)
    {
        try
        {
            QuranHubUser user =  await _userManager.FindByIdAsync(userId);

            return Ok(_userResponseModelsFactory.BuildAboutInfoResponseModel(user));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }
}
