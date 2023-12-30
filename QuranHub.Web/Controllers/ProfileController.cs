
namespace QuranHub.Web.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly Serilog.ILogger _logger;
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
        Serilog.ILogger logger,
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
    public async Task<ActionResult<IEnumerable<object>>> GetUserPostsAsync(string userId) 
    {
        try
        {
            List<ShareablePost> posts = await _profileService.GetUserShareablePostsAsync(userId);

            List<SharedPost> sharedPosts = await _profileService.GetUserSharedPostsAsync(userId);

            List<object> mergedPosts = await _viewModelsService.MergePostsAsync(posts, sharedPosts);

            return Ok(mergedPosts);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet("UserFollowers/{UserId}")]
    public async Task<ActionResult<IEnumerable<UserBasicInfoViewModel>>> GetUserFollowersAsync(string userId) 
    {
        try
        {
            List <QuranHubUser> users = await _profileService.GetUserFollowersAsync(userId);

            List<UserBasicInfoViewModel> usersViewModels = _userViewModelsFactory.BuildUsersBasicInfoViewModel(users);

            return Ok(usersViewModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet("UserFollowings/{UserId}")]
    public async Task<ActionResult<IEnumerable<UserBasicInfoViewModel>>> GetUserFollowingsAsync(string userId)
    {
        try
        {
            List<QuranHubUser> users = await _profileService.GetUserFollowingsAsync(userId);

            List<UserBasicInfoViewModel> usersViewModels = _userViewModelsFactory.BuildUsersBasicInfoViewModel(users);

            return Ok(usersViewModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet("UserFollowers/{UserId}/{KeyWord}")]
    public async Task<ActionResult<IEnumerable<UserBasicInfoViewModel>>> GetUserFollowersAsync(string userId, string KeyWord) 
    {
        try
        {
            List<QuranHubUser> users = await _profileService.GetUserFollowersAsync(userId, KeyWord);

            List<UserBasicInfoViewModel> usersViewModels = _userViewModelsFactory.BuildUsersBasicInfoViewModel(users);

            return Ok(usersViewModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet("UserFollowings/{UserId}/{KeyWord}")]
    public async Task<ActionResult<IEnumerable<UserBasicInfoViewModel>>> GetUserFollowingsAsync(string userId, string KeyWord)
    {
        try
        {
            List<QuranHubUser> users = await _profileService.GetUserFollowingsAsync(userId, KeyWord);

            List<UserBasicInfoViewModel> usersViewModels = _userViewModelsFactory.BuildUsersBasicInfoViewModel(users);

            return Ok(usersViewModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }      

    [HttpGet("UserProfile/{UserId}")]
    public async Task<ActionResult<ProfileViewModel>> GetUserProfileAsync(string userId)
    {
        try
        {
            QuranHubUser user = await _userManager.FindByIdAsync(userId);

            ProfileViewModel profileModel = _userViewModelsFactory.BuildProfileViewModel(user);

            return Ok(profileModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost("editCoverPicture")]
    public async Task<ActionResult<byte[]>> PostEditCoverPicture([FromForm] CoverPictureModel coverPictureModel) 
    {
        try
        {
            IFormFile formFile = coverPictureModel.CoverPictureFile;

            byte[] coverPicture = _viewModelsService.ReadFileIntoArray(formFile);

            return Ok(await _profileService.EditCoverPictureAsync(coverPicture, _currentUser));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost("editProfilePicture")]
    public async Task<ActionResult<byte[]>> PostEditProfilePicture([FromForm] ProfilePictureModel profilePictureModel)
    {
        try
        {
            QuranHubUser user = await _userManager.GetUserAsync(User);

            IFormFile formFile = profilePictureModel.ProfilePictureFile;

            byte[] profilePicture = _viewModelsService.ReadFileIntoArray(formFile);

            return Ok(await _profileService.EditProfilePictureAsync(profilePicture,_currentUser));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }

    }

    [HttpGet("CheckFollowing/{UserId}")]
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

    [HttpPost("FollowUser")]
    public async Task<ActionResult<bool>> FollowUser([FromBody] Follow follow) 
    {
        try
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

    [HttpPost("UnfollowUser")]
    public async Task<ActionResult<bool>> UnfollowUser([FromBody] Follow follow) 
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

    [HttpGet("AboutInfo/{UserId}")]
    public async Task<ActionResult<AboutInfoViewModel>> GetAboutInfoAsync(string userId)
    {
        try
        {
            QuranHubUser user =  await _userManager.FindByIdAsync(userId);

            return Ok(_userViewModelsFactory.BuildAboutInfoViewModel(user));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }
}
