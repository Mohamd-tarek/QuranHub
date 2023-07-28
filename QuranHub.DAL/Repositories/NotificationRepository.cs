
namespace QuranHub.DAL.Repositories;

public class NotificationRepository : INotificationRepository
{
    private IdentityDataContext _identityDataContext;
    public NotificationRepository(IdentityDataContext identityDataContext)
    {
        _identityDataContext = identityDataContext;  
    }

    public async Task<List<Notification>> GetNotificationsAsync(string userId)
    {
        List<Notification> Notifications = await this._identityDataContext.Notifications
                                                 .Include(notification => notification.SourceUser)
                                                 .Where(notification => notification.TargetUserId == userId)
                                                 .OrderByDescending(notification => notification.DateTime)
                                                 .Take(10)
                                                 .ToListAsync();
                                                 

        return Notifications;
    }

    public async Task<List<Notification>> GetMoreNotificationsAsync(int offset, int amount, QuranHubUser user)
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
    public async Task<object> GetNotificationByIdAsync(int notificationId, string type)
    {
        switch(type){
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
    public async Task<Notification> GetNotificationByIdAsync(int notificationId)
    {
        
        return await this._identityDataContext.Notifications
                         .Include(notification => notification.SourceUser)
                         .FirstAsync(notification => notification.NotificationId == notificationId );
    }
    public async Task<FollowNotification> GetFollowNotificationByIdAsync(int notificationId)
    {
        
        return await this._identityDataContext.FollowNotifications
                         .Include(notification => notification.SourceUser)
                         .FirstAsync(notification => notification.NotificationId == notificationId );
    }
    public async Task<PostReactNotification> GetPostReactNotificationByIdAsync(int notificationId)
    {
        
        return await this._identityDataContext.PostReactNotifications
                         .Include(notification => notification.SourceUser)
                         .FirstAsync(notification => notification.NotificationId == notificationId );
    }
    public async Task<ShareNotification> GetShareNotificationByIdAsync(int notificationId)
    {
        
        return await this._identityDataContext.ShareNotifications
                         .Include(notification => notification.SourceUser)
                         .FirstAsync(notification => notification.NotificationId == notificationId );

    }

    public async Task<PostShareNotification> GetPostShareNotificationByIdAsync(int notificationId)
    {

        return await this._identityDataContext.PostShareNotifications
                         .Include(notification => notification.SourceUser)
                         .FirstAsync(notification => notification.NotificationId == notificationId);

    }
    public async Task<CommentNotification> GetCommentNotificationByIdAsync(int notificationId)
    {
        
        return await this._identityDataContext.CommentNotifications
                         .Include(notification => notification.SourceUser)
                         .FirstAsync(notification => notification.NotificationId == notificationId );
    }
    public async Task<PostCommentNotification> GetPostCommentNotificationByIdAsync(int notificationId)
    {

        return await this._identityDataContext.PostCommentNotifications
                         .Include(notification => notification.SourceUser)
                         .FirstAsync(notification => notification.NotificationId == notificationId);
    }

    public async Task<CommentReactNotification> GetCommentReactNotificationByIdAsync(int notificationId)
    {
        
        return await this._identityDataContext.CommentReactNotifications
                         .Include(notification => notification.SourceUser)
                         .FirstAsync(notification => notification.NotificationId == notificationId );
    }
    public async Task<PostCommentReactNotification> GetPostCommentReactNotificationByIdAsync(int notificationId)
    {

        return await this._identityDataContext.PostCommentReactNotifications
                         .Include(notification => notification.SourceUser)
                         .FirstAsync(notification => notification.NotificationId == notificationId);
    }

    public async Task<List<Notification>> GetUserNotificationsAsync(QuranHubUser user)
    {
        return await this.GetNotificationsAsync(user.Id);

    }


    public async Task MarkNotificationAsSeenAsync(int notificationId)
    {
        Notification notification = await this._identityDataContext.Notifications.FindAsync(notificationId);

        notification.Seen = true;

        await this._identityDataContext.SaveChangesAsync();

    }

    public async Task<bool> DeleteNotificationAsync(int notificationId)
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

}
