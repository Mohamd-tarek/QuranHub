
namespace QuranHub.Web.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PostController : ControllerBase
{
    private readonly Serilog.ILogger _logger;
    private IPostRepository _postRepository;
    private IPostResponseModelsFactory _postResponseModelsFactory;
    private INotificationResponseModelsFactory _notificationResponseModelsFactory;
    private IHubContext<NotificationHub> _notificationHubContext;
    private UserManager<QuranHubUser> _userManager;
    private HttpContext _httpContext;
    private QuranHubUser _currentUser;

    public  PostController(
        Serilog.ILogger logger,
        IPostRepository postRepository,
        UserManager<QuranHubUser> userManager,
        IHubContext<NotificationHub> notificationHubContext,
        IPostResponseModelsFactory postResponseModelsFactory,
        INotificationResponseModelsFactory notificationResponseModelsFactory,
        IHttpContextAccessor httpContextAccessor)
    {
       _logger = logger ?? throw new ArgumentNullException(nameof(logger));
       _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
       _httpContext = httpContextAccessor.HttpContext  ?? throw new ArgumentNullException(nameof(httpContextAccessor));;
       _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
       _notificationHubContext = notificationHubContext ?? throw new ArgumentNullException(nameof(notificationHubContext));
       _postResponseModelsFactory = postResponseModelsFactory ?? throw new ArgumentNullException(nameof(postResponseModelsFactory));
       _notificationResponseModelsFactory = notificationResponseModelsFactory ?? throw new ArgumentNullException(nameof(notificationResponseModelsFactory));

        ClaimsPrincipal claimsPrincipal = this._httpContext.User;

        if(claimsPrincipal.Identity.IsAuthenticated)
        {
            _currentUser = _userManager.GetUserAsync(this._httpContext.User).Result;
        }
    }
    
