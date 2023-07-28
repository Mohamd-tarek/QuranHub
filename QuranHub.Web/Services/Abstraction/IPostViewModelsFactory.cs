
namespace QuranHub.Web.Services;

public interface IPostViewModelsFactory 
{
    public Task<PostViewModel> BuildPostViewModelAsync(Post post);
    public Task<ShareablePostViewModel> BuildShareablePostViewModelAsync(ShareablePost post);
    public Task<SharedPostViewModel> BuildSharedPostViewModelAsync(SharedPost SharedPost);
    public VerseViewModel BuildVerseViewModel(Verse Verse);
    public List<PostReactViewModel> BuildPostReactsViewModel(List<PostReact> postReacts);
    public Task<bool> CheckPostReactedToAsync(int PostId);
    public Task<List<CommentViewModel>> BuildCommentsViewModelAsync(List<PostComment> comments);
    public List<CommentReactViewModel> BuildCommentReactsViewModel(List<PostCommentReact> commentReacts);
    public List<ShareViewModel> BuildSharesViewModel(List<PostShare> shares);
    public PostReactViewModel BuildPostReactViewModel(PostReact postReact);
    public Task<CommentViewModel> BuildCommentViewModelAsync(PostComment comment);
    public CommentReactViewModel BuildCommentReactViewModel(PostCommentReact commentReact);
    public Task<bool> CheckCommentReactedToAsync(int CommentId);
    public ShareViewModel BuildShareViewModel(PostShare share);
    public Task<ShareViewModel> BuildSharedPostShareViewModelAsync(PostShare share);

}
