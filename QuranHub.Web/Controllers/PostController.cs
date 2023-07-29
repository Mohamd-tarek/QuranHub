
namespace QuranHub.Web.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private ILogger<PostController> _logger;
    private IPostRepository _postRepository;
    private IPostViewModelsFactory _postViewModelsFactory;
    private INotificationViewModelsFactory _notificationViewModelsFactory;
    private IHubContext<NotificationHub> _notificationHubContext;
    private UserManager<QuranHubUser> _userManager;
    private HttpContext _httpContext;
    private QuranHubUser _currentUser;

    public  PostController(
        ILogger<PostController> logger,
        IPostRepository postRepository,
        UserManager<QuranHubUser> userManager,
        IHubContext<NotificationHub> notificationHubContext,
        IPostViewModelsFactory postViewModelsFactory,
        INotificationViewModelsFactory notificationViewModelsFactory,
        IHttpContextAccessor httpContextAccessor)
    {
       _logger = logger ?? throw new ArgumentNullException(nameof(logger));
       _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
       _httpContext = httpContextAccessor.HttpContext  ?? throw new ArgumentNullException(nameof(httpContextAccessor));;
       _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
       _notificationHubContext = notificationHubContext ?? throw new ArgumentNullException(nameof(notificationHubContext));
       _postViewModelsFactory = postViewModelsFactory ?? throw new ArgumentNullException(nameof(postViewModelsFactory));
       _notificationViewModelsFactory = notificationViewModelsFactory ?? throw new ArgumentNullException(nameof(notificationViewModelsFactory));

        ClaimsPrincipal claimsPrincipal = this._httpContext.User;

        if(claimsPrincipal.Identity.IsAuthenticated)
        {
            _currentUser = _userManager.GetUserAsync(this._httpContext.User).Result;
        }
    }
    
    [HttpGet("GetPostById/{PostId}")]
    public async Task<PostViewModel> GetPostByIdAsync(int PostId)
    {
        Post post = await this._postRepository.GetPostByIdAsync(PostId);

        PostViewModel postViewModel =await this._postViewModelsFactory.BuildPostViewModelAsync(post);
      
        return postViewModel;
    }

    [HttpGet("GetPostById/{PostId}/{CommentId}")]
    public async Task<PostViewModel> GetPostByIdAsync(int PostId, int CommentId)
    {
        Post post = await this._postRepository.GetPostByIdWithSpecificCommentAsync(PostId, CommentId);

        PostViewModel postViewModel =await this._postViewModelsFactory.BuildPostViewModelAsync(post);
      
        return postViewModel;
    }

    [HttpGet("LoadMorePostReacts/{PostId}/{Offset}/{Size}")]
    public async Task<IEnumerable<PostReactViewModel>> LoadMorePostReacts(int PostId, int Offset, int Size)
    {
        List<PostReact> postReacts = await _postRepository.GetMorePostReactsAsync(PostId, Offset, Size);

        List<PostReactViewModel> postReactsModels =  _postViewModelsFactory.BuildPostReactsViewModel(postReacts);

        return postReactsModels;
    }

    [HttpGet("LoadMoreComments/{PostId}/{Offset}/{Size}")]
    public async Task<IEnumerable<CommentViewModel>> LoadMoreCommentsAsync(int PostId, int Offset, int Size) 
    {
        List<PostComment> comments = await _postRepository.GetMoreCommentsAsync(PostId, Offset, Size);

        List<CommentViewModel> commentViewModels = await _postViewModelsFactory.BuildCommentsViewModelAsync(comments);

        return commentViewModels;
    }

    [HttpGet("LoadMoreCommentReacts/{PostId}/{Offset}/{Size}")]
    public async Task<IEnumerable<CommentReactViewModel>> LoadMoreCommentReactsAsync(int PostId, int Offset, int Size)
    {
        List<PostCommentReact> comments = await _postRepository.GetMoreCommentReactsAsync(PostId, Offset, Size);

        List<CommentReactViewModel> commentReactViewModels =  _postViewModelsFactory.BuildCommentReactsViewModel(comments);

        return commentReactViewModels;
    }

    [HttpGet("LoadMoreShares/{PostId}/{Offset}/{Size}")]
    public async Task<IEnumerable<ShareViewModel>> LoadMoreSharesAsync(int PostId, int Offset, int Size)
    {
        List<PostShare> shares = await _postRepository.GetMoreSharesAsync(PostId, Offset, Size);

        List<ShareViewModel> ShareViewModels =  _postViewModelsFactory.BuildSharesViewModel(shares);

        return ShareViewModels;
    }

    [HttpPost("AddPostReact")]
    public async Task<PostReactViewModel> AddPostReactAsync([FromBody] PostReact postReact) 
    {
        Tuple<PostReact, PostReactNotification>  result = await _postRepository.AddPostReactAsync(postReact, _currentUser);

        var user = await _userManager.FindByIdAsync(result.Item2.TargetUserId);

        if(result.Item2.TargetUserId != null && result.Item2.TargetUserId != this._currentUser.Id && user.Online)
        {
            PostReactNotificationViewModel notification =  this._notificationViewModelsFactory.BuildPostReactNotificationViewModel(result.Item2);

            await this._notificationHubContext.Clients.Client(user.ConnectionId).SendAsync("RecieveNotification", notification);
        }

        return _postViewModelsFactory.BuildPostReactViewModel(result.Item1);
    }

    [HttpDelete("RemovePostReact")]
    public async Task<ActionResult> RemovePostReactAsync(int postId)
    {
        try
        {
            if( await _postRepository.RemovePostReactAsync(postId, _currentUser))
            {
                return Ok();
            }
        }
        catch(Exception ex)
        {
            throw new InvalidOperationException(nameof(RemovePostReactAsync));
        }

        return BadRequest();
    }

    [HttpPost("AddComment")]
    public async Task<CommentViewModel> AddCommentAsync([FromBody] PostComment comment)
    {
        Tuple<PostComment, PostCommentNotification> result = await _postRepository.AddCommentAsync(comment, _currentUser);

        var user = await _userManager.FindByIdAsync(result.Item2.TargetUserId);

        if (result.Item2.TargetUserId != null && result.Item2.TargetUserId != this._currentUser.Id && user.Online)
        {
            CommentNotificationViewModel notification =  this._notificationViewModelsFactory.BuildCommentNotificationViewModel(result.Item2);

            await this._notificationHubContext.Clients.Client(user.ConnectionId).SendAsync("RecieveNotification", notification);
        }

        return await _postViewModelsFactory.BuildCommentViewModelAsync(result.Item1);
    }

    [HttpDelete("RemoveComment")]
    public async Task<Boolean> RemoveCommentAsync(int commentId)
    {
        return await _postRepository.RemoveCommentAsync(commentId);
    }

    [HttpPost("AddCommentReact")]
    public async Task<CommentReactViewModel> AddCommentReactAsync([FromBody] PostCommentReact commentReact) 
    {
        Tuple<PostCommentReact, PostCommentReactNotification> result = await _postRepository.AddCommentReactAsync(commentReact, _currentUser);

        var user = await _userManager.FindByIdAsync(result.Item2.TargetUserId);

        if (result.Item2.TargetUserId != null &&  result.Item2.TargetUserId != this._currentUser.Id && user.Online)
        {
            CommentReactNotificationViewModel notification =  this._notificationViewModelsFactory.BuildCommentReactNotificationViewModel(result.Item2);

            await this._notificationHubContext.Clients.Client(user.ConnectionId).SendAsync("RecieveNotification", notification);
        }

        return  _postViewModelsFactory.BuildCommentReactViewModel(result.Item1);
    }

    [HttpDelete("RemoveCommentReact")]
    public async Task<ActionResult> RemoveCommentReactAsync(int commentId) 
    {
        try
        {
            if (await _postRepository.RemoveCommentReactAsync(commentId, _currentUser))
            {
                return Ok();
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.StackTrace);
        }

        return BadRequest();
    }

    [HttpPost("SharePost")]
    public async Task<ShareViewModel> SharePostAsync([FromBody] SharedPost sharedPost)
    {
        Tuple<PostShare, PostShareNotification> result = await _postRepository.SharePostAsync(sharedPost, _currentUser);

        var user = await _userManager.FindByIdAsync(result.Item2.TargetUserId);

        if (result.Item2.TargetUserId != null && result.Item2.TargetUserId != this._currentUser.Id && user.Online)
        {
            ShareNotificationViewModel notification =  this._notificationViewModelsFactory.BuildShareNotificationViewModel(result.Item2);

            await this._notificationHubContext.Clients.Client(user.ConnectionId).SendAsync("RecieveNotification", notification);
        }

        return _postViewModelsFactory.BuildShareViewModel(result.Item1);
    }

    [HttpGet("Verses")]
    public async Task<IEnumerable<Verse>> GetVersesAsync()
    {
        return await _postRepository.GetVersesAsync();
    }

    [HttpPut("EditPost")]
    public async Task<ActionResult> EditPostAsync([FromBody] Post post)
    {
        try
        {
            
            await _postRepository.EditPostAsync(post);

            return Ok();
        }
        catch(Exception ex)
        {
            return BadRequest();
        }        
    }

    [HttpDelete("DeletePost")]
    public async Task<ActionResult> DeletePostAsync(int postId)
    {
       if( await _postRepository.DeletePostAsync(postId))
        {
            return Ok();
        }

        return BadRequest();
    }
}
