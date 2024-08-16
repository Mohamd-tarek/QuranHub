
namespace QuranHub.Web.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class HomeController : ControllerBase
{
    private readonly Serilog.ILogger _logger;
    private IHomeService _homeService;
    private IUserResponseModelsFactory _userResponseModelsFactory;
    private IPostResponseModelsFactory _postResponseModelsFactory;
    private IResponseModelsService _responseModelsService;
    private UserManager<QuranHubUser> _userManager;
    private HttpContext _httpContext;
    private QuranHubUser _currentUser;

    public  HomeController(
        Serilog.ILogger logger,
        IHomeService homeService,
        IUserResponseModelsFactory userResponseModelsFactor,
        IPostResponseModelsFactory postResponseModelsFactory,
        IResponseModelsService responseModelsService,
        UserManager<QuranHubUser> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
       _homeService = homeService ?? throw new ArgumentNullException(nameof(homeService));
       _userResponseModelsFactory = userResponseModelsFactor ?? throw new ArgumentNullException(nameof(userResponseModelsFactor));
       _postResponseModelsFactory = postResponseModelsFactory ?? throw new ArgumentNullException(nameof(postResponseModelsFactory));
       _responseModelsService = responseModelsService ?? throw new ArgumentNullException(nameof(responseModelsService));
       _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
       _httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor));

        ClaimsPrincipal claimsPrincipal = this._httpContext.User;

        if (claimsPrincipal.Identity.IsAuthenticated)
        {
            _currentUser = _userManager.GetUserAsync(this._httpContext.User).Result;
        }


    }

    [HttpGet(Router.Home.NewFeeds)]
    public async Task<ActionResult<IEnumerable<object>>> GetNewFeedsAsync()
    {
        try
        {
            List<object> postResponseModels = new List<object>();

           if (User.Identity.IsAuthenticated)
           {

                List<ShareablePost> posts = await _homeService.GetShareablePostsAsync(_currentUser.Id);

                List<SharedPost> sharedPosts = await _homeService.GetSharedPostsAsync(_currentUser.Id);

                postResponseModels = await _responseModelsService.MergePostsAsync(posts, sharedPosts);
           }

            return Ok(postResponseModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost(Router.Home.AddPost)]
    public async Task<ActionResult<ShareablePostResponseModel>> AddPost([FromBody] AddPostRequestModel addPost)
    {
        try
        {
            ShareablePost insertedPost = await _homeService.CreatePostAsync(addPost);

            return Ok(await _postResponseModelsFactory.BuildShareablePostResponseModelAsync(insertedPost));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Home.FindUsersByName)]
    public async Task<ActionResult<IEnumerable<UserResponseModel>>> FindUsersByNameAsync(string name) 
    {
        try
        {
            List<QuranHubUser> users = await _homeService.FindUsersByNameAsync(name);

            List<UserResponseModel> userModels = _userResponseModelsFactory.BuildUsersResponseModel(users);

            return Ok(userModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Home.SearchPosts)]
    public async Task<ActionResult<IEnumerable<object>>> SearchPostsAsync(string keyword)
    {
        try
        {
            List<object> postResponseModels = new List<object>();

            List<ShareablePost> posts = await _homeService.SearchShareablePostsAsync(keyword);

            List<SharedPost> sharedPosts = await _homeService.SearchSharedPostsAsync(keyword);

            postResponseModels = await _responseModelsService.MergePostsAsync(posts, sharedPosts);

            return Ok(postResponseModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

}
