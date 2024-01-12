
namespace QuranHub.BLL.Abstraction;
/// <summary>
/// represent home service logic 
/// </summary>
public interface IHomeService 
{
    /// <summary>
    /// create post
    /// </summary>
    /// <param name="post"></param>
    /// <returns></returns>
    public Task<ShareablePost> CreatePostAsync(ShareablePost post);
    /// <summary>
    /// get user followeds
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<List<QuranHubUser>> GetUserFollowedsAsync(string userId);
    /// <summary>
    /// get shareable post
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<List<ShareablePost>> GetShareablePostsAsync(string userId);
    /// <summary>
    /// get shared post
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<List<SharedPost>> GetSharedPostsAsync(string userId);
    /// <summary>
    /// find user by name 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task<List<QuranHubUser>> FindUsersByNameAsync(string name);
    /// <summary>
    /// search shareable post
    /// </summary>
    /// <param name="keyword"></param>
    /// <returns></returns>
    public Task<List<ShareablePost>> SearchShareablePostsAsync(string keyword);
    /// <summary>
    /// search shared post
    /// </summary>
    /// <param name="keyword"></param>
    /// <returns></returns>
    public Task<List<SharedPost>> SearchSharedPostsAsync(string keyword);

}
