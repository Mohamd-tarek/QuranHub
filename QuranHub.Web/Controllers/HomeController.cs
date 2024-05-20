
namespace QuranHub.Web.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class HomeController : ControllerBase
{
    private readonly Serilog.ILogger _logger;
    private IHomeService _homeService;
    private IUserViewModelsFactory _userViewModelsFactory;
    private IPostViewModelsFactory _postViewModelsFactory;
    private IViewModelsService _viewModelsService;
    private UserManager<QuranHubUser> _userManager;
    private HttpContext _httpContext;
    private QuranHubUser _currentUser;

    public  HomeController(
        Serilog.ILogger logger,
        IHomeService homeService,
        IUserViewModelsFactory userViewModelsFactor,
        IPostViewModelsFactory postViewModelsFactory,
        IViewModelsService viewModelsService,
        UserManager<QuranHubUser> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
       _homeService = homeService ?? throw new ArgumentNullException(nameof(homeService));
       _userViewModelsFactory = userViewModelsFactor ?? throw new ArgumentNullException(nameof(userViewModelsFactor));
       _postViewModelsFactory = postViewModelsFactory ?? throw new ArgumentNullException(nameof(postViewModelsFactory));
       _viewModelsService = viewModelsService ?? throw new ArgumentNullException(nameof(viewModelsService));
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
            List<object> postViewModels = new List<object>();

           if (User.Identity.IsAuthenticated)
           {

                List<ShareablePost> posts = await _homeService.GetShareablePostsAsync(_currentUser.Id);

                List<SharedPost> sharedPosts = await _homeService.GetSharedPostsAsync(_currentUser.Id);

                postViewModels = await _viewModelsService.MergePostsAsync(posts, sharedPosts);
           }

            return Ok(postViewModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost(Router.Home.AddPost)]
    public async Task<ActionResult<ShareablePostViewModel>> AddPost([FromBody] ShareablePost post)
    {
        try
        {
            ShareablePost insertedPost = await _homeService.CreatePostAsync(post);

            return Ok(await _postViewModelsFactory.BuildShareablePostViewModelAsync(insertedPost));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Home.FindUsersByName)]
    public async Task<ActionResult<IEnumerable<UserViewModel>>> FindUsersByNameAsync(string name) 
    {
        try
        {
            List<QuranHubUser> users = await _homeService.FindUsersByNameAsync(name);

            List<UserViewModel> userModels = _userViewModelsFactory.BuildUsersViewModel(users);

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
            List<object> postViewModels = new List<object>();

            List<ShareablePost> posts = await _homeService.SearchShareablePostsAsync(keyword);

            List<SharedPost> sharedPosts = await _homeService.SearchSharedPostsAsync(keyword);

            postViewModels = await _viewModelsService.MergePostsAsync(posts, sharedPosts);

            return Ok(postViewModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

}
