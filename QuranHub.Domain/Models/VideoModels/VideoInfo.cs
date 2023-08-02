
namespace QuranHub.Domain.Models;

public class VideoInfo : IEquatable<VideoInfo>
{
    public int VideoInfoId { get; set; }
    public byte[] ThumbnailImage  {get; set;}
    public string Name { get; set; }
    public string Type { get; set; }
    public TimeSpan Duration { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public string Path { get; set; }
    public int Views { get; set; }
    public int PlayListInfoId { get; set; }
    public PlayListInfo PlayListInfo { get; set; }

    public int ReactsCount { get; set; }
    public int CommentsCount { get; set; }
    public List<VideoInfoReact> VideoInfoReacts { get; set; } = new();
    public List<VideoInfoCommentReactNotification> VideoInfoCommentReactNotifications { get; set; } = new();
    public List<VideoInfoComment> VideoInfoComments { get; set; } = new();

    public VideoInfoReact AddVideoInfoReact(string quranHubUserId, int type = 0)
    {
        var React = new VideoInfoReact(quranHubUserId, VideoInfoId, type);

        VideoInfoReacts.Add(React);

        ReactsCount++;

        return React;
    }

   

    public void RemoveVideoInfoReact(int VideoInfoReactId)
    {
        VideoInfoReacts.Remove(new VideoInfoReact() { ReactId = VideoInfoReactId });

        ReactsCount--;
    }

    public VideoInfoComment AddVideoInfoComment(string quranHubUserId, string text, int? verseId)
    {
        var Comment = new VideoInfoComment(quranHubUserId, VideoInfoId, text, verseId);

        VideoInfoComments.Add(Comment);

        CommentsCount++;

        return Comment;
    }
   

    public void RemoveVideoInfoComment(int CommentId)
    {
        VideoInfoComments.Remove(new VideoInfoComment() { CommentId = CommentId });

        CommentsCount--;
    }


    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        VideoInfo objAsVideoInfo = obj as VideoInfo;
        if (objAsVideoInfo == null) return false;
        else return Equals(objAsVideoInfo);
    }

    public override int GetHashCode()
    {
        return VideoInfoId;
    }

    public bool Equals(VideoInfo VideoInfo)
    {
        if (VideoInfo == null) return false;
        return VideoInfoId.Equals(VideoInfo.VideoInfoId);
    }
}
