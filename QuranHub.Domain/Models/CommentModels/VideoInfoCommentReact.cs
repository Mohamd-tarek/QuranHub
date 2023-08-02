
namespace QuranHub.Domain.Models;

public class VideoInfoCommentReact : CommentReact
{
    public int VideoInfoId { get; set; }
    public VideoInfo VideoInfo { get; set; }

    public VideoInfoCommentReactNotification VideoInfoCommentReactNotification { get; set; }

    public VideoInfoCommentReact(): base()
    {}
    public VideoInfoCommentReact(string quranHubUserId, int commentId, int videoInfoId, int type = 0):base(quranHubUserId, commentId, type)
    {
        VideoInfoId = videoInfoId;
    }

}

