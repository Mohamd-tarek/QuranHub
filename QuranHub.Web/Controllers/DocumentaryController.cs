
namespace QuranHub.Web.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class DocumentaryController : ControllerBase
{
    private ILogger<DocumentaryController> _logger;
    private IDocumentaryRepository _documentaryRepository;
    private IVideoInfoViewModelsFactory _videoInfoViewModelsFactory;
    private INotificationViewModelsFactory _notificationViewModelsFactory;
    private IHubContext<NotificationHub> _notificationHubContext;
    private UserManager<QuranHubUser> _userManager;
    private HttpContext _httpContext;
    private QuranHubUser _currentUser;


    public DocumentaryController(
        ILogger<DocumentaryController> logger,
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
    public async Task<IEnumerable<PlayListInfo>> GetPlayListsInfoAsync() 
    {
        return await  this._documentaryRepository.GetPlayListsAsync();
    }

    [HttpGet("PlayListInfo/{PlaylistName}")]
    public async Task<PlayListInfo> GetPlayListsInfoAsync(string PlaylistName)
    {
        return await this._documentaryRepository.GetPlayListByNameAsync(PlaylistName);
    }

    [HttpGet("VideoInfoForPlayList/{playListName}/{offset}/{amount}")]
    public async Task<IEnumerable<VideoInfo>> GetVideoInfoForPlayList(string playListName, int offset = 0, int amount = 20) 
    {
        return await  this._documentaryRepository.GetVideoInfoForPlayListAsync(playListName, offset, amount);
    }

    [HttpGet("VideoInfo/{name}")]
    public async Task<VideoInfoViewModel> GetVideoInfoAsync(string name) 
    {
        VideoInfo videoInfo =  await  this._documentaryRepository.GetVideoInfoByNameAsync(name);

        return await this._videoInfoViewModelsFactory.BuildVideoInfoViewModelAsync(videoInfo);
    }

    [HttpGet("VideoInfo/{name}/{CommentId}")]
    public async Task<VideoInfoViewModel> GetVideoInfoAsync(string name, int CommentId)
    {
        VideoInfo videoInfo = await this._documentaryRepository.GetVideoInfoByNameAsync(name);

        return await this._videoInfoViewModelsFactory.BuildVideoInfoViewModelAsync(videoInfo);
    }

    

    [HttpGet("LoadMoreReacts/{VideoInfoId}/{Offset}/{Size}")]
    public async Task<IEnumerable<ReactViewModel>> LoadMoreVideoInfoReacts(int VideoInfoId, int Offset, int Size)
    {
        List<VideoInfoReact> videoInfoReacts = await _documentaryRepository.GetMoreVideoInfoReactsAsync(VideoInfoId, Offset, Size);

        List<ReactViewModel> videoInfoReactsModels = _videoInfoViewModelsFactory.BuildVideoInfoReactsViewModel(videoInfoReacts);

        return videoInfoReactsModels;
    }

    [HttpGet("LoadMoreComments/{VideoInfoId}/{Offset}/{Size}")]
    public async Task<IEnumerable<CommentViewModel>> LoadMoreCommentsAsync(int VideoInfoId, int Offset, int Size)
    {
        List<VideoInfoComment> comments = await _documentaryRepository.GetMoreVideoInfoCommentsAsync(VideoInfoId, Offset, Size);

        List<CommentViewModel> commentViewModels = await _videoInfoViewModelsFactory.BuildCommentsViewModelAsync(comments);

        return commentViewModels;
    }

    [HttpGet("LoadMoreCommentReacts/{VideoInfoId}/{Offset}/{Size}")]
    public async Task<IEnumerable<ReactViewModel>> LoadMoreCommentReactsAsync(int VideoInfoId, int Offset, int Size)
    {
        List<VideoInfoCommentReact> comments = await _documentaryRepository.GetMoreVideoInfoCommentReactsAsync(VideoInfoId, Offset, Size);

        List<ReactViewModel> commentReactViewModels = _videoInfoViewModelsFactory.BuildCommentReactsViewModel(comments);

        return commentReactViewModels;
    }

  

    [HttpPost("AddReact")]
    public async Task<ReactViewModel> AddVideoInfoReactAsync([FromBody] VideoInfoReact videoInfoReact)
    {
        VideoInfoReact VideoInfoReact = await _documentaryRepository.AddVideoInfoReactAsync(videoInfoReact
            , _currentUser);

       

        return _videoInfoViewModelsFactory.BuildVideoInfoReactViewModel(VideoInfoReact);
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
    public async Task<CommentViewModel> AddCommentAsync([FromBody] VideoInfoComment comment)
    {
        VideoInfoComment VideoInfoComment = await _documentaryRepository.AddVideoInfoCommentAsync(comment, _currentUser);

       

        return await _videoInfoViewModelsFactory.BuildCommentViewModelAsync(VideoInfoComment);
    }

    [HttpDelete("RemoveComment")]
    public async Task<Boolean> RemoveCommentAsync(int commentId)
    {
        return await _documentaryRepository.RemoveVideoInfoCommentAsync(commentId);
    }

    [HttpPost("AddCommentReact")]
    public async Task<ReactViewModel> AddCommentReactAsync([FromBody] VideoInfoCommentReact commentReact)
    {
        Tuple<VideoInfoCommentReact, VideoInfoCommentReactNotification> result = await _documentaryRepository.AddVideoInfoCommentReactAsync(commentReact, _currentUser);

        var user = await _userManager.FindByIdAsync(result.Item2.TargetUserId);

        if (result.Item2.TargetUserId != null && result.Item2.TargetUserId != this._currentUser.Id && user.Online)
        {
            CommentReactNotificationViewModel notification = this._notificationViewModelsFactory.BuildCommentReactNotificationViewModel(result.Item2);

            await this._notificationHubContext.Clients.Client(user.ConnectionId).SendAsync("RecieveNotification", notification);
        }

        return _videoInfoViewModelsFactory.BuildCommentReactViewModel(result.Item1);
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
    public async Task<IEnumerable<Verse>> GetVersesAsync()
    {
        return await _documentaryRepository.GetVersesAsync();
    }

   

  
}
