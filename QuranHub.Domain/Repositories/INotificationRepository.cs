
namespace QuranHub.Domain.Repositories;

/// <summary>
/// represent notification repository
/// </summary>
public interface INotificationRepository 
{
    /// <summary>
    ///  get notfication for a user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<List<Notification>> GetNotificationsAsync(string userId);
    /// <summary>
    /// get notification by Id
    /// </summary>
    /// <param name="notificationId"></param>
    /// <returns></returns>
    public Task<Notification> GetNotificationByIdAsync(int notificationId);
    /// <summary>
    /// get follow relationship notifcation
    /// </summary>
    /// <param name="notificationId"></param>
    /// <returns></returns>
    public Task<FollowNotification> GetFollowNotificationByIdAsync(int notificationId);
    /// <summary>
    /// get post react notification
    /// </summary>
    /// <param name="notificationId"></param>
    /// <returns></returns>
    public Task<PostReactNotification> GetPostReactNotificationByIdAsync(int notificationId);
    /// <summary>
    /// get share notifcation
    /// </summary>
    /// <param name="notificationId"></param>
    /// <returns></returns>
    public Task<ShareNotification> GetShareNotificationByIdAsync(int notificationId);
    /// <summary>
    /// get post share notification
    /// </summary>
    /// <param name="notificationId"></param>
    /// <returns></returns>
    public Task<PostShareNotification> GetPostShareNotificationByIdAsync(int notificationId);
    /// <summary>
    /// get comment notification
    /// </summary>
    /// <param name="notificationId"></param>
    /// <returns></returns>
    public Task<CommentNotification> GetCommentNotificationByIdAsync(int notificationId);
    /// <summary>
    /// get post comment notification
    /// </summary>
    /// <param name="notificationId"></param>
    /// <returns></returns>
    public Task<PostCommentNotification> GetPostCommentNotificationByIdAsync(int notificationId);
    /// <summary>
    /// get comment react notification
    /// </summary>
    /// <param name="notificationId"></param>
    /// <returns></returns>
    public Task<CommentReactNotification> GetCommentReactNotificationByIdAsync(int notificationId);
    /// <summary>
    /// get post comment react notification
    /// </summary>
    /// <param name="notificationId"></param>
    /// <returns></returns>
    public Task<PostCommentReactNotification> GetPostCommentReactNotificationByIdAsync(int notificationId);
    /// <summary>
    /// get user notification
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<List<Notification>> GetUserNotificationsAsync(QuranHubUser user);
    /// <summary>
    /// get more notifification
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="amount"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<List<Notification>> GetMoreNotificationsAsync(int offset, int amount, QuranHubUser user);
    /// <summary>
    /// mark notification as seen
    /// </summary>
    /// <param name="notificationId"></param>
    /// <returns></returns>
    public Task MarkNotificationAsSeenAsync(int notificationId);
    /// <summary>
    /// delete notification
    /// </summary>
    /// <param name="notificationId"></param>
    /// <returns></returns>
    public Task<bool> DeleteNotificationAsync(int notificationId);

}
