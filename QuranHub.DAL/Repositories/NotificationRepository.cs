
using Microsoft.Extensions.Logging;

namespace QuranHub.DAL.Repositories;
/// <inheritdoc/>
public class NotificationRepository : INotificationRepository
{
    private IdentityDataContext _identityDataContext;
    private readonly ILogger<NotificationRepository> _logger;
    public NotificationRepository(
        IdentityDataContext identityDataContext,
        ILogger<NotificationRepository> logger)
    {
        _identityDataContext = identityDataContext;
        _logger = logger;
    }

    public async Task<List<Notification>> GetNotificationsAsync(string userId)
    {
        try
        {
            List<Notification> Notifications = await this._identityDataContext.Notifications
                                                 .Include(notification => notification.SourceUser)
                                                 .Where(notification => notification.TargetUserId == userId)
                                                 .OrderByDescending(notification => notification.DateTime)
                                                 .Take(10)
                                                 .ToListAsync();
            return Notifications;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<List<Notification>> GetMoreNotificationsAsync(int offset, int amount, QuranHubUser user)
    {
        try
        {
            List<Notification> Notifications = await _identityDataContext.Notifications
                                           .Include(notification => notification.SourceUser)
                                           .Where(notification => notification.TargetUserId == user.Id)
                                           .OrderByDescending(notification => notification.DateTime)
                                           .AsQueryable()
                                           .Skip(offset)
                                           .Take(amount)
                                           .ToListAsync();
            return Notifications;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }
    public async Task<object> GetNotificationByIdAsync(int notificationId, string type)
    {
        try
        {
            switch (type){
            case "FollowNotification" : return await this.GetFollowNotificationByIdAsync(notificationId); break;
            case "PostReactNotification" : return await this.GetPostReactNotificationByIdAsync(notificationId); break;
            case "ShareNotification" : return await this.GetShareNotificationByIdAsync(notificationId); break;
            case "PostShareNotification": return await this.GetPostShareNotificationByIdAsync(notificationId); break;
            case "CommentNotification" : return await this.GetCommentNotificationByIdAsync(notificationId); break;
            case "PostCommentNotification": return await this.GetPostCommentNotificationByIdAsync(notificationId); break;
            case "CommentReactNotification" : return await this.GetCommentReactNotificationByIdAsync(notificationId); break;
            case "PostCommentReactNotification": return await this.GetPostCommentReactNotificationByIdAsync(notificationId); break;
            default: return await this.GetNotificationByIdAsync(notificationId); break;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }
    public async Task<Notification> GetNotificationByIdAsync(int notificationId)
    {
        try
        {
            return await this._identityDataContext.Notifications
                         .Include(notification => notification.SourceUser)
                         .FirstAsync(notification => notification.NotificationId == notificationId );
}
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }
    public async Task<FollowNotification> GetFollowNotificationByIdAsync(int notificationId)
    {
        try
        {
            return await this._identityDataContext.FollowNotifications
                         .Include(notification => notification.SourceUser)
                         .FirstAsync(notification => notification.NotificationId == notificationId );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }
    public async Task<PostReactNotification> GetPostReactNotificationByIdAsync(int notificationId)
    {
        try
        {
            return await this._identityDataContext.PostReactNotifications
                         .Include(notification => notification.SourceUser)
                         .FirstAsync(notification => notification.NotificationId == notificationId );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }
    public async Task<ShareNotification> GetShareNotificationByIdAsync(int notificationId)
    {
        try
        {
            return await this._identityDataContext.ShareNotifications
                         .Include(notification => notification.SourceUser)
                         .FirstAsync(notification => notification.NotificationId == notificationId );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }

    }

    public async Task<PostShareNotification> GetPostShareNotificationByIdAsync(int notificationId)
    {
        try
        {
            return await this._identityDataContext.PostShareNotifications
                         .Include(notification => notification.SourceUser)
                         .FirstAsync(notification => notification.NotificationId == notificationId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }
    public async Task<CommentNotification> GetCommentNotificationByIdAsync(int notificationId)
    {
        try
        {
            return await this._identityDataContext.CommentNotifications
                         .Include(notification => notification.SourceUser)
                         .FirstAsync(notification => notification.NotificationId == notificationId );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }
    public async Task<PostCommentNotification> GetPostCommentNotificationByIdAsync(int notificationId)
    {
        try
        {
            return await this._identityDataContext.PostCommentNotifications
                         .Include(notification => notification.SourceUser)
                         .FirstAsync(notification => notification.NotificationId == notificationId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<CommentReactNotification> GetCommentReactNotificationByIdAsync(int notificationId)
    {
        try
        {
            return await this._identityDataContext.CommentReactNotifications
                         .Include(notification => notification.SourceUser)
                         .FirstAsync(notification => notification.NotificationId == notificationId );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }
    public async Task<PostCommentReactNotification> GetPostCommentReactNotificationByIdAsync(int notificationId)
    {
        try
        {
            return await this._identityDataContext.PostCommentReactNotifications
                         .Include(notification => notification.SourceUser)
                         .FirstAsync(notification => notification.NotificationId == notificationId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<List<Notification>> GetUserNotificationsAsync(QuranHubUser user)
    {
        try
        {
            return await this.GetNotificationsAsync(user.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }


    public async Task MarkNotificationAsSeenAsync(int notificationId)
     {
        try
        {
            Notification notification = await this._identityDataContext.Notifications.FindAsync(notificationId);

            notification.Seen = true;

            await this._identityDataContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return;
        }
    }

    public async Task<bool> DeleteNotificationAsync(int notificationId)
    {
        try
        {
            Notification notification = await this._identityDataContext.Notifications.FindAsync(notificationId);

            EntityEntry<Notification> notificationEntityEntry = this._identityDataContext.Notifications.Remove(notification);

            await _identityDataContext.SaveChangesAsync();

            if (notificationEntityEntry.State.Equals(EntityState.Detached))
            {
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }
    }

}
