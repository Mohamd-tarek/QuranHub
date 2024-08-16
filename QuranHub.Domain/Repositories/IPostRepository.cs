
namespace QuranHub.Domain.Repositories;
/// <summary>
/// represent post repository
/// </summary>
public interface IPostRepository 
{
    /// <summary>
    /// create a post
    /// </summary>
    /// <param name="post"></param>
    /// <returns></returns>
    public Task<ShareablePost> CreatePostAsync(PostPrivacy privacy, string text, int verseId, string quranHubUserId);
    /// <summary>
    /// get post by id 
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    public Task<Post> GetPostByIdAsync(int postId);
    /// <summary>
    /// get post with specific comment
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="commentId"></param>
    /// <returns></returns>
    public Task<Post> GetPostByIdWithSpecificCommentAsync(int postId, int commentId);
    /// <summary>
    /// get shareabke post by Id
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    public Task<ShareablePost> GetShareablePostByIdAsync(int postId);
    /// <summary>
    /// get shared post by id 
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    public Task<SharedPost> GetSharedPostByIdAsync(int postId);
    /// <summary>
    /// get shareable post for specific user
    /// </summary>
    /// <param name="quranHubUserId"></param>
    /// <returns></returns>
    public Task<List<ShareablePost>> GetShareablePostsByQuranHubUserIdAsync(string quranHubUserId);
    /// <summary>
    /// get shared post for specific user
    /// </summary>
    /// <param name="quranHubUserId"></param>
    /// <returns></returns>
    public Task<List<SharedPost>> GetSharedPostsByQuranHubUserIdAsync(string quranHubUserId);
    /// <summary>
    /// get verse by id 
    /// </summary>
    /// <param name="VerseId"></param>
    /// <returns></returns>
    public Task<Verse> GetVerseAsync(int VerseId);
    /// <summary>
    /// get post reacts
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    public Task<List<PostReact>> GetPostReactsAsync(int postId);
    /// <summary>
    /// get post comments
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    public Task<List<PostComment>> GetPostCommentsAsync(int postId);
    /// <summary>
    /// get post comment by id 
    /// </summary>
    /// <param name="commentId"></param>
    /// <returns></returns>
    public Task<PostComment> GetPostCommentByIdAsync(int commentId);
    /// <summary>
    /// get post shares
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    public Task<List<PostShare>> GetPostSharesAsync(int postId);
    /// <summary>
    /// get shared post's share
    /// </summary>
    /// <param name="shareId"></param>
    /// <returns></returns>
    public Task<PostShare> GetSharedPostShareAsync(int shareId);
    /// <summary>
    /// get more post comments
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="offset"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public Task<List<PostComment>> GetMorePostCommentsAsync(int postId, int offset, int amount);
    /// <summary>
    /// get more post comment reacts
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="offset"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public Task<List<PostCommentReact>> GetMorePostCommentReactsAsync(int postId, int offset, int amount);
    /// <summary>
    /// get more post reacts
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="offset"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public Task<List<PostReact>> GetMorePostReactsAsync(int postId, int offset, int amount);
    /// <summary>
    /// get more post shares
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="offset"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public Task<List<PostShare>> GetMorePostSharesAsync(int postId, int offset, int amount);
    /// <summary>
    /// edit post 
    /// </summary>
    /// <param name="post"></param>
    /// <returns></returns>
    public Task<Post> EditPostAsync(int postId, PostPrivacy privacy, string text, int verseId);
    /// <summary>
    /// delete post
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    public Task<bool> DeletePostAsync(int postId);
    /// <summary>
    /// add post react
    /// </summary>
    /// <param name="postReact"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<Tuple<PostReact, PostReactNotification>> AddPostReactAsync(int  postId, QuranHubUser user);
    /// <summary>
    /// remove post react
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<bool> RemovePostReactAsync(int postId, QuranHubUser user);
    /// <summary>
    /// add post comment
    /// </summary>
    /// <param name="comment"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<Tuple<PostComment, PostCommentNotification>> AddPostCommentAsync(int postId,string text, int? verseId, QuranHubUser user);
    /// <summary>
    /// remove post comment
    /// </summary>
    /// <param name="commentId"></param>
    /// <returns></returns>
    public Task<bool> RemovePostCommentAsync(int commentId);
    /// <summary>
    /// add post comment react
    /// </summary>
    /// <param name="commentReact"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<Tuple<PostCommentReact, PostCommentReactNotification>> AddPostCommentReactAsync(int commentId, QuranHubUser user); 
    /// <summary>
    /// remove post comment react
    /// </summary>
    /// <param name="commentId"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<bool> RemovePostCommentReactAsync(int commentId, QuranHubUser user);
    /// <summary>
    /// share post
    /// </summary>
    /// <param name="sharedPost"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<Tuple<PostShare, PostShareNotification>> SharePostAsync(PostPrivacy privacy, int postId, string text, int? verseId, QuranHubUser user);
    /// <summary>
    /// search shareable post 
    /// </summary>
    /// <param name="keyword"></param>
    /// <returns></returns>
    public  Task<List<ShareablePost>> SearchShareablePostsAsync(string keyword);
    /// <summary>
    /// search shared post
    /// </summary>
    /// <param name="keyword"></param>
    /// <returns></returns>
    public Task<List<SharedPost>> SearchSharedPostsAsync(string keyword);
    /// <summary>
    /// get verses
    /// </summary>
    /// <returns></returns>
    public Task<List<Verse>> GetVersesAsync();

}
