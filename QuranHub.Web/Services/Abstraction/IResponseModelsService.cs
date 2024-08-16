
namespace QuranHub.Web.Services;

public interface IResponseModelsService
{
     public Task<List<object>> MergePostsAsync(List<ShareablePost> posts, List<SharedPost> sharedPosts);
     public byte[] ReadFileIntoArray(IFormFile formFile);
   

}
