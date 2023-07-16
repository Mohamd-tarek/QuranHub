
namespace QuranHub.DAL.Repositories;

public class FollowRepository : IFollowRepository
{
    private IdentityDataContext _identityDataContext;
    public FollowRepository(IdentityDataContext identityDataContext)
    {
        _identityDataContext = identityDataContext;  
    }

    public async Task<Follow> GetFollowByIdAsync(int followId)
    {
         Follow follow = await this._identityDataContext.Follows.FindAsync(followId);

         return follow;
    }


    public async Task<List<Follow>> GetUserFollowersAsync(string userId) 
    {             
        List<Follow> follows = await this._identityDataContext.Follows
                                                              .Include(follow => follow.Follower)
                                                              .Include(follow => follow.Followed)
                                                              .Where(follow => follow.FollowedId == userId)
                                                              .ToListAsync();  
        return follows;
    }

    

    public async Task<List<Follow>> GetUserFollowingsAsync(string userId)
    {
        List<Follow> follows = await this._identityDataContext.Follows
                                                              .Include(follow => follow.Follower)
                                                              .Include(follow => follow.Followed)
                                                              .Where(follow => follow.FollowerId == userId)
                                                              .ToListAsync();


        return follows;
    }
    public async Task<List<QuranHubUser>> GetOrderedUserFollowedsAsync(string userId) 
    {             
        List<QuranHubUser> follows = await this._identityDataContext.Follows
                                                                    .Include(follow => follow.Followed)
                                                                    .Where(follow => follow.FollowerId == userId)
                                                                    .OrderBy(f => f.Comments)
                                                                    .ThenBy(f => f.Likes)
                                                                    .Select(f => f.Followed)
                                                                    .ToListAsync();  
        return follows;
    }

     public async Task<Tuple<bool, FollowNotification>> AddFollowAsync(Follow follow, QuranHubUser user) 
    {
        follow.DateTime = DateTime.Now;

        await this._identityDataContext.Follows.AddAsync(follow);

        await this._identityDataContext.SaveChangesAsync();

        string message = user.UserName + " started following you";

        FollowNotification followNotification = new FollowNotification(user.Id, follow.FollowedId, message, follow.FollowId);

        this._identityDataContext.FollowNotifications.Add(followNotification);

        await this._identityDataContext.SaveChangesAsync();

        if (this._identityDataContext.Follows.Contains(follow))
        {
              return new Tuple<bool, FollowNotification>(true, followNotification);
        }

        return new Tuple<bool, FollowNotification>(false, followNotification);
    }

    public async Task<bool> RemoveFollowAsync(Follow _follow) 
    {
        Follow follow = await this._identityDataContext.Follows
                                                       .Where((follow) => follow.FollowerId ==  _follow.FollowerId && follow.FollowedId ==  _follow.FollowedId )
                                                       .FirstAsync();

        this._identityDataContext.Remove(follow); 

        await this._identityDataContext.SaveChangesAsync();

        return true;

    }

    public async Task<bool> FollowExistAsync(string followerId, string followedId)
    {
        Follow follow = await this._identityDataContext.Follows
                                                       .Where(follow => follow.FollowerId == followerId && follow.FollowedId == followedId)
                                                       .FirstAsync();

        if (follow == null)
        {
            return false;
        }

        return true;   
    }  

}
