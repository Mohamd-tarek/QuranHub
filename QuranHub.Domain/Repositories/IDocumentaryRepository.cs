
namespace QuranHub.Domain.Repositories;

public interface IDocumentaryRepository 
{
    public Task<List<PlayListInfo>> GetPlayListsAsync();
    public Task<PlayListInfo> GetPlayListByNameAsync(string name);
    public Task<List<VideoInfo>> GetVideoInfoForPlayListAsync(string playListName, int offset, int amount);
    public Task<VideoInfo> GetVideoInfoByNameAsync(string name);

}
