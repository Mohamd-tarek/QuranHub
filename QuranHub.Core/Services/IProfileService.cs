
using QuranHub.Core.Dtos.Request;

namespace QuranHub.Core;
/// <summary>
/// represent profile service logic
/// </summary>
public interface IProfileService 
{
    /// <summary>
    /// get user shareable posts
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<List<ShareablePost>> GetUserShareablePostsAsync(string userId);
    /// <summary>
    /// get user shared posts 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<List<SharedPost>> GetUserSharedPostsAsync(string userId);
    /// <summary>
    /// get user followers
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<List<QuranHubUser>> GetUserFollowersAsync(string userId); 
    /// <summary>
    /// get user followings
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<List<QuranHubUser>> GetUserFollowingsAsync(string userId);
    /// <summary>
    /// get user followers by keyword
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="KeyWord"></param>
    /// <returns></returns>
    public Task<List<QuranHubUser>> GetUserFollowersAsync(string userId, string KeyWord);
    /// <summary>
    /// get user followings by key word
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="KeyWord"></param>
    /// <returns></returns>
    public Task<List<QuranHubUser>> GetUserFollowingsAsync(string userId, string KeyWord);
    /// <summary>
    /// get cover picture
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<byte[]> GetCoverPicture(string userId);
    /// <summary>
    /// edit cover picture
    /// </summary>
    /// <param name="coverPictureModel"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<byte[]> EditCoverPictureAsync(byte[] coverPictureModel, QuranHubUser user);
    /// <summary>
    /// get profile picture 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<byte[]> GetProfilePicture(string userId);
    /// <summary>
    /// edit profile picture 
    /// </summary>
    /// <param name="coverPictureModel"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<byte[]> EditProfilePictureAsync(byte[] coverPictureModel, QuranHubUser user);
    /// <summary>
    /// check followings
    /// </summary>
    /// <param name="followerId"></param>
    /// <param name="followedId"></param>
    /// <returns></returns>
    public Task<bool> CheckFollowingAsync(string followerId, string followedId);
    /// <summary>
    /// add follow 
    /// </summary>
    /// <param name="follow"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<Tuple<bool, FollowNotification>> AddFollowAsync(FollowRequestModel follow, QuranHubUser user);
    /// <summary>
    /// remove follow 
    /// </summary>
    /// <param name="follow"></param>
    /// <returns></returns>
    public Task<bool> RemoveFollowAsync(FollowRequestModel follow); 

}
