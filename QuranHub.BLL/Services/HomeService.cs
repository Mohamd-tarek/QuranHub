using FFmpeg.AutoGen;
using Microsoft.Extensions.Logging;
using QuranHub.Core.Dtos.Request;

namespace QuranHub.BLL.Services;

public class HomeService : IHomeService
{   
    private UserManager<QuranHubUser> _userManager;
    private IPostRepository _postRepository;
    private IFollowRepository _followRepository;
    private ILogger<HomeService> _logger;

    public  HomeService(
        UserManager<QuranHubUser> userManager,
        IFollowRepository followRepository,
        IPostRepository postRepository,
        ILogger<HomeService> logger)
    {
        _userManager = userManager;
        _followRepository = followRepository;
        _postRepository = postRepository;  
        _logger = logger;
    }

    public async Task<ShareablePost> CreatePostAsync(AddPostRequestModel addPost)
    {
        try
        {
            return await this._postRepository.CreatePostAsync(addPost.Privacy, addPost.Text, addPost.VerseId, addPost.QuranHubUserId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<List<QuranHubUser>> GetUserFollowedsAsync(string userId)
    {
        try
        {
            return await this._followRepository.GetOrderedUserFollowedsAsync(userId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<List<ShareablePost>> GetShareablePostsAsync(string userId)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }
   
    public async Task<List<SharedPost>> GetSharedPostsAsync(string userId)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }
   
    public async Task<List<QuranHubUser>> FindUsersByNameAsync(string name)
    {
        try
        {
            List<QuranHubUser> users =  await _userManager.Users
                                                          .Where(user => user.UserName.Contains(name))
                                                          .ToListAsync();

            return users;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
        
    }

    public async Task<List<ShareablePost>> SearchShareablePostsAsync(string keyword)
    {
        try
        {
            return await this._postRepository.SearchShareablePostsAsync(keyword);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<List<SharedPost>> SearchSharedPostsAsync(string keyword)
    {
        try
        {
            return await this._postRepository.SearchSharedPostsAsync(keyword);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }
}
