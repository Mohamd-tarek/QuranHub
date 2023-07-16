
namespace QuranHub.Web.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    private ILogger<HomeController> _logger;
    private IHomeService _homeService;
    private IUserViewModelsFactory _userViewModelsFactory;
    private IPostViewModelsFactory _postViewModelsFactory;
    private IViewModelsService _viewModelsService;
    private UserManager<QuranHubUser> _userManager;
    private HttpContext _httpContext;
    private QuranHubUser _currentUser;

    public  HomeController(
        ILogger<HomeController> logger,
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

    [HttpGet("NewFeeds")]
    public async Task<IEnumerable<object>> GetNewFeedsAsync()
    {
       List<object> postViewModels = new List<object>();

       if (User.Identity.IsAuthenticated)
       {

            List<ShareablePost> posts = await _homeService.GetShareablePostsAsync(_currentUser.Id);

            List<SharedPost> sharedPosts = await _homeService.GetSharedPostsAsync(_currentUser.Id);

            postViewModels = await _viewModelsService.MergePostsAsync(posts, sharedPosts);
       }

        return postViewModels;
    }

    [HttpPost("AddPost")]
    public async Task<ShareablePostViewModel> AddPost([FromBody] ShareablePost post)
    {
        ShareablePost insertedPost = await _homeService.CreatePostAsync(post);

        return await _postViewModelsFactory.BuildShareablePostViewModelAsync(insertedPost);
    }

    [HttpGet("FindUsersByName/{name}")]
    public async Task<IEnumerable<UserViewModel>> FindUsersByNameAsync(string name) 
    {
        List<QuranHubUser> users = await _homeService.FindUsersByNameAsync(name);

        List<UserViewModel> userModels = _userViewModelsFactory.BuildUsersViewModel(users);

        return userModels;
    }

    [HttpGet("SearchPosts/{keyword}")]
    public async Task<IEnumerable<object>> SearchPostsAsync(string keyword)
    {
        List<object> postViewModels = new List<object>();

        List<ShareablePost> posts = await _homeService.SearchShareablePostsAsync(keyword);

        List<SharedPost> sharedPosts = await _homeService.SearchSharedPostsAsync(keyword);

        postViewModels = await _viewModelsService.MergePostsAsync(posts, sharedPosts);

        return postViewModels;
    }

}
