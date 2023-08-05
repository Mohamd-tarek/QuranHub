
namespace QuranHub.Domain.Models;

public class VideoInfoCommentReactNotification : CommentReactNotification
{
    public int VideoInfoId { get; set; }
    public VideoInfo VideoInfo { get; set; }
    public int VideoInfoCommentReactId { get; set; }
    public VideoInfoCommentReact VideoInfoCommentReact { get; set; }
    public VideoInfoCommentReactNotification():base()
    { }
    public VideoInfoCommentReactNotification(string sourceUserId, string targetUserId, string message, int commentId, int commentReactId, int videoInfoId)
    : base(sourceUserId, targetUserId, message, commentId, commentReactId)
    {
        Type = "VideoInfoCommentReactNotification";
        VideoInfoId = videoInfoId;
        VideoInfoCommentReactId = commentReactId;
    }
}
