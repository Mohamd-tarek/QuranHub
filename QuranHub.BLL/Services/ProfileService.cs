using Microsoft.Extensions.Logging;
using QuranHub.Core.Dtos.Request;

namespace QuranHub.BLL.Services;

public class ProfileService : IProfileService 
{   
    private UserManager<QuranHubUser> _userManager;
    private IPostRepository _postRepository;
    private IFollowRepository _followRepository;
    ILogger<ProfileService> _logger;
    public  ProfileService(
        IPostRepository postRepository,
        IFollowRepository followRepository,
        UserManager<QuranHubUser> userManager,
        ILogger<ProfileService> logger)
    {
        _postRepository = postRepository;
        _followRepository = followRepository;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<List<ShareablePost>> GetUserShareablePostsAsync(string userId)
    {
        try
        {
            List<ShareablePost> Posts = await this._postRepository.GetShareablePostsByQuranHubUserIdAsync(userId);

            return Posts;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<List<SharedPost>> GetUserSharedPostsAsync(string userId)
    {
        try
        {
            List<SharedPost> SharedPosts = await this._postRepository.GetSharedPostsByQuranHubUserIdAsync(userId);

            return SharedPosts;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<List<QuranHubUser>> GetUserFollowersAsync(string userId)
    {
        try
        {
            List<Follow> follows = await this._followRepository.GetUserFollowersAsync(userId);

            List<QuranHubUser> followers =  this.GetUsersAsync(follows);
        
            return followers;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<List<QuranHubUser>> GetUserFollowingsAsync(string userId)
    {
        try
        {
            List<Follow> follows = await this._followRepository.GetUserFollowingsAsync(userId);

            List<QuranHubUser> followers =  this.GetUsersAsync(follows, true);

            return followers;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<List<QuranHubUser>> GetUserFollowersAsync(string userId, string KeyWord)
    {
        try
        {
            List<Follow> follows =  await this._followRepository.GetUserFollowersAsync(userId);

            List<QuranHubUser> followers = this.GetUsersAsync(follows);

             List<QuranHubUser> filteredFollowers = new List<QuranHubUser>();

             foreach ( var follower in followers)
             {
                    if (follower.UserName.Contains(KeyWord))
                    {
                         filteredFollowers.Add(follower);
                    }
             }
        
            return filteredFollowers;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<List<QuranHubUser>> GetUserFollowingsAsync(string userId, string KeyWord)
    {
        try
        {
            List<Follow> follows = await this._followRepository.GetUserFollowingsAsync(userId);

            List<QuranHubUser> followings =  this.GetUsersAsync(follows, true);

            List<QuranHubUser> filteredFollowings = new List<QuranHubUser>();

             foreach ( var following in followings)
             {
                    if (following.UserName.Contains(KeyWord))
                    {
                         filteredFollowings.Add(following);
                    }
             }
        
            return filteredFollowings;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    private List<QuranHubUser> GetUsersAsync(List<Follow> follows, bool followings = false)
    {
        try
        {
            List<QuranHubUser> followers = new List<QuranHubUser>();

            foreach (var follow in follows)
            {
                QuranHubUser follower = followings ? follow.Followed : follow.Follower ;
                followers.Add(follower);
            }  

            return followers;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }


    public async Task<byte[]> GetCoverPicture(string userId)
    {
        try
        {
            QuranHubUser user  = await _userManager.FindByIdAsync(userId);

            return user.CoverPicture;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<byte[]> EditCoverPictureAsync(byte[] coverPicture, QuranHubUser user)
    {
        try
        {
            user.CoverPicture = coverPicture;

            IdentityResult result =  await _userManager.UpdateAsync(user);  

            if (!result.Succeeded)
            {
                return null;
            }

           return user.CoverPicture;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async  Task<byte[]> GetProfilePicture(string userId)
    {
        try
        {
            QuranHubUser user  = await _userManager.FindByIdAsync(userId);

             return user.ProfilePicture;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

     public async Task<byte[]> EditProfilePictureAsync(byte[] profilePicture, QuranHubUser user)
     {
        try
        {
            user.ProfilePicture = profilePicture;

            IdentityResult result =  await _userManager.UpdateAsync(user);  

            if (!result.Succeeded)
            {
                return null;
            }

           return user.ProfilePicture;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<bool> CheckFollowingAsync(string followerId, string followedId)
    {
        try
        {
            return await this._followRepository.FollowExistAsync(followerId, followedId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }
    }

    public async Task<Tuple<bool, FollowNotification>> AddFollowAsync(FollowRequestModel follow, QuranHubUser user)
    {
        try
        {
             return await _followRepository.AddFollowAsync(follow.FollowerId, follow.FollowedId, user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<bool> RemoveFollowAsync(FollowRequestModel follow)
    {
        try
        {
                return await _followRepository.RemoveFollowAsync(follow.FollowerId, follow.FollowedId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }
    }

}