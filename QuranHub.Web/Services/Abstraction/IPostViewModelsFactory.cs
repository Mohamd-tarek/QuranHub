
namespace QuranHub.Web.Services;

public interface IPostViewModelsFactory 
{
    public Task<PostViewModel> BuildPostViewModelAsync(Post post);
    public Task<ShareablePostViewModel> BuildShareablePostViewModelAsync(ShareablePost post);
    public Task<SharedPostViewModel> BuildSharedPostViewModelAsync(SharedPost SharedPost);
    public VerseViewModel BuildVerseViewModel(Verse Verse);
    public List<PostReactViewModel> BuildPostReactsViewModel(List<PostReact> postReacts);
    public Task<bool> CheckPostReactedToAsync(int PostId);
    public Task<List<CommentViewModel>> BuildCommentsViewModelAsync(List<Comment> comments);
    public List<CommentReactViewModel> BuildCommentReactsViewModel(List<CommentReact> commentReacts);
    public List<ShareViewModel> BuildSharesViewModel(List<Share> shares);
    public PostReactViewModel BuildPostReactViewModel(PostReact postReact);
    public Task<CommentViewModel> BuildCommentViewModelAsync(Comment comment);
    public CommentReactViewModel BuildCommentReactViewModel(CommentReact commentReact);
    public Task<bool> CheckCommentReactedToAsync(int CommentId);
    public ShareViewModel BuildShareViewModel(Share share);
    public Task<ShareViewModel> BuildSharedPostShareViewModelAsync(Share share);

}
