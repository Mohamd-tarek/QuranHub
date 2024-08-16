
namespace QuranHub.Web.Services;

public interface IPostResponseModelsFactory 
{
    public Task<PostResponseModel> BuildPostResponseModelAsync(Post post);
    public Task<ShareablePostResponseModel> BuildShareablePostResponseModelAsync(ShareablePost post);
    public Task<SharedPostResponseModel> BuildSharedPostResponseModelAsync(SharedPost SharedPost);
    public VerseResponseModel BuildVerseResponseModel(Verse Verse);
    public List<ReactResponseModel> BuildPostReactsResponseModel(List<PostReact> postReacts);
    public Task<bool> CheckPostReactedToAsync(int PostId);
    public Task<List<CommentResponseModel>> BuildCommentsResponseModelAsync(List<PostComment> comments);
    public List<ReactResponseModel> BuildCommentReactsResponseModel(List<PostCommentReact> commentReacts);
    public List<PostShareResponseModel> BuildSharesResponseModel(List<PostShare> shares);
    public ReactResponseModel BuildPostReactResponseModel(PostReact postReact);
    public Task<CommentResponseModel> BuildCommentResponseModelAsync(PostComment comment);
    public ReactResponseModel BuildCommentReactResponseModel(PostCommentReact commentReact);
    public Task<bool> CheckCommentReactedToAsync(int CommentId);
    public PostShareResponseModel BuildShareResponseModel(PostShare share);
    public Task<PostShareResponseModel> BuildSharedPostShareResponseModelAsync(PostShare share);

}
