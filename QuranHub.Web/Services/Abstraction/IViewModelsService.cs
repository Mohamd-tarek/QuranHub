
namespace QuranHub.Web.Services;

public interface IViewModelsService
{
     public Task<List<object>> MergePostsAsync(List<ShareablePost> posts, List<SharedPost> sharedPosts);
     public byte[] ReadFileIntoArray(IFormFile formFile);
   

}
