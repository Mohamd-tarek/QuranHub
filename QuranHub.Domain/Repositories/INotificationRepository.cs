
namespace QuranHub.Domain.Repositories;

public interface INotificationRepository 
{
    public Task<List<Notification>> GetNotificationsAsync(string userId);
    public Task<Notification> GetNotificationByIdAsync(int notificationId);
    public Task<FollowNotification> GetFollowNotificationByIdAsync(int notificationId);
    public Task<PostReactNotification> GetPostReactNotificationByIdAsync(int notificationId);
    public Task<ShareNotification> GetShareNotificationByIdAsync(int notificationId);
    public Task<PostShareNotification> GetPostShareNotificationByIdAsync(int notificationId);
    public Task<CommentNotification> GetCommentNotificationByIdAsync(int notificationId);
    public Task<PostCommentNotification> GetPostCommentNotificationByIdAsync(int notificationId);
    public Task<CommentReactNotification> GetCommentReactNotificationByIdAsync(int notificationId);
    public Task<PostCommentReactNotification> GetPostCommentReactNotificationByIdAsync(int notificationId);
    public Task<List<Notification>> GetUserNotificationsAsync(QuranHubUser user);
    public Task<List<Notification>> GetMoreNotificationsAsync(int offset, int amount, QuranHubUser user);
    public Task MarkNotificationAsSeenAsync(int notificationId);

    public Task<bool> DeleteNotificationAsync(int notificationId);

}
