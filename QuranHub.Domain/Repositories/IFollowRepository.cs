
namespace QuranHub.Domain.Repositories;

public interface IFollowRepository 
{
    public Task<Follow> GetFollowByIdAsync(int followId);
    public Task<List<Follow>> GetUserFollowersAsync(string userId); 
    public Task<List<Follow>> GetUserFollowingsAsync(string userId);
    public  Task<List<QuranHubUser>> GetOrderedUserFollowedsAsync(string userId); 
    public Task<Tuple<bool, FollowNotification>> AddFollowAsync(Follow follow, QuranHubUser user);
    public Task<bool> RemoveFollowAsync(Follow follow); 
    public Task<bool> FollowExistAsync(string followerId, string followedId);

}
