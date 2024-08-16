

namespace QuranHub.Core.Dtos.Response;

public class VideoInfoResponseModel : Response
{
    public int VideoInfoId { get; set; }
    public byte[] ThumbnailImage { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public TimeSpan Duration { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public string Path { get; set; }
    public int Views { get; set; }
    public int PlayListInfoId { get; set; }
    public PlayListInfo PlayListInfo { get; set; }
    public bool ReactedTo {get; set;}
    public int ReactsCount {get; set;}
    public int CommentsCount { get; set;}
    public IEnumerable<CommentResponseModel> Comments { get; set; }
}
