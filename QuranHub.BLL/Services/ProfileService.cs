namespace QuranHub.BLL.Services;

public class ProfileService : IProfileService 
{   
    private UserManager<QuranHubUser> _userManager;
    private IPostRepository _postRepository;
    private IFollowRepository _followRepository;

    public  ProfileService(
        IPostRepository postRepository,
        IFollowRepository followRepository,
        UserManager<QuranHubUser> userManager)
    {
        _postRepository = postRepository;
        _followRepository = followRepository;
        _userManager = userManager;
    }

    public async Task<List<ShareablePost>> GetUserShareablePostsAsync(string userId) 
    {
        List<ShareablePost> Posts = await this._postRepository.GetShareablePostsByQuranHubUserIdAsync(userId);

        return Posts;

    }

    public async Task<List<SharedPost>> GetUserSharedPostsAsync(string userId) 
    {
        List<SharedPost> SharedPosts = await this._postRepository.GetSharedPostsByQuranHubUserIdAsync(userId);

        return SharedPosts;
    }

    public async Task<List<QuranHubUser>> GetUserFollowersAsync(string userId) 
    {             
        List<Follow> follows = await this._followRepository.GetUserFollowersAsync(userId);

        List<QuranHubUser> followers =  this.GetUsersAsync(follows);
        
        return followers;
    }

    public async Task<List<QuranHubUser>> GetUserFollowingsAsync(string userId)
    {
        List<Follow> follows = await this._followRepository.GetUserFollowingsAsync(userId);

        List<QuranHubUser> followers =  this.GetUsersAsync(follows, true);

        return followers;
    }

    public async Task<List<QuranHubUser>> GetUserFollowersAsync(string userId, string KeyWord) 
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

    public async Task<List<QuranHubUser>> GetUserFollowingsAsync(string userId, string KeyWord)
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

    private List<QuranHubUser> GetUsersAsync(List<Follow> follows, bool followings = false)
    {
        List<QuranHubUser> followers = new List<QuranHubUser>();

        foreach (var follow in follows)
        {
            QuranHubUser follower = followings ? follow.Followed : follow.Follower ;
            followers.Add(follower);
        }  

        return followers;
    }


    public async Task<byte[]> GetCoverPicture(string userId)
    {
        QuranHubUser user  = await _userManager.FindByIdAsync(userId);

        return user.CoverPicture;  
    }

    public async Task<byte[]> EditCoverPictureAsync(byte[] coverPicture, QuranHubUser user) 
    {
        user.CoverPicture = coverPicture;

        IdentityResult result =  await _userManager.UpdateAsync(user);  

        if (!result.Succeeded)
        {
            return null;
        }

       return user.CoverPicture;
    }

    public async  Task<byte[]> GetProfilePicture(string userId)
    {
         QuranHubUser user  = await _userManager.FindByIdAsync(userId);

         return user.ProfilePicture;  
    }

     public async Task<byte[]> EditProfilePictureAsync(byte[] profilePicture, QuranHubUser user) 
    {
        user.ProfilePicture = profilePicture;

        IdentityResult result =  await _userManager.UpdateAsync(user);  

        if (!result.Succeeded)
        {
            return null;
        }

       return user.ProfilePicture;
    }

    public async Task<bool> CheckFollowingAsync(string followerId, string followedId)
    {
        return await this._followRepository.FollowExistAsync(followedId, followerId);
    }

    public async Task<Tuple<bool, FollowNotification>> AddFollowAsync(Follow follow, QuranHubUser user) 
    {
        return await _followRepository.AddFollowAsync(follow, user);
    }

    public async Task<bool> RemoveFollowAsync(Follow follow) 
    {
        return await _followRepository.RemoveFollowAsync(follow);
    }

}