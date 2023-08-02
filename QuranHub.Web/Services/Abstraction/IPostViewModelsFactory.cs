
namespace QuranHub.Web.Services;

public interface IPostViewModelsFactory 
{
    public Task<PostViewModel> BuildPostViewModelAsync(Post post);
    public Task<ShareablePostViewModel> BuildShareablePostViewModelAsync(ShareablePost post);
    public Task<SharedPostViewModel> BuildSharedPostViewModelAsync(SharedPost SharedPost);
    public VerseViewModel BuildVerseViewModel(Verse Verse);
    public List<ReactViewModel> BuildPostReactsViewModel(List<PostReact> postReacts);
    public Task<bool> CheckPostReactedToAsync(int PostId);
    public Task<List<CommentViewModel>> BuildCommentsViewModelAsync(List<PostComment> comments);
    public List<ReactViewModel> BuildCommentReactsViewModel(List<PostCommentReact> commentReacts);
    public List<PostShareViewModel> BuildSharesViewModel(List<PostShare> shares);
    public ReactViewModel BuildPostReactViewModel(PostReact postReact);
    public Task<CommentViewModel> BuildCommentViewModelAsync(PostComment comment);
    public ReactViewModel BuildCommentReactViewModel(PostCommentReact commentReact);
    public Task<bool> CheckCommentReactedToAsync(int CommentId);
    public PostShareViewModel BuildShareViewModel(PostShare share);
    public Task<PostShareViewModel> BuildSharedPostShareViewModelAsync(PostShare share);

}
