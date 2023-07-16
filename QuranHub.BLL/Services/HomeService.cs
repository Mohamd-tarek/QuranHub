namespace QuranHub.BLL.Services;

public class HomeService : IHomeService
{   

    private UserManager<QuranHubUser> _userManager;
    private IPostRepository _postRepository;
    private IFollowRepository _followRepository;

   
    public  HomeService(
        UserManager<QuranHubUser> userManager,
        IFollowRepository followRepository,
        IPostRepository postRepository)
    {
        _userManager = userManager;
        _followRepository = followRepository;
        _postRepository = postRepository;  
    }

    public async Task<ShareablePost> CreatePostAsync(ShareablePost post)
    {
        return await this._postRepository.CreatePostAsync(post);
    }

    public async Task<List<QuranHubUser>> GetUserFollowedsAsync(string userId)
    {
        return await this._followRepository.GetOrderedUserFollowedsAsync(userId);
    }

    public async Task<List<ShareablePost>> GetShareablePostsAsync(string userId)
    {
        List<QuranHubUser> followed = await this.GetUserFollowedsAsync(userId);

        List<ShareablePost> posts = new List<ShareablePost>();

        foreach (var user in followed)
        {
            List<ShareablePost> followedPosts = await _postRepository.GetShareablePostsByQuranHubUserIdAsync(user.Id);
            posts.AddRange(followedPosts);
        }

        return posts;
    }
   
    public async Task<List<SharedPost>> GetSharedPostsAsync(string userId)
    {
        List<QuranHubUser> followed = await this.GetUserFollowedsAsync(userId);

        List<SharedPost> sharedPosts = new List<SharedPost>();

        foreach (var user in followed)
        {
            List<SharedPost> followedSharedPosts = await _postRepository.GetSharedPostsByQuranHubUserIdAsync(user.Id);
            sharedPosts.AddRange(followedSharedPosts);
        }

        return sharedPosts;
    }
   
    public async Task<List<QuranHubUser>> FindUsersByNameAsync(string name) 
    {
        List<QuranHubUser> users =  await _userManager.Users
                                                      .Where(user => user.UserName.Contains(name))
                                                      .ToListAsync();

        return users;
        
    }

    public async Task<List<ShareablePost>> SearchShareablePostsAsync(string keyword)
    {
        return await this._postRepository.SearchShareablePostsAsync(keyword);
    }

    public async Task<List<SharedPost>> SearchSharedPostsAsync(string keyword)
    {
        return await this._postRepository.SearchSharedPostsAsync(keyword);
    }
}
