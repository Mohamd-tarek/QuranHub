

namespace QuranHub.Web.Services;

public interface INotificationViewModelsFactory
{
    public List<NotificationViewModel> BuildNotificationsViewModel(List<Notification> notifications);
    public NotificationViewModel BuildNotificationViewModel(Notification notification );
    public FollowNotificationViewModel BuildFollowNotificationViewModel(FollowNotification followNotification);
    public PostReactNotificationViewModel BuildPostReactNotificationViewModel(PostReactNotification postReactNotification);
    public ShareNotificationViewModel BuildShareNotificationViewModel(ShareNotification shareNotification);
    public ShareNotificationViewModel BuildPostShareNotificationViewModel(PostShareNotification shareNotification);
    public CommentNotificationViewModel BuildCommentNotificationViewModel(CommentNotification commentNotification);
    public CommentNotificationViewModel BuildPostCommentNotificationViewModel(PostCommentNotification commentNotification);
    public CommentReactNotificationViewModel BuildCommentReactNotificationViewModel(CommentReactNotification commentReactNotification);
    public CommentReactNotificationViewModel BuildPostCommentReactNotificationViewModel(PostCommentReactNotification commentReactNotification);

}
