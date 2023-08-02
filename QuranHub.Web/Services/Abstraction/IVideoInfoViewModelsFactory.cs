
namespace QuranHub.Web.Services;

public interface IVideoInfoViewModelsFactory 
{
    public Task<VideoInfoViewModel> BuildVideoInfoViewModelAsync(VideoInfo videoInfo);
    public VerseViewModel BuildVerseViewModel(Verse Verse);
    public List<ReactViewModel> BuildVideoInfoReactsViewModel(List<VideoInfoReact> videoInfoReacts);
    public Task<bool> CheckVideoInfoReactedToAsync(int VideoInfoId);
    public Task<List<CommentViewModel>> BuildCommentsViewModelAsync(List<VideoInfoComment> comments);
    public List<ReactViewModel> BuildCommentReactsViewModel(List<VideoInfoCommentReact> commentReacts);
    public ReactViewModel BuildVideoInfoReactViewModel(VideoInfoReact videoInfoReact);
    public Task<CommentViewModel> BuildCommentViewModelAsync(VideoInfoComment comment);
    public ReactViewModel BuildCommentReactViewModel(VideoInfoCommentReact commentReact);
    public Task<bool> CheckCommentReactedToAsync(int CommentId);

}
