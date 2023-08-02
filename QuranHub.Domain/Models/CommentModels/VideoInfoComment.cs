

namespace QuranHub.Domain.Models;

public class VideoInfoComment :Comment
{
    public int VideoInfoId { get; set; }
    public VideoInfo VideoInfo { get; set; }
    public List<VideoInfoCommentReact> VideoInfoCommentReacts { get; set; } = new();
    public List<VideoInfoCommentReactNotification> VideoInfoCommentReactNotifications { get; set; } = new();


    public VideoInfoComment():base()
    { }

    public VideoInfoComment(string quranHubUserId, int videoInfoId, string text, int? verseId):base(quranHubUserId, text, verseId)
    {
        VideoInfoId = videoInfoId;
    }

    public VideoInfoCommentReact AddVideoInfoCommentReact(string quranHubUserId, int type = 0)
    {

        var VideoInfoCommentReact = new VideoInfoCommentReact(quranHubUserId, CommentId, VideoInfoId, type);

        VideoInfoCommentReacts.Add(VideoInfoCommentReact);

        ReactsCount++;

        return VideoInfoCommentReact;
    }

    public VideoInfoCommentReactNotification AddVideoInfoCommentReactNotifiaction(QuranHubUser quranHubUser, int reactId)
    {
        string message = quranHubUser.UserName + " reacted to your comment "
           + "\"" + ( this.Text.Length < 40 ? this.Text : this.Text.Substring(0, 40 ) + "...") + "\"";

        var CommentReactNotification = new VideoInfoCommentReactNotification(quranHubUser.Id, this.QuranHubUserId, message, this.CommentId, reactId, this.VideoInfoId);

        VideoInfoCommentReactNotifications.Add(CommentReactNotification);
        return CommentReactNotification;
    }

    public void RemoveVideoInfoCommentReact(int VideoInfoCommentReactId)
    {
        VideoInfoCommentReacts.Remove(new VideoInfoCommentReact() {ReactId = VideoInfoCommentReactId });

        this.RemoveCommentReact(VideoInfoCommentReactId);

    }

}

