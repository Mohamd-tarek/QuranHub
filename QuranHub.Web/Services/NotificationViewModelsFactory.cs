
namespace QuranHub.Web.Services;

public class NotificationViewModelsFactory : INotificationViewModelsFactory
{
    private IUserViewModelsFactory _userViewModelsFactory;
     
    public NotificationViewModelsFactory(
        IUserViewModelsFactory userViewModelsFactory)
    {
        _userViewModelsFactory = userViewModelsFactory ?? throw new ArgumentNullException(nameof(userViewModelsFactory));
    }

    public  List<NotificationViewModel> BuildNotificationsViewModel(List<Notification> notifications)
    {
        List<NotificationViewModel> notificationsViewModels = new List<NotificationViewModel>();

        foreach (var notification in notifications)
        {
            notificationsViewModels.Add(this.BuildNotificationViewModel(notification));
        }

        return notificationsViewModels;
    }
    public NotificationViewModel BuildNotificationViewModel(Notification notification)
    {
        NotificationViewModel notificationViewModel = new NotificationViewModel()
        {
            NotificationId = notification.NotificationId,
            DateTime = notification.DateTime,
            SourceUser = this._userViewModelsFactory.BuildUserBasicInfoViewModel(notification.SourceUser),
            Message = notification.Message,
            Type = notification.Type,
            Seen = notification.Seen,

        };
        return notificationViewModel;
    }
    public FollowNotificationViewModel BuildFollowNotificationViewModel(FollowNotification followNotification)
    {
        FollowNotificationViewModel followNotificationViewModel = new FollowNotificationViewModel()
        {
            NotificationId = followNotification.NotificationId,
            DateTime = followNotification.DateTime,
            SourceUser = this._userViewModelsFactory.BuildUserBasicInfoViewModel(followNotification.SourceUser),
            Message = followNotification.Message,
            Type = followNotification.Type,
            Seen = followNotification.Seen,
            FollowId = followNotification.FollowId

        };
        return followNotificationViewModel;
    }
    public  PostNotificationViewModel BuildPostNotificationViewModel(PostNotification postNotification)
    {
        PostNotificationViewModel postNotificationViewModel = new PostNotificationViewModel()
        {
            NotificationId = postNotification.NotificationId,
            DateTime = postNotification.DateTime,
            SourceUser = this._userViewModelsFactory.BuildUserBasicInfoViewModel(postNotification.SourceUser),
            Message = postNotification.Message,
            Type = postNotification.Type,
            Seen = postNotification.Seen,
            PostId = postNotification.PostId

        };
        return postNotificationViewModel;
    }
    public PostReactNotificationViewModel BuildPostReactNotificationViewModel(PostReactNotification postReactNotification)
    {
        PostReactNotificationViewModel postReactNotificationViewModel = new PostReactNotificationViewModel()
        {
            NotificationId = postReactNotification.NotificationId,
            DateTime = postReactNotification.DateTime,
            SourceUser = this._userViewModelsFactory.BuildUserBasicInfoViewModel(postReactNotification.SourceUser),
            Message = postReactNotification.Message,
            Type = postReactNotification.Type,
            Seen = postReactNotification.Seen,
            PostId = postReactNotification.PostId,
            PostReactId = postReactNotification.ReactId

        };
        return postReactNotificationViewModel;
    }
    public ShareNotificationViewModel BuildShareNotificationViewModel(ShareNotification shareNotification)
    {
        ShareNotificationViewModel shareNotificationViewModel = new ShareNotificationViewModel()
        {
            NotificationId = shareNotification.NotificationId,
            DateTime = shareNotification.DateTime,
            SourceUser = this._userViewModelsFactory.BuildUserBasicInfoViewModel(shareNotification.SourceUser),
            Message = shareNotification.Message,
            Type = shareNotification.Type,
            Seen = shareNotification.Seen,
            PostId = shareNotification.PostId,
            ShareId =  shareNotification.ShareId
        };
        return shareNotificationViewModel;
    }
    public  CommentNotificationViewModel BuildCommentNotificationViewModel(CommentNotification commentNotification)
    {
        CommentNotificationViewModel commentNotificationViewModel = new CommentNotificationViewModel()
        {
            NotificationId = commentNotification.NotificationId,
            DateTime = commentNotification.DateTime,
            SourceUser = this._userViewModelsFactory.BuildUserBasicInfoViewModel(commentNotification.SourceUser),
            Message = commentNotification.Message,
            Type = commentNotification.Type,
            Seen = commentNotification.Seen,
            PostId = commentNotification.PostId,
            CommentId = commentNotification.CommentId 

        };
        return commentNotificationViewModel;

    }
    public CommentReactNotificationViewModel BuildCommentReactNotificationViewModel(CommentReactNotification commentReactNotification)
    {
        CommentReactNotificationViewModel commentReactNotificationViewModel = new CommentReactNotificationViewModel()
        {
            NotificationId = commentReactNotification.NotificationId,
            DateTime = commentReactNotification.DateTime,
            SourceUser = this._userViewModelsFactory.BuildUserBasicInfoViewModel(commentReactNotification.SourceUser),
            Message = commentReactNotification.Message,
            Type = commentReactNotification.Type,
            Seen = commentReactNotification.Seen,
            PostId = commentReactNotification.PostId,
            CommentId = commentReactNotification.CommentId,
            CommentReactId = commentReactNotification.CommentReactId
        };
        return commentReactNotificationViewModel;

    }


}
