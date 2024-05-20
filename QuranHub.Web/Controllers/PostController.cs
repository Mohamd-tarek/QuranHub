
namespace QuranHub.Web.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PostController : ControllerBase
{
    private readonly Serilog.ILogger _logger;
    private IPostRepository _postRepository;
    private IPostViewModelsFactory _postViewModelsFactory;
    private INotificationViewModelsFactory _notificationViewModelsFactory;
    private IHubContext<NotificationHub> _notificationHubContext;
    private UserManager<QuranHubUser> _userManager;
    private HttpContext _httpContext;
    private QuranHubUser _currentUser;

    public  PostController(
        Serilog.ILogger logger,
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
    
    [HttpGet(Router.Post.GetPostById)]
    public async Task<ActionResult<PostViewModel>> GetPostByIdAsync(int PostId)
    {
        try
        {
            Post post = await this._postRepository.GetPostByIdAsync(PostId);

            PostViewModel postViewModel =await this._postViewModelsFactory.BuildPostViewModelAsync(post);
      
            return Ok(postViewModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Post.GetPostByIdForComment)]
    public async Task<ActionResult<PostViewModel>> GetPostByIdAsync(int PostId, int CommentId)
    {
        try
        {
            Post post = await this._postRepository.GetPostByIdWithSpecificCommentAsync(PostId, CommentId);

            PostViewModel postViewModel =await this._postViewModelsFactory.BuildPostViewModelAsync(post);
      
            return Ok(postViewModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Post.LoadMorePostReacts)]
    public async Task<ActionResult<IEnumerable<ReactViewModel>>> LoadMorePostReacts(int PostId, int Offset, int Size)
    {
        try
        {
            List<PostReact> postReacts = await _postRepository.GetMorePostReactsAsync(PostId, Offset, Size);

            List<ReactViewModel> postReactsModels =  _postViewModelsFactory.BuildPostReactsViewModel(postReacts);

            return Ok(postReactsModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Post.LoadMoreComments)]
    public async Task<ActionResult<IEnumerable<CommentViewModel>>> LoadMoreCommentsAsync(int PostId, int Offset, int Size) 
    {
        try
        {
            List<PostComment> comments = await _postRepository.GetMorePostCommentsAsync(PostId, Offset, Size);

            List<CommentViewModel> commentViewModels = await _postViewModelsFactory.BuildCommentsViewModelAsync(comments);

            return Ok(commentViewModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Post.LoadMoreCommentReacts)]
    public async Task<ActionResult<IEnumerable<ReactViewModel>>> LoadMoreCommentReactsAsync(int PostId, int Offset, int Size)
    {
        try
        {
            List<PostCommentReact> comments = await _postRepository.GetMorePostCommentReactsAsync(PostId, Offset, Size);

            List<ReactViewModel> commentReactViewModels =  _postViewModelsFactory.BuildCommentReactsViewModel(comments);

            return Ok(commentReactViewModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Post.LoadMoreShares)]
    public async Task<ActionResult<IEnumerable<ShareViewModel>>> LoadMoreSharesAsync(int PostId, int Offset, int Size)
    {
        try
        {
            List<PostShare> shares = await _postRepository.GetMorePostSharesAsync(PostId, Offset, Size);

            List<PostShareViewModel> ShareViewModels =  _postViewModelsFactory.BuildSharesViewModel(shares);

            return Ok(ShareViewModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost(Router.Post.AddPostReact)]
    public async Task<ActionResult<ReactViewModel>> AddPostReactAsync([FromBody] PostReact postReact) 
    {
        try
        {
            Tuple<PostReact, PostReactNotification>  result = await _postRepository.AddPostReactAsync(postReact, _currentUser);

            var user = await _userManager.FindByIdAsync(result.Item2.TargetUserId);

            if(result.Item2.TargetUserId != null && result.Item2.TargetUserId != this._currentUser.Id && user.Online)
            {
                PostReactNotificationViewModel notification =  this._notificationViewModelsFactory.BuildPostReactNotificationViewModel(result.Item2);

                await this._notificationHubContext.Clients.Client(user.ConnectionId).SendAsync("RecieveNotification", notification);
            }

            return Ok( _postViewModelsFactory.BuildPostReactViewModel(result.Item1));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpDelete(Router.Post.RemovePostReact)]
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

    [HttpPost(Router.Post.AddComment)]
    public async Task<ActionResult<CommentViewModel>> AddCommentAsync([FromBody] PostComment comment)
    {
        try
        {
            Tuple<PostComment, PostCommentNotification> result = await _postRepository.AddPostCommentAsync(comment, _currentUser);

            var user = await _userManager.FindByIdAsync(result.Item2.TargetUserId);

            if (result.Item2.TargetUserId != null && result.Item2.TargetUserId != this._currentUser.Id && user.Online)
            {
                CommentNotificationViewModel notification =  this._notificationViewModelsFactory.BuildCommentNotificationViewModel(result.Item2);

                await this._notificationHubContext.Clients.Client(user.ConnectionId).SendAsync("RecieveNotification", notification);
            }

            return Ok( await _postViewModelsFactory.BuildCommentViewModelAsync(result.Item1));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpDelete(Router.Post.RemoveComment)]
    public async Task<ActionResult<bool>> RemoveCommentAsync(int commentId)
    {
        try
        {
            return  Ok(await _postRepository.RemovePostCommentAsync(commentId));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost(Router.Post.AddCommentReact)]
    public async Task<ActionResult<ReactViewModel>> AddCommentReactAsync([FromBody] PostCommentReact commentReact) 
    {
        try
        {
            Tuple<PostCommentReact, PostCommentReactNotification> result = await _postRepository.AddPostCommentReactAsync(commentReact, _currentUser);

            var user = await _userManager.FindByIdAsync(result.Item2.TargetUserId);

            if (result.Item2.TargetUserId != null &&  result.Item2.TargetUserId != this._currentUser.Id && user.Online)
            {
                CommentReactNotificationViewModel notification =  this._notificationViewModelsFactory.BuildCommentReactNotificationViewModel(result.Item2);

                await this._notificationHubContext.Clients.Client(user.ConnectionId).SendAsync("RecieveNotification", notification);
            }

            return  Ok(_postViewModelsFactory.BuildCommentReactViewModel(result.Item1));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpDelete(Router.Post.RemoveCommentReact)]
    public async Task<ActionResult> RemoveCommentReactAsync(int commentId) 
    {
        try
        {
            if (await _postRepository.RemovePostCommentReactAsync(commentId, _currentUser))
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

    [HttpPost(Router.Post.SharePost)]
    public async Task<ActionResult<ShareViewModel>> SharePostAsync([FromBody] SharedPost sharedPost)
    {
        try
        {
            Tuple<PostShare, PostShareNotification> result = await _postRepository.SharePostAsync(sharedPost, _currentUser);

            var user = await _userManager.FindByIdAsync(result.Item2.TargetUserId);

            if (result.Item2.TargetUserId != null && result.Item2.TargetUserId != this._currentUser.Id && user.Online)
            {
                ShareNotificationViewModel notification =  this._notificationViewModelsFactory.BuildShareNotificationViewModel(result.Item2);

                await this._notificationHubContext.Clients.Client(user.ConnectionId).SendAsync("RecieveNotification", notification);
            }

            return Ok(_postViewModelsFactory.BuildShareViewModel(result.Item1));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Post.Verses)]
    public async Task<ActionResult<IEnumerable<Verse>>> GetVersesAsync()
    {
        try
        {
            return Ok(await _postRepository.GetVersesAsync());
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpPut(Router.Post.EditPost)]
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

    [HttpDelete(Router.Post.DeletePost)]
    public async Task<ActionResult> DeletePostAsync(int postId)
    {
        try
        {
            if ( await _postRepository.DeletePostAsync(postId))
            {
                return Ok();
            }

            return BadRequest();
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }
}
