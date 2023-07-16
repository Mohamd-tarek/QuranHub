

namespace QuranHub.Web.Services;

public interface INotificationViewModelsFactory
{
    public List<NotificationViewModel> BuildNotificationsViewModel(List<Notification> notifications);
    public NotificationViewModel BuildNotificationViewModel(Notification notification );
    public FollowNotificationViewModel BuildFollowNotificationViewModel(FollowNotification followNotification);
    public PostNotificationViewModel BuildPostNotificationViewModel(PostNotification postNotification);
    public PostReactNotificationViewModel BuildPostReactNotificationViewModel(PostReactNotification postReactNotification);
    public ShareNotificationViewModel BuildShareNotificationViewModel(ShareNotification shareNotification);
    public CommentNotificationViewModel BuildCommentNotificationViewModel(CommentNotification commentNotification);
    public CommentReactNotificationViewModel BuildCommentReactNotificationViewModel(CommentReactNotification commentReactNotification);

 }
