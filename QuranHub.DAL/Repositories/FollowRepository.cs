
using Microsoft.Extensions.Logging;

namespace QuranHub.DAL.Repositories;
/// <inheritdoc/>
public class FollowRepository : IFollowRepository
{
    private IdentityDataContext _identityDataContext;
    private readonly ILogger<FollowRepository> _logger;
    public FollowRepository(
        IdentityDataContext identityDataContext,
        ILogger<FollowRepository> logger)
    {
        _identityDataContext = identityDataContext;
        _logger = logger;
    }

    public async Task<Follow> GetFollowByIdAsync(int followId)
    {
        try
        {
            Follow follow = await this._identityDataContext.Follows.FindAsync(followId);

            return follow;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }


    public async Task<List<Follow>> GetUserFollowersAsync(string userId) 
    {
        try
        {
            List<Follow> follows = await this._identityDataContext.Follows
                                                              .Include(follow => follow.Follower)
                                                              .Include(follow => follow.Followed)
                                                              .Where(follow => follow.FollowedId == userId)
                                                              .ToListAsync();  
            return follows;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    

    public async Task<List<Follow>> GetUserFollowingsAsync(string userId)
    {
        try
        {
            List<Follow> follows = await this._identityDataContext.Follows
                                                              .Include(follow => follow.Follower)
                                                              .Include(follow => follow.Followed)
                                                              .Where(follow => follow.FollowerId == userId)
                                                              .ToListAsync();
            return follows;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }
    public async Task<List<QuranHubUser>> GetOrderedUserFollowedsAsync(string userId)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

     public async Task<Tuple<bool, FollowNotification>> AddFollowAsync(Follow follow, QuranHubUser user)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<bool> RemoveFollowAsync(Follow _follow)
    {
        try
        {
            Follow follow = await this._identityDataContext.Follows
                                                       .Where((follow) => follow.FollowerId ==  _follow.FollowerId && follow.FollowedId ==  _follow.FollowedId )
                                                       .FirstAsync();

            this._identityDataContext.Remove(follow); 

            await this._identityDataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }

    }

    public async Task<bool> FollowExistAsync(string followerId, string followedId)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }
    }  

}
