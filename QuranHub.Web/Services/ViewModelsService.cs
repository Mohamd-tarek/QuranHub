
namespace QuranHub.Web.Services;

public class ViewModelsService : IViewModelsService
{
    private IPostViewModelsFactory _postViewModelsFactory;

    public ViewModelsService(
        IPostViewModelsFactory postViewModelsFactory)
    {
        _postViewModelsFactory =  postViewModelsFactory ?? throw new ArgumentNullException(nameof(postViewModelsFactory));
    }

    public async Task<List<object>> MergePostsAsync(List<ShareablePost> posts, List<SharedPost> sharedPosts)
    {
        List<object> mergedPosts = new List<object>();

        var firstPointer = 0;
        var secondPointer = 0;

        while (firstPointer < posts.Count && secondPointer < sharedPosts.Count)
        {
            if (posts[firstPointer].DateTime < sharedPosts[secondPointer].DateTime)
            {
                mergedPosts.Add(await this._postViewModelsFactory.BuildShareablePostViewModelAsync(posts[firstPointer]));
                firstPointer++;
            }
            else
            {
                mergedPosts.Add(await this._postViewModelsFactory.BuildSharedPostViewModelAsync(sharedPosts[secondPointer]));
                secondPointer++;
            }
        }

        while (firstPointer < posts.Count)
        {
            mergedPosts.Add(await this._postViewModelsFactory.BuildShareablePostViewModelAsync(posts[firstPointer]));
            firstPointer++;
        }

        while (secondPointer < sharedPosts.Count)
        {
            mergedPosts.Add(await this._postViewModelsFactory.BuildSharedPostViewModelAsync(sharedPosts[secondPointer]));
            secondPointer++;
        }

        return mergedPosts;
    }

    public byte[] ReadFileIntoArray(IFormFile formFile)
    {
        var stream = new MemoryStream();

        formFile.CopyTo(stream);

        return stream.ToArray();
    }

}
