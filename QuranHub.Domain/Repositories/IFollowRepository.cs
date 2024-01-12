
namespace QuranHub.Domain.Repositories;
/// <summary>
/// represent follow relationship repository
/// </summary>
public interface IFollowRepository 
{
    /// <summary>
    /// get follow realtionship by id
    /// </summary>
    /// <param name="followId"></param>
    /// <returns></returns>
    public Task<Follow> GetFollowByIdAsync(int followId);
    /// <summary>
    /// get user followers
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<List<Follow>> GetUserFollowersAsync(string userId); 
    /// <summary>
    /// get user followings
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<List<Follow>> GetUserFollowingsAsync(string userId);
    /// <summary>
    /// get user followeds by order
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public  Task<List<QuranHubUser>> GetOrderedUserFollowedsAsync(string userId); 
    /// <summary>
    /// add follow relationship
    /// </summary>
    /// <param name="follow"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<Tuple<bool, FollowNotification>> AddFollowAsync(Follow follow, QuranHubUser user);
    /// <summary>
    /// remove follow relationship
    /// </summary>
    /// <param name="follow"></param>
    /// <returns></returns>
    public Task<bool> RemoveFollowAsync(Follow follow); 
    /// <summary>
    /// check if follow relationship exist
    /// </summary>
    /// <param name="followerId"></param>
    /// <param name="followedId"></param>
    /// <returns></returns>
    public Task<bool> FollowExistAsync(string followerId, string followedId);

}
