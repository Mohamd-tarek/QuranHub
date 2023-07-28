
namespace QuranHub.Domain.Repositories;

public interface IPostRepository 
{
    public Task<ShareablePost> CreatePostAsync(ShareablePost post);
    public Task<Post> GetPostByIdAsync(int postId);
    public Task<Post> GetPostByIdWithSpecificCommentAsync(int postId, int commentId);
    public Task<ShareablePost> GetShareablePostByIdAsync(int postId);
    public Task<SharedPost> GetSharedPostByIdAsync(int postId);
    public Task<List<ShareablePost>> GetShareablePostsByQuranHubUserIdAsync(string quranHubUserId);
    public Task<List<SharedPost>> GetSharedPostsByQuranHubUserIdAsync(string quranHubUserId);
    public Task<Verse> GetVerseAsync(int VerseId);
    public Task<List<PostReact>> GetPostReactsAsync(int postId);
    public Task<List<PostComment>> GetCommentsAsync(int postId);
    public Task<PostComment> GetCommentByIdAsync(int commentId);
    public Task<List<PostShare>> GetPostSharesAsync(int postId);
    public Task<PostShare> GetSharedPostShareAsync(int shareId);
    public Task<List<PostComment>> GetMoreCommentsAsync(int postId, int offset, int amount);
    public Task<List<PostCommentReact>> GetMoreCommentReactsAsync(int postId, int offset, int amount);
    public Task<List<PostReact>> GetMorePostReactsAsync(int postId, int offset, int amount);
    public Task<List<PostShare>> GetMoreSharesAsync(int postId, int offset, int amount);
    public Task<Post> EditPostAsync(Post post);
    public Task<bool> DeletePostAsync(int postId);
    public Task<Tuple<PostReact, PostReactNotification>> AddPostReactAsync(PostReact postReact, QuranHubUser user); 
    public Task<bool> RemovePostReactAsync(int postId, QuranHubUser user);
    public Task<Tuple<PostComment, PostCommentNotification>> AddCommentAsync(PostComment comment, QuranHubUser user);
    public Task<bool> RemoveCommentAsync(int commentId);
    public Task<Tuple<PostCommentReact, PostCommentReactNotification>> AddCommentReactAsync(PostCommentReact commentReact, QuranHubUser user); 
    public Task<bool> RemoveCommentReactAsync(int commentId, QuranHubUser user);
    public Task<Tuple<PostShare, PostShareNotification>> SharePostAsync(SharedPost sharedPost, QuranHubUser user);
    public  Task<List<ShareablePost>> SearchShareablePostsAsync(string keyword);
     public Task<List<SharedPost>> SearchSharedPostsAsync(string keyword);
    public Task<List<Verse>> GetVersesAsync();

}
