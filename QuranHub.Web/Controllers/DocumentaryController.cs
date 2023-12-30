
namespace QuranHub.Web.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class DocumentaryController : ControllerBase
{
    private readonly Serilog.ILogger _logger;
    private IDocumentaryRepository _documentaryRepository;
    private IVideoInfoViewModelsFactory _videoInfoViewModelsFactory;
    private INotificationViewModelsFactory _notificationViewModelsFactory;
    private IHubContext<NotificationHub> _notificationHubContext;
    private UserManager<QuranHubUser> _userManager;
    private HttpContext _httpContext;
    private QuranHubUser _currentUser;


    public DocumentaryController(
        Serilog.ILogger logger,
        IDocumentaryRepository documentaryRepository,
        UserManager<QuranHubUser> userManager,
        IHubContext<NotificationHub> notificationHubContext,
        INotificationViewModelsFactory notificationViewModelsFactory,
        IVideoInfoViewModelsFactory videoInfoViewModelsFactory,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _documentaryRepository = documentaryRepository ?? throw new ArgumentNullException(nameof(documentaryRepository));
        _httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor)); ;
        _videoInfoViewModelsFactory = videoInfoViewModelsFactory ?? throw new ArgumentNullException(nameof(videoInfoViewModelsFactory));
        _notificationViewModelsFactory = notificationViewModelsFactory ?? throw new ArgumentNullException(nameof(notificationViewModelsFactory));
        _notificationHubContext = notificationHubContext ?? throw new ArgumentNullException(nameof(notificationHubContext));

        ClaimsPrincipal claimsPrincipal = this._httpContext.User;

        if (claimsPrincipal.Identity.IsAuthenticated)
        {
            _currentUser = _userManager.GetUserAsync(this._httpContext.User).Result;
        }
    }

    [HttpGet("PlayListsInfo")]
    public async Task<ActionResult<IEnumerable<PlayListInfo>>> GetPlayListsInfoAsync() 
    {
        try
        {
            return   Ok(await this._documentaryRepository.GetPlayListsAsync());
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet("PlayListInfo/{PlaylistName}")]
    public async Task<ActionResult<PlayListInfo>> GetPlayListsInfoAsync(string PlaylistName)
    {
        try
        {
            return  Ok(await this._documentaryRepository.GetPlayListByNameAsync(PlaylistName));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet("VideoInfoForPlayList/{playListName}/{offset}/{amount}")]
    public async Task<ActionResult<IEnumerable<VideoInfo>>> GetVideoInfoForPlayList(string playListName, int offset = 0, int amount = 20) 
    {
        try
        {
            return Ok( await  this._documentaryRepository.GetVideoInfoForPlayListAsync(playListName, offset, amount));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet("VideoInfo/{name}")]
    public async Task<ActionResult<VideoInfoViewModel>> GetVideoInfoAsync(string name) 
    {
        try
        {
            VideoInfo videoInfo =  await  this._documentaryRepository.GetVideoInfoByNameAsync(name);

            return Ok( await this._videoInfoViewModelsFactory.BuildVideoInfoViewModelAsync(videoInfo));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet("VideoInfo/{name}/{CommentId}")]
    public async Task<ActionResult<VideoInfoViewModel>> GetVideoInfoAsync(string name, int CommentId)
    {
        try
        {
            VideoInfo videoInfo = await this._documentaryRepository.GetVideoInfoByNameAsync(name);

            return Ok( await this._videoInfoViewModelsFactory.BuildVideoInfoViewModelAsync(videoInfo));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet("LoadMoreReacts/{VideoInfoId}/{Offset}/{Size}")]
    public async Task<ActionResult<IEnumerable<ReactViewModel>>> LoadMoreVideoInfoReacts(int VideoInfoId, int Offset, int Size)
    {
        try
        {
            List<VideoInfoReact> videoInfoReacts = await _documentaryRepository.GetMoreVideoInfoReactsAsync(VideoInfoId, Offset, Size);

            List<ReactViewModel> videoInfoReactsModels = _videoInfoViewModelsFactory.BuildVideoInfoReactsViewModel(videoInfoReacts);

            return Ok(videoInfoReactsModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet("LoadMoreComments/{VideoInfoId}/{Offset}/{Size}")]
    public async Task<ActionResult<IEnumerable<CommentViewModel>>> LoadMoreCommentsAsync(int VideoInfoId, int Offset, int Size)
    {
        try
        {
            List<VideoInfoComment> comments = await _documentaryRepository.GetMoreVideoInfoCommentsAsync(VideoInfoId, Offset, Size);

            List<CommentViewModel> commentViewModels = await _videoInfoViewModelsFactory.BuildCommentsViewModelAsync(comments);

            return Ok(commentViewModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet("LoadMoreCommentReacts/{VideoInfoId}/{Offset}/{Size}")]
    public async Task<ActionResult<IEnumerable<ReactViewModel>>> LoadMoreCommentReactsAsync(int VideoInfoId, int Offset, int Size)
    {
        try
        {
            List<VideoInfoCommentReact> comments = await _documentaryRepository.GetMoreVideoInfoCommentReactsAsync(VideoInfoId, Offset, Size);

            List<ReactViewModel> commentReactViewModels = _videoInfoViewModelsFactory.BuildCommentReactsViewModel(comments);

            return Ok(commentReactViewModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

  

    [HttpPost("AddReact")]
    public async Task<ActionResult<ReactViewModel>> AddVideoInfoReactAsync([FromBody] VideoInfoReact videoInfoReact)
    {
        try
        {
            VideoInfoReact VideoInfoReact = await _documentaryRepository.AddVideoInfoReactAsync(videoInfoReact, _currentUser);

            return Ok(_videoInfoViewModelsFactory.BuildVideoInfoReactViewModel(VideoInfoReact));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpDelete("RemoveReact")]
    public async Task<ActionResult> RemoveVideoInfoReactReactAsync(int VideoInfoReactId)
    {
        try
        {
            if (await _documentaryRepository.RemoveVideoInfoReactAsync(VideoInfoReactId, _currentUser))
            {
                return Ok();
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(nameof(RemoveVideoInfoReactReactAsync));
        }

        return BadRequest();
    }

    [HttpPost("AddComment")]
    public async Task<ActionResult<CommentViewModel>> AddCommentAsync([FromBody] VideoInfoComment comment)
    {
        try
        {
            VideoInfoComment VideoInfoComment = await _documentaryRepository.AddVideoInfoCommentAsync(comment, _currentUser);

            return Ok( await _videoInfoViewModelsFactory.BuildCommentViewModelAsync(VideoInfoComment));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpDelete("RemoveComment")]
    public async Task<ActionResult<bool>> RemoveCommentAsync(int commentId)
    {
        try
        {
            return Ok( await _documentaryRepository.RemoveVideoInfoCommentAsync(commentId));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost("AddCommentReact")]
    public async Task<ActionResult<ReactViewModel>> AddCommentReactAsync([FromBody] VideoInfoCommentReact commentReact)
    {
        try
        {
            Tuple<VideoInfoCommentReact, VideoInfoCommentReactNotification> result = await _documentaryRepository.AddVideoInfoCommentReactAsync(commentReact, _currentUser);

            var user = await _userManager.FindByIdAsync(result.Item2.TargetUserId);

            if (result.Item2.TargetUserId != null && result.Item2.TargetUserId != this._currentUser.Id && user.Online)
            {
                CommentReactNotificationViewModel notification = this._notificationViewModelsFactory.BuildCommentReactNotificationViewModel(result.Item2);

                await this._notificationHubContext.Clients.Client(user.ConnectionId).SendAsync("RecieveNotification", notification);
            }

            return Ok( _videoInfoViewModelsFactory.BuildCommentReactViewModel(result.Item1));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpDelete("RemoveCommentReact")]
    public async Task<ActionResult> RemoveCommentReactAsync(int commentId)
    {
        try
        {
            if (await _documentaryRepository.RemoveVideoInfoCommentReactAsync(commentId, _currentUser))
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

    [HttpGet("Verses")]
    public async Task<ActionResult<IEnumerable<Verse>>> GetVersesAsync()
    {
        try
        {
            return Ok( await _documentaryRepository.GetVersesAsync());
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

   

  
}
