
namespace QuranHub.Web.Services;

public interface IVideoInfoResponseModelsFactory 
{
    public Task<VideoInfoResponseModel> BuildVideoInfoResponseModelAsync(VideoInfo videoInfo);
    public VerseResponseModel BuildVerseResponseModel(Verse Verse);
    public List<ReactResponseModel> BuildVideoInfoReactsResponseModel(List<VideoInfoReact> videoInfoReacts);
    public Task<bool> CheckVideoInfoReactedToAsync(int VideoInfoId);
    public Task<List<CommentResponseModel>> BuildCommentsResponseModelAsync(List<VideoInfoComment> comments);
    public List<ReactResponseModel> BuildCommentReactsResponseModel(List<VideoInfoCommentReact> commentReacts);
    public ReactResponseModel BuildVideoInfoReactResponseModel(VideoInfoReact videoInfoReact);
    public Task<CommentResponseModel> BuildCommentResponseModelAsync(VideoInfoComment comment);
    public ReactResponseModel BuildCommentReactResponseModel(VideoInfoCommentReact commentReact);
    public Task<bool> CheckCommentReactedToAsync(int CommentId);

}
