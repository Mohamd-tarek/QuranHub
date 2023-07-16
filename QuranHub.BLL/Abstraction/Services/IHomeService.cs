
namespace QuranHub.BLL.Abstraction;

public interface IHomeService 
{
    public Task<ShareablePost> CreatePostAsync(ShareablePost post);
    public Task<List<QuranHubUser>> GetUserFollowedsAsync(string userId);
    public Task<List<ShareablePost>> GetShareablePostsAsync(string userId);
    public Task<List<SharedPost>> GetSharedPostsAsync(string userId);
    public Task<List<QuranHubUser>> FindUsersByNameAsync(string name);
    public Task<List<ShareablePost>> SearchShareablePostsAsync(string keyword);
    public Task<List<SharedPost>> SearchSharedPostsAsync(string keyword);

}
