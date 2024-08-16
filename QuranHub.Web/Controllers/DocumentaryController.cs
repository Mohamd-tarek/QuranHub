
namespace QuranHub.Web.Controllers;



[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class DocumentaryController : ControllerBase
{
    private readonly Serilog.ILogger _logger;
    private IDocumentaryRepository _documentaryRepository;
    private IVideoInfoResponseModelsFactory _videoInfoResponseModelsFactory;
    private INotificationResponseModelsFactory _notificationResponseModelsFactory;
    private IHubContext<NotificationHub> _notificationHubContext;
    private UserManager<QuranHubUser> _userManager;
    private HttpContext _httpContext;
    private QuranHubUser _currentUser;


    public DocumentaryController(
        Serilog.ILogger logger,
        IDocumentaryRepository documentaryRepository,
        UserManager<QuranHubUser> userManager,
        IHubContext<NotificationHub> notificationHubContext,
        INotificationResponseModelsFactory notificationResponseModelsFactory,
        IVideoInfoResponseModelsFactory videoInfoResponseModelsFactory,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _documentaryRepository = documentaryRepository ?? throw new ArgumentNullException(nameof(documentaryRepository));
        _httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor)); ;
        _videoInfoResponseModelsFactory = videoInfoResponseModelsFactory ?? throw new ArgumentNullException(nameof(videoInfoResponseModelsFactory));
        _notificationResponseModelsFactory = notificationResponseModelsFactory ?? throw new ArgumentNullException(nameof(notificationResponseModelsFactory));
        _notificationHubContext = notificationHubContext ?? throw new ArgumentNullException(nameof(notificationHubContext));

        ClaimsPrincipal claimsPrincipal = this._httpContext.User;

        if (claimsPrincipal.Identity.IsAuthenticated)
        {
            _currentUser = _userManager.GetUserAsync(this._httpContext.User).Result;
        }
    }

    [HttpGet(Router.Documentary.PlayListsInfo)]
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

    [HttpGet(Router.Documentary.PlayListInfo)]
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

    [HttpGet(Router.Documentary.VideoInfoForPlayList)]
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

    [HttpGet(Router.Documentary.VideoInfo)]
    public async Task<ActionResult<VideoInfoResponseModel>> GetVideoInfoAsync(string name) 
    {
        try
        {
            VideoInfo videoInfo =  await  this._documentaryRepository.GetVideoInfoByNameAsync(name);

            return Ok( await this._videoInfoResponseModelsFactory.BuildVideoInfoResponseModelAsync(videoInfo));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }


    [HttpGet(Router.Documentary.LoadMoreReacts)]
    public async Task<ActionResult<IEnumerable<ReactResponseModel>>> LoadMoreVideoInfoReacts(int VideoInfoId, int Offset, int Size)
    {
        try
        {
            List<VideoInfoReact> videoInfoReacts = await _documentaryRepository.GetMoreVideoInfoReactsAsync(VideoInfoId, Offset, Size);

            List<ReactResponseModel> videoInfoReactsModels = _videoInfoResponseModelsFactory.BuildVideoInfoReactsResponseModel(videoInfoReacts);

            return Ok(videoInfoReactsModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Documentary.LoadMoreComments)]
    public async Task<ActionResult<IEnumerable<CommentResponseModel>>> LoadMoreCommentsAsync(int VideoInfoId, int Offset, int Size)
    {
        try
        {
            List<VideoInfoComment> comments = await _documentaryRepository.GetMoreVideoInfoCommentsAsync(VideoInfoId, Offset, Size);

            List<CommentResponseModel> commentResponseModels = await _videoInfoResponseModelsFactory.BuildCommentsResponseModelAsync(comments);

            return Ok(commentResponseModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Documentary.LoadMoreCommentReacts)]
    public async Task<ActionResult<IEnumerable<ReactResponseModel>>> LoadMoreCommentReactsAsync(int VideoInfoId, int Offset, int Size)
    {
        try
        {
            List<VideoInfoCommentReact> comments = await _documentaryRepository.GetMoreVideoInfoCommentReactsAsync(VideoInfoId, Offset, Size);

            List<ReactResponseModel> commentReactResponseModels = _videoInfoResponseModelsFactory.BuildCommentReactsResponseModel(comments);

            return Ok(commentReactResponseModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

  

    [HttpPost(Router.Documentary.AddReact)]
    public async Task<ActionResult<ReactResponseModel>> AddVideoInfoReactAsync([FromBody] VideoInfoReact videoInfoReact)
    {
        try
        {
            VideoInfoReact VideoInfoReact = await _documentaryRepository.AddVideoInfoReactAsync(videoInfoReact, _currentUser);

            return Ok(_videoInfoResponseModelsFactory.BuildVideoInfoReactResponseModel(VideoInfoReact));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpDelete(Router.Documentary.RemoveReact)]
    public async Task<ActionResult> RemoveVideoInfoReactAsync(int VideoInfoReactId)
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
            throw new InvalidOperationException(nameof(RemoveVideoInfoReactAsync));
        }

        return BadRequest();
    }

    [HttpPost(Router.Documentary.AddComment)]
    public async Task<ActionResult<CommentResponseModel>> AddCommentAsync([FromBody] VideoInfoComment comment)
    {
        try
        {
            VideoInfoComment VideoInfoComment = await _documentaryRepository.AddVideoInfoCommentAsync(comment, _currentUser);

            return Ok( await _videoInfoResponseModelsFactory.BuildCommentResponseModelAsync(VideoInfoComment));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpDelete(Router.Documentary.RemoveComment)]
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

    [HttpPost(Router.Documentary.AddCommentReact)]
    public async Task<ActionResult<ReactResponseModel>> AddCommentReactAsync([FromBody] VideoInfoCommentReact commentReact)
    {
        try
        {
            Tuple<VideoInfoCommentReact, VideoInfoCommentReactNotification> result = await _documentaryRepository.AddVideoInfoCommentReactAsync(commentReact, _currentUser);

            var user = await _userManager.FindByIdAsync(result.Item2.TargetUserId);

            if (result.Item2.TargetUserId != null && result.Item2.TargetUserId != this._currentUser.Id && user.Online)
            {
                CommentReactNotificationResponseModel notification = this._notificationResponseModelsFactory.BuildCommentReactNotificationResponseModel(result.Item2);

                await this._notificationHubContext.Clients.Client(user.ConnectionId).SendAsync("RecieveNotification", notification);
            }

            return Ok( _videoInfoResponseModelsFactory.BuildCommentReactResponseModel(result.Item1));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpDelete(Router.Documentary.RemoveCommentReact)]
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

    [HttpGet(Router.Documentary.Verses)]
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
