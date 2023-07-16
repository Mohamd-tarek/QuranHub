
namespace QuranHub.BLL.Abstraction;

public interface IProfileService 
{
    public Task<List<ShareablePost>> GetUserShareablePostsAsync(string userId);
    public Task<List<SharedPost>> GetUserSharedPostsAsync(string userId);
    public Task<List<QuranHubUser>> GetUserFollowersAsync(string userId); 
    public Task<List<QuranHubUser>> GetUserFollowingsAsync(string userId);
    public Task<List<QuranHubUser>> GetUserFollowersAsync(string userId, string KeyWord);
    public Task<List<QuranHubUser>> GetUserFollowingsAsync(string userId, string KeyWord);
    public Task<byte[]> GetCoverPicture(string userId);
    public Task<byte[]> EditCoverPictureAsync(byte[] coverPictureModel, QuranHubUser user);
    public Task<byte[]> GetProfilePicture(string userId);
    public Task<byte[]> EditProfilePictureAsync(byte[] coverPictureModel, QuranHubUser user);
    public Task<bool> CheckFollowingAsync(string followerId, string followedId);
    public Task<Tuple<bool, FollowNotification>> AddFollowAsync(Follow follow, QuranHubUser user);
    public Task<bool> RemoveFollowAsync(Follow follow); 

}
