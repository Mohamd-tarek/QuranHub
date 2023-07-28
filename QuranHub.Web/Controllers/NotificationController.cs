

namespace QuranHub.Web.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private ILogger<NotificationController> _logger;
    private INotificationRepository _notificationRepository;
    private INotificationViewModelsFactory _notificationViewModelsFactory;
    private UserManager<QuranHubUser> _userManager;
    private HttpContext _httpContext;
    private QuranHubUser _currentUser;
    public NotificationController(
        ILogger<NotificationController> logger,
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
    public async Task<object> GetNotificationByIdAsync(int notificationId)
    {   
        Notification notification = await _notificationRepository.GetNotificationByIdAsync(notificationId);
        
        switch(notification.Type){
            case "FollowNotification" : return await this.GetFollowNotificationByIdAsync(notificationId); break;
            case "PostReactNotification" : return await this.GetPostReactNotificationByIdAsync(notificationId); break;
            case "ShareNotification" : return await this.GetShareNotificationByIdAsync(notificationId); break;
            case "PostShareNotification": return await this.GetPostShareNotificationByIdAsync(notificationId); break;
            case "CommentNotification" : return await this.GetCommentNotificationByIdAsync(notificationId); break;
            case "PostCommentNotification": return await this.GetPostCommentNotificationByIdAsync(notificationId); break;
            case "CommentReactNotification" : return await this.GetCommentReactNotificationByIdAsync(notificationId); break;
            case "PostCommentReactNotification": return await this.GetPostCommentReactNotificationByIdAsync(notificationId); break;
            default: return  this._notificationViewModelsFactory.BuildNotificationViewModel(notification); break;
        }
    }
    private async Task<FollowNotificationViewModel> GetFollowNotificationByIdAsync(int notifictionId)
    {
        FollowNotification notification =  await _notificationRepository.GetFollowNotificationByIdAsync(notifictionId);
        FollowNotificationViewModel notificationViewModel =  this._notificationViewModelsFactory.BuildFollowNotificationViewModel(notification);
        return notificationViewModel;
    }

    private async Task<ShareNotificationViewModel> GetShareNotificationByIdAsync(int notifictionId)
    {
        ShareNotification notification =  await _notificationRepository.GetShareNotificationByIdAsync(notifictionId);
        ShareNotificationViewModel notificationViewModel =  this._notificationViewModelsFactory.BuildShareNotificationViewModel(notification);
        return notificationViewModel;
    }
    private async Task<ShareNotificationViewModel> GetPostShareNotificationByIdAsync(int notifictionId)
    {
        PostShareNotification notification = await _notificationRepository.GetPostShareNotificationByIdAsync(notifictionId);
        ShareNotificationViewModel notificationViewModel = this._notificationViewModelsFactory.BuildShareNotificationViewModel(notification);
        return notificationViewModel;
    }
    private async Task<CommentNotificationViewModel> GetCommentNotificationByIdAsync(int notifictionId)
    {
        CommentNotification notification =  await _notificationRepository.GetCommentNotificationByIdAsync(notifictionId);
        CommentNotificationViewModel notificationViewModel =  this._notificationViewModelsFactory.BuildCommentNotificationViewModel(notification);
        return notificationViewModel;
    }
    private async Task<CommentNotificationViewModel> GetPostCommentNotificationByIdAsync(int notifictionId)
    {
        PostCommentNotification notification = await _notificationRepository.GetPostCommentNotificationByIdAsync(notifictionId);
        CommentNotificationViewModel notificationViewModel = this._notificationViewModelsFactory.BuildCommentNotificationViewModel(notification);
        return notificationViewModel;
    }

    private async Task<CommentReactNotificationViewModel> GetCommentReactNotificationByIdAsync(int notifictionId)
    {
        CommentReactNotification notification =  await _notificationRepository.GetCommentReactNotificationByIdAsync(notifictionId);
        CommentReactNotificationViewModel notificationViewModel =  this._notificationViewModelsFactory.BuildCommentReactNotificationViewModel(notification);
        return notificationViewModel;
    }
    private async Task<CommentReactNotificationViewModel> GetPostCommentReactNotificationByIdAsync(int notifictionId)
    {
        PostCommentReactNotification notification = await _notificationRepository.GetPostCommentReactNotificationByIdAsync(notifictionId);
        CommentReactNotificationViewModel notificationViewModel = this._notificationViewModelsFactory.BuildCommentReactNotificationViewModel(notification);
        return notificationViewModel;
    }

    private async Task<PostReactNotificationViewModel> GetPostReactNotificationByIdAsync(int notifictionId)
    {
        PostReactNotification notification =  await _notificationRepository.GetPostReactNotificationByIdAsync(notifictionId);
        PostReactNotificationViewModel notificationViewModel =  this._notificationViewModelsFactory.BuildPostReactNotificationViewModel(notification);
        return notificationViewModel;
    }

    [HttpGet("Recent")]
    public async Task<IEnumerable<NotificationViewModel>> GetRecentNotificationsAsync() 
    {
        List<Notification> notifications =  await _notificationRepository.GetUserNotificationsAsync(_currentUser);

        List<NotificationViewModel> notificationsViewModels =  this._notificationViewModelsFactory.BuildNotificationsViewModel(notifications);

        return notificationsViewModels;
    }

    [HttpGet("LoadMoreNotifications/{Offset}/{Size}")]
    public async Task<IEnumerable<object>> LoadMoreNotificatinsAsync(int Offset, int Size) 
    {
        List<Notification> notifications = await _notificationRepository.GetMoreNotificationsAsync(Offset, Size, _currentUser);

        List<NotificationViewModel> notificationsViewModels =  this._notificationViewModelsFactory.BuildNotificationsViewModel(notifications);

        return notificationsViewModels;
    }

    [HttpGet("Seen/{NotificationId}")]
    public async Task MarkNotificationAsSeenAsync(int NotifictionId)
    {
        await _notificationRepository.MarkNotificationAsSeenAsync(NotifictionId);
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> DeleteNotificationAsync(int notificationId)
    {
       if( await _notificationRepository.DeleteNotificationAsync(notificationId))
       {
            return Ok();
       }
       else
       {
            return BadRequest();
       }
    }
}
