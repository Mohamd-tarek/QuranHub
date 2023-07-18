
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
