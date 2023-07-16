
namespace QuranHub.Domain.Models;

public class PlayListInfo : IEquatable<PlayListInfo>
{
    public int PlayListInfoId { get; set; }
    public byte[] Image {get; set;}
    public string Name { get; set; }
    public int NumberOfVideos { get; set; }

    List<VideoInfo> VideosData {get; set;}

   

     public override bool Equals(object obj)
    {
        if (obj == null) return false;
        PlayListInfo objAsPlayListInfo = obj as PlayListInfo;
        if (objAsPlayListInfo == null) return false;
        else return Equals(objAsPlayListInfo);
    }

    public override int GetHashCode()
    {
        return PlayListInfoId;
    }

    public bool Equals(PlayListInfo PlayListInfo)
    {
        if (PlayListInfo == null) return false;
        return PlayListInfoId.Equals(PlayListInfo.PlayListInfoId);
    }
}
