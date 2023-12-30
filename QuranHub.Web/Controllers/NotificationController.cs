

namespace QuranHub.Web.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly Serilog.ILogger _logger;
    private INotificationRepository _notificationRepository;
    private INotificationViewModelsFactory _notificationViewModelsFactory;
    private UserManager<QuranHubUser> _userManager;
    private HttpContext _httpContext;
    private QuranHubUser _currentUser;
    public NotificationController(
        Serilog.ILogger logger,
        INotificationRepository notificationRepository,
        INotificationViewModelsFactory notificationViewModelsFactory,   
        UserManager<QuranHubUser> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _httpContext = httpContextAccessor.HttpContext  ?? throw new ArgumentNullException(nameof(httpContextAccessor));;
        _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
        _notificationViewModelsFactory = notificationViewModelsFactory ?? throw new ArgumentNullException(nameof(notificationViewModelsFactory));

        ClaimsPrincipal claimsPrincipal = this._httpContext.User;

        if(claimsPrincipal.Identity.IsAuthenticated)
        {
            _currentUser = _userManager.GetUserAsync(this._httpContext.User).Result;
        }
    }
    [HttpGet("GetNotificationById/{notificationId}")]
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
                default: return Ok(this._notificationViewModelsFactory.BuildNotificationViewModel(notification)); break;
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }
    private async Task<ActionResult<FollowNotificationViewModel>> GetFollowNotificationByIdAsync(int notifictionId)
    {
        try
        {
            FollowNotification notification =  await _notificationRepository.GetFollowNotificationByIdAsync(notifictionId);
            FollowNotificationViewModel notificationViewModel =  this._notificationViewModelsFactory.BuildFollowNotificationViewModel(notification);
            return Ok(notificationViewModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    private async Task<ActionResult<ShareNotificationViewModel>> GetShareNotificationByIdAsync(int notifictionId)
    {
        try
        {
            ShareNotification notification =  await _notificationRepository.GetShareNotificationByIdAsync(notifictionId);
            ShareNotificationViewModel notificationViewModel =  this._notificationViewModelsFactory.BuildShareNotificationViewModel(notification);
            return Ok(notificationViewModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }
    private async Task<ActionResult<ShareNotificationViewModel>> GetPostShareNotificationByIdAsync(int notifictionId)
    {
        try
        {
            PostShareNotification notification = await _notificationRepository.GetPostShareNotificationByIdAsync(notifictionId);
            Console.WriteLine("notification.PostId: " + notification.PostId);
            ShareNotificationViewModel notificationViewModel = this._notificationViewModelsFactory.BuildPostShareNotificationViewModel(notification);
            return Ok(notificationViewModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }
    private async Task<ActionResult<CommentNotificationViewModel>> GetCommentNotificationByIdAsync(int notifictionId)
    {
        try
        {
            CommentNotification notification =  await _notificationRepository.GetCommentNotificationByIdAsync(notifictionId);
            CommentNotificationViewModel notificationViewModel =  this._notificationViewModelsFactory.BuildCommentNotificationViewModel(notification);
            return Ok(notificationViewModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }
    private async Task<ActionResult<CommentNotificationViewModel>> GetPostCommentNotificationByIdAsync(int notifictionId)
    {
        try
        {
            PostCommentNotification notification = await _notificationRepository.GetPostCommentNotificationByIdAsync(notifictionId);
            CommentNotificationViewModel notificationViewModel = this._notificationViewModelsFactory.BuildPostCommentNotificationViewModel(notification);
            return Ok(notificationViewModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    private async Task<ActionResult<CommentReactNotificationViewModel>> GetCommentReactNotificationByIdAsync(int notifictionId)
    {
        try
        {
            CommentReactNotification notification =  await _notificationRepository.GetCommentReactNotificationByIdAsync(notifictionId);
            CommentReactNotificationViewModel notificationViewModel =  this._notificationViewModelsFactory.BuildCommentReactNotificationViewModel(notification);
            return Ok(notificationViewModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }
    private async Task<ActionResult<CommentReactNotificationViewModel>> GetPostCommentReactNotificationByIdAsync(int notifictionId)
    {
        try
        {
            PostCommentReactNotification notification = await _notificationRepository.GetPostCommentReactNotificationByIdAsync(notifictionId);
            CommentReactNotificationViewModel notificationViewModel = this._notificationViewModelsFactory.BuildPostCommentReactNotificationViewModel(notification);
            return Ok(notificationViewModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    private async Task<ActionResult<PostReactNotificationViewModel>> GetPostReactNotificationByIdAsync(int notifictionId)
    {
        try
        {
            PostReactNotification notification =  await _notificationRepository.GetPostReactNotificationByIdAsync(notifictionId);
            PostReactNotificationViewModel notificationViewModel =  this._notificationViewModelsFactory.BuildPostReactNotificationViewModel(notification);
            return Ok(notificationViewModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet("Recent")]
    public async Task<ActionResult<IEnumerable<NotificationViewModel>>> GetRecentNotificationsAsync() 
    {
        try
        {
            List<Notification> notifications =  await _notificationRepository.GetUserNotificationsAsync(_currentUser);

            List<NotificationViewModel> notificationsViewModels =  this._notificationViewModelsFactory.BuildNotificationsViewModel(notifications);

            return Ok(notificationsViewModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet("LoadMoreNotifications/{Offset}/{Size}")]
    public async Task<ActionResult<IEnumerable<object>>> LoadMoreNotificatinsAsync(int Offset, int Size) 
    {
        try
        {
            List<Notification> notifications = await _notificationRepository.GetMoreNotificationsAsync(Offset, Size, _currentUser);

            List<NotificationViewModel> notificationsViewModels =  this._notificationViewModelsFactory.BuildNotificationsViewModel(notifications);

            return Ok(notificationsViewModels);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet("Seen/{NotificationId}")]
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

    [HttpDelete("Delete")]
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
