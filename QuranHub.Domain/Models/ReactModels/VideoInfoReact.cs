
namespace QuranHub.Domain.Models;

public class VideoInfoReact : React
{
    public int VideoInfoId { get; set; }
    public VideoInfo VideoInfo { get; set; }

    public VideoInfoReact():base()
    {}
    public VideoInfoReact(string quranHubUserId, int videoInfoId, int type = 0):base(quranHubUserId, type)
    {
        VideoInfoId = videoInfoId;
    }

}
