

namespace QuranHub.Web.Controllers;

[ApiController]

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class NotificationController : ControllerBase
{
    private readonly Serilog.ILogger _logger;
    private INotificationRepository _notificationRepository;
    private INotificationResponseModelsFactory _notificationResponseModelsFactory;
    private UserManager<QuranHubUser> _userManager;
    private HttpContext _httpContext;
    private QuranHubUser _currentUser;
    public NotificationController(
        Serilog.ILogger logger,
        INotificationRepository notificationRepository,
        INotificationResponseModelsFactory notificationResponseModelsFactory,   
        UserManager<QuranHubUser> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _httpContext = httpContextAccessor.HttpContext  ?? throw new ArgumentNullException(nameof(httpContextAccessor));;
        _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
        _notificationResponseModelsFactory = notificationResponseModelsFactory ?? throw new ArgumentNullException(nameof(notificationResponseModelsFactory));

        ClaimsPrincipal claimsPrincipal = this._httpContext.User;

        if(claimsPrincipal.Identity.IsAuthenticated)
        {
            _currentUser = _userManager.GetUserAsync(this._httpContext.User).Result;
        }
    }
    [HttpGet(Router.Notification.GetNotificationById)]
    public async Task<ActionResult<object>> GetNotificationByIdAsync(int notificationId)
    {
        try
        {
            Notification notification = await _notificationRepository.GetNotificationByIdAsync(notificationId);
        
            switch(notification.Type){
                case "FollowNotification" : return Ok( await this.GetFollowNotificationByIdAsync(notificationId)); break;
                case "PostReactNotification" : return Ok(await this.GetPostReactNotificationByIdAsync(notificationId)); break;
                case "ShareNotification" : return Ok(await this.GetShareNotificationByIdAsync(notificationId)); break;
                case "PostShareNotification": return Ok(await this.GetPostShareNotificationByIdAsync(notificationId)); break;
                case "CommentNotification" : return Ok(await this.GetCommentNotificationByIdAsync(notificationId)); break;
                case "PostCommentNotification": return Ok(await this.GetPostCommentNotificationByIdAsync(notificationId)); break;
                case "CommentReactNotification" : return Ok(await this.GetCommentReactNotificationByIdAsync(notificationId)); break;
                case "PostCommentReactNotification": return Ok(await this.GetPostCommentReactNotificationByIdAsync(notificationId)); break;
                default: return Ok(this._notificationResponseModelsFactory.BuildNotificationResponseModel(notification)); break;
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }
    private async Task<ActionResult<FollowNotificationResponseModel>> GetFollowNotificationByIdAsync(int notifictionId)
    {
        try
        {
            FollowNotification notification =  await _notificationRepository.GetFollowNotificationByIdAsync(notifictionId);
            FollowNotificationResponseModel notificationResponseModel =  this._notificationResponseModelsFactory.BuildFollowNotificationResponseModel(notification);
            return Ok(notificationResponseModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    private async Task<ActionResult<ShareNotificationResponseModel>> GetShareNotificationByIdAsync(int notifictionId)
    {
        try
        {
            ShareNotification notification =  await _notificationRepository.GetShareNotificationByIdAsync(notifictionId);
            ShareNotificationResponseModel notificationResponseModel =  this._notificationResponseModelsFactory.BuildShareNotificationResponseModel(notification);
            return Ok(notificationResponseModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }
    private async Task<ActionResult<ShareNotificationResponseModel>> GetPostShareNotificationByIdAsync(int notifictionId)
    {
        try
        {
            PostShareNotification notification = await _notificationRepository.GetPostShareNotificationByIdAsync(notifictionId);
            Console.WriteLine("notification.PostId: " + notification.PostId);
            ShareNotificationResponseModel notificationResponseModel = this._notificationResponseModelsFactory.BuildPostShareNotificationResponseModel(notification);
            return Ok(notificationResponseModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }
    private async Task<ActionResult<CommentNotificationResponseModel>> GetCommentNotificationByIdAsync(int notifictionId)
    {
        try
        {
            CommentNotification notification =  await _notificationRepository.GetCommentNotificationByIdAsync(notifictionId);
            CommentNotificationResponseModel notificationResponseModel =  this._notificationResponseModelsFactory.BuildCommentNotificationResponseModel(notification);
            return Ok(notificationResponseModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }
    private async Task<ActionResult<CommentNotificationResponseModel>> GetPostCommentNotificationByIdAsync(int notifictionId)
    {
        try
        {
            PostCommentNotification notification = await _notificationRepository.GetPostCommentNotificationByIdAsync(notifictionId);
            CommentNotificationResponseModel notificationResponseModel = this._notificationResponseModelsFactory.BuildPostCommentNotificationResponseModel(notification);
            return Ok(notificationResponseModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    private async Task<ActionResult<CommentReactNotificationResponseModel>> GetCommentReactNotificationByIdAsync(int notifictionId)
    {
        try
        {
            CommentReactNotification notification =  await _notificationRepository.GetCommentReactNotificationByIdAsync(notifictionId);
            CommentReactNotificationResponseModel notificationResponseModel =  this._notificationResponseModelsFactory.BuildCommentReactNotificationResponseModel(notification);
            return Ok(notificationResponseModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }
    private async Task<ActionResult<CommentReactNotificationResponseModel>> GetPostCommentReactNotificationByIdAsync(int notifictionId)
    {
        try
        {
            PostCommentReactNotification notification = await _notificationRepository.GetPostCommentReactNotificationByIdAsync(notifictionId);
            CommentReactNotificationResponseModel notificationResponseModel = this._notificationResponseModelsFactory.BuildPostCommentReactNotificationResponseModel(notification);
            return Ok(notificationResponseModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    private async Task<ActionResult<PostReactNotificationResponseModel>> GetPostReactNotificationByIdAsync(int notifictionId)
    {
        try
        {
            PostReactNotification notification =  await _notificationRepository.GetPostReactNotificationByIdAsync(notifictionId);
            PostReactNotificationResponseModel notificationResponseModel =  this._notificationResponseModelsFactory.BuildPostReactNotificationResponseModel(notification);
            return Ok(notificationResponseModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Notification.Recent)]
    public async Task<ActionResult<IEnumerable<NotificationResponseModel>>> GetRecentNotificationsAsync() 
    {
        try
        {
            List<Notification> notifications =  await _notificationRepository.GetUserNotificationsAsync(_currentUser);

            List<NotificationResponseModel> notificationsResponseModels =  this._notificationResponseModelsFactory.BuildNotificationsResponseModel(notifications);

            return Ok(notificationsResponseModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Notification.LoadMoreNotifications)]
    public async Task<ActionResult<IEnumerable<object>>> LoadMoreNotificatinsAsync(int Offset, int Size) 
    {
        try
        {
            List<Notification> notifications = await _notificationRepository.GetMoreNotificationsAsync(Offset, Size, _currentUser);

            List<NotificationResponseModel> notificationsResponseModels =  this._notificationResponseModelsFactory.BuildNotificationsResponseModel(notifications);

            return Ok(notificationsResponseModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Notification.Seen)]
    public async Task<ActionResult> MarkNotificationAsSeenAsync(int NotifictionId)
    {
        try
        {
            await _notificationRepository.MarkNotificationAsSeenAsync(NotifictionId);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpDelete(Router.Notification.Delete)]
    public async Task<ActionResult> DeleteNotificationAsync(int notificationId)
    {
        try
        {
            if ( await _notificationRepository.DeleteNotificationAsync(notificationId))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }
}
