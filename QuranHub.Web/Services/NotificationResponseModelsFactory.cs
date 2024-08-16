
namespace QuranHub.Web.Services;

public class NotificationResponseModelsFactory : INotificationResponseModelsFactory
{
    private IUserResponseModelsFactory _userResponseModelsFactory;
     
    public NotificationResponseModelsFactory(
        IUserResponseModelsFactory userResponseModelsFactory)
    {
        _userResponseModelsFactory = userResponseModelsFactory ?? throw new ArgumentNullException(nameof(userResponseModelsFactory));
    }

    public  List<NotificationResponseModel> BuildNotificationsResponseModel(List<Notification> notifications)
    {
        List<NotificationResponseModel> notificationsResponseModels = new List<NotificationResponseModel>();

        foreach (var notification in notifications)
        {
            notificationsResponseModels.Add(this.BuildNotificationResponseModel(notification));
        }

        return notificationsResponseModels;
    }
    public NotificationResponseModel BuildNotificationResponseModel(Notification notification)
    {
        NotificationResponseModel notificationResponseModel = new NotificationResponseModel()
        {
            NotificationId = notification.NotificationId,
            DateTime = notification.DateTime,
            SourceUser = this._userResponseModelsFactory.BuildUserBasicInfoResponseModel(notification.SourceUser),
            Message = notification.Message,
            Type = notification.Type,
            Seen = notification.Seen,

        };
        return notificationResponseModel;
    }
    public FollowNotificationResponseModel BuildFollowNotificationResponseModel(FollowNotification followNotification)
    {
        FollowNotificationResponseModel followNotificationResponseModel = new FollowNotificationResponseModel()
        {
            NotificationId = followNotification.NotificationId,
            DateTime = followNotification.DateTime,
            SourceUser = this._userResponseModelsFactory.BuildUserBasicInfoResponseModel(followNotification.SourceUser),
            Message = followNotification.Message,
            Type = followNotification.Type,
            Seen = followNotification.Seen,
            FollowId = followNotification.FollowId

        };
        return followNotificationResponseModel;
    }
   
    public PostReactNotificationResponseModel BuildPostReactNotificationResponseModel(PostReactNotification postReactNotification)
    {
        PostReactNotificationResponseModel postReactNotificationResponseModel = new PostReactNotificationResponseModel()
        {
            NotificationId = postReactNotification.NotificationId,
            DateTime = postReactNotification.DateTime,
            SourceUser = this._userResponseModelsFactory.BuildUserBasicInfoResponseModel(postReactNotification.SourceUser),
            Message = postReactNotification.Message,
            Type = postReactNotification.Type,
            Seen = postReactNotification.Seen,
            PostId = postReactNotification.PostId,
            PostReactId = postReactNotification.ReactId ??= 0

        };
        return postReactNotificationResponseModel;
    }
    public ShareNotificationResponseModel BuildShareNotificationResponseModel(ShareNotification shareNotification)
    {
        ShareNotificationResponseModel shareNotificationResponseModel = new ShareNotificationResponseModel()
        {
            NotificationId = shareNotification.NotificationId,
            DateTime = shareNotification.DateTime,
            SourceUser = this._userResponseModelsFactory.BuildUserBasicInfoResponseModel(shareNotification.SourceUser),
            Message = shareNotification.Message,
            Type = shareNotification.Type,
            Seen = shareNotification.Seen,
            ShareId =  shareNotification.ShareId ??= 0
        };
        return shareNotificationResponseModel;
    }

    public ShareNotificationResponseModel BuildPostShareNotificationResponseModel(PostShareNotification shareNotification)
    {
        ShareNotificationResponseModel shareNotificationResponseModel = new ShareNotificationResponseModel()
        {
            NotificationId = shareNotification.NotificationId,
            DateTime = shareNotification.DateTime,
            SourceUser = this._userResponseModelsFactory.BuildUserBasicInfoResponseModel(shareNotification.SourceUser),
            Message = shareNotification.Message,
            Type = shareNotification.Type,
            Seen = shareNotification.Seen,
            PostId = shareNotification.PostId,
            ShareId = shareNotification.ShareId ??= 0
        };
        return shareNotificationResponseModel;
    }
    public  CommentNotificationResponseModel BuildCommentNotificationResponseModel(CommentNotification commentNotification)
    {
        CommentNotificationResponseModel commentNotificationResponseModel = new CommentNotificationResponseModel()
        {
            NotificationId = commentNotification.NotificationId,
            DateTime = commentNotification.DateTime,
            SourceUser = this._userResponseModelsFactory.BuildUserBasicInfoResponseModel(commentNotification.SourceUser),
            Message = commentNotification.Message,
            Type = commentNotification.Type,
            Seen = commentNotification.Seen,
            CommentId = commentNotification.CommentId  ??= 0

        };
        return commentNotificationResponseModel;

    }

    public CommentNotificationResponseModel BuildPostCommentNotificationResponseModel(PostCommentNotification commentNotification)
    {
        CommentNotificationResponseModel commentNotificationResponseModel = new CommentNotificationResponseModel()
        {
            NotificationId = commentNotification.NotificationId,
            DateTime = commentNotification.DateTime,
            SourceUser = this._userResponseModelsFactory.BuildUserBasicInfoResponseModel(commentNotification.SourceUser),
            Message = commentNotification.Message,
            Type = commentNotification.Type,
            Seen = commentNotification.Seen,
            PostId = commentNotification.PostId,
            CommentId = commentNotification.CommentId ??= 0

        };
        return commentNotificationResponseModel;

    }
    public CommentReactNotificationResponseModel BuildCommentReactNotificationResponseModel(CommentReactNotification commentReactNotification)
    {
        CommentReactNotificationResponseModel commentReactNotificationResponseModel = new CommentReactNotificationResponseModel()
        {
            NotificationId = commentReactNotification.NotificationId,
            DateTime = commentReactNotification.DateTime,
            SourceUser = this._userResponseModelsFactory.BuildUserBasicInfoResponseModel(commentReactNotification.SourceUser),
            Message = commentReactNotification.Message,
            Type = commentReactNotification.Type,
            Seen = commentReactNotification.Seen,
            CommentId = commentReactNotification.CommentId,
            CommentReactId = commentReactNotification.ReactId ??= 0
        };
        return commentReactNotificationResponseModel;

    }
    public CommentReactNotificationResponseModel BuildPostCommentReactNotificationResponseModel(PostCommentReactNotification commentReactNotification)
    {
        CommentReactNotificationResponseModel commentReactNotificationResponseModel = new CommentReactNotificationResponseModel()
        {
            NotificationId = commentReactNotification.NotificationId,
            DateTime = commentReactNotification.DateTime,
            SourceUser = this._userResponseModelsFactory.BuildUserBasicInfoResponseModel(commentReactNotification.SourceUser),
            Message = commentReactNotification.Message,
            Type = commentReactNotification.Type,
            Seen = commentReactNotification.Seen,
            CommentId = commentReactNotification.CommentId,
            CommentReactId = commentReactNotification.ReactId ??= 0,
            PostId = commentReactNotification.PostId
        };
        return commentReactNotificationResponseModel;

    }


}