    [HttpGet(Router.Post.GetPostById)]
    public async Task<ActionResult<PostResponseModel>> GetPostByIdAsync(int PostId)
    {
        try
        {
            Post post = await this._postRepository.GetPostByIdAsync(PostId);

            PostResponseModel postResponseModel =await this._postResponseModelsFactory.BuildPostResponseModelAsync(post);
      
            return Ok(postResponseModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Post.GetPostByIdForComment)]
    public async Task<ActionResult<PostResponseModel>> GetPostByIdAsync(int PostId, int CommentId)
    {
        try
        {
            Post post = await this._postRepository.GetPostByIdWithSpecificCommentAsync(PostId, CommentId);

            PostResponseModel postResponseModel =await this._postResponseModelsFactory.BuildPostResponseModelAsync(post);
      
            return Ok(postResponseModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Post.LoadMorePostReacts)]
    public async Task<ActionResult<IEnumerable<ReactResponseModel>>> LoadMorePostReacts(int PostId, int Offset, int Size)
    {
        try
        {
            List<PostReact> postReacts = await _postRepository.GetMorePostReactsAsync(PostId, Offset, Size);

            List<ReactResponseModel> postReactsModels =  _postResponseModelsFactory.BuildPostReactsResponseModel(postReacts);

            return Ok(postReactsModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Post.LoadMoreComments)]
    public async Task<ActionResult<IEnumerable<CommentResponseModel>>> LoadMoreCommentsAsync(int PostId, int Offset, int Size) 
    {
        try
        {
            List<PostComment> comments = await _postRepository.GetMorePostCommentsAsync(PostId, Offset, Size);

            List<CommentResponseModel> commentResponseModels = await _postResponseModelsFactory.BuildCommentsResponseModelAsync(comments);

            return Ok(commentResponseModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Post.LoadMoreCommentReacts)]
    public async Task<ActionResult<IEnumerable<ReactResponseModel>>> LoadMoreCommentReactsAsync(int PostId, int Offset, int Size)
    {
        try
        {
            List<PostCommentReact> comments = await _postRepository.GetMorePostCommentReactsAsync(PostId, Offset, Size);

            List<ReactResponseModel> commentReactResponseModels =  _postResponseModelsFactory.BuildCommentReactsResponseModel(comments);

            return Ok(commentReactResponseModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Post.LoadMoreShares)]
    public async Task<ActionResult<IEnumerable<ShareResponseModel>>> LoadMoreSharesAsync(int PostId, int Offset, int Size)
    {
        try
        {
            List<PostShare> shares = await _postRepository.GetMorePostSharesAsync(PostId, Offset, Size);

            List<PostShareResponseModel> ShareResponseModels =  _postResponseModelsFactory.BuildSharesResponseModel(shares);

            return Ok(ShareResponseModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost(Router.Post.AddPostReact)]
    public async Task<ActionResult<ReactResponseModel>> AddPostReactAsync([FromBody] AddPostReactRequestModel postReact) 
    {
        try
        {
            Tuple<PostReact, PostReactNotification>  result = await _postRepository.AddPostReactAsync(postReact.PostId, _currentUser);

            var user = await _userManager.FindByIdAsync(result.Item2.TargetUserId);

            if(result.Item2.TargetUserId != null && result.Item2.TargetUserId != this._currentUser.Id && user.Online)
            {
                PostReactNotificationResponseModel notification =  this._notificationResponseModelsFactory.BuildPostReactNotificationResponseModel(result.Item2);

                await this._notificationHubContext.Clients.Client(user.ConnectionId).SendAsync("RecieveNotification", notification);
            }

            return Ok( _postResponseModelsFactory.BuildPostReactResponseModel(result.Item1));
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
    public async Task<ActionResult<CommentResponseModel>> AddCommentAsync([FromBody] AddPostCommentRequestModel comment)
    {
        try
        {
            Tuple<PostComment, PostCommentNotification> result = await _postRepository.AddPostCommentAsync(comment.PostId, comment.Text, comment.VerseId, _currentUser);

            var user = await _userManager.FindByIdAsync(result.Item2.TargetUserId);

            if (result.Item2.TargetUserId != null && result.Item2.TargetUserId != this._currentUser.Id && user.Online)
            {
                CommentNotificationResponseModel notification =  this._notificationResponseModelsFactory.BuildCommentNotificationResponseModel(result.Item2);

                await this._notificationHubContext.Clients.Client(user.ConnectionId).SendAsync("RecieveNotification", notification);
            }

            return Ok( await _postResponseModelsFactory.BuildCommentResponseModelAsync(result.Item1));
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
    public async Task<ActionResult<ReactResponseModel>> AddCommentReactAsync([FromBody] AddCommentReactRequestModel commentReact) 
    {
        try
        {
            Tuple<PostCommentReact, PostCommentReactNotification> result = await _postRepository.AddPostCommentReactAsync(commentReact.CommentId, _currentUser);

            var user = await _userManager.FindByIdAsync(result.Item2.TargetUserId);

            if (result.Item2.TargetUserId != null &&  result.Item2.TargetUserId != this._currentUser.Id && user.Online)
            {
                CommentReactNotificationResponseModel notification =  this._notificationResponseModelsFactory.BuildCommentReactNotificationResponseModel(result.Item2);

                await this._notificationHubContext.Clients.Client(user.ConnectionId).SendAsync("RecieveNotification", notification);
            }

            return  Ok(_postResponseModelsFactory.BuildCommentReactResponseModel(result.Item1));
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
    public async Task<ActionResult<ShareResponseModel>> SharePostAsync([FromBody] SharePostRequestModel sharePost)
    {
        try
        {
            Tuple<PostShare, PostShareNotification> result = await _postRepository.SharePostAsync(sharePost.Privacy, sharePost.PostId, sharePost.Text, sharePost.VerseId, _currentUser);

            var user = await _userManager.FindByIdAsync(result.Item2.TargetUserId);

            if (result.Item2.TargetUserId != null && result.Item2.TargetUserId != this._currentUser.Id && user.Online)
            {
                ShareNotificationResponseModel notification =  this._notificationResponseModelsFactory.BuildShareNotificationResponseModel(result.Item2);

                await this._notificationHubContext.Clients.Client(user.ConnectionId).SendAsync("RecieveNotification", notification);
            }

            return Ok(_postResponseModelsFactory.BuildShareResponseModel(result.Item1));
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
    public async Task<ActionResult> EditPostAsync([FromBody] EditPostRequestModel post)
    {
        try
        {
            
            await _postRepository.EditPostAsync(post.PostId, post.Privacy, post.Text, post.VerseId);

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
