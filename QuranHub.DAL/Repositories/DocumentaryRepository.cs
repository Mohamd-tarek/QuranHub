
namespace QuranHub.DAL.Repositories;

public class DocumentaryRepository : IDocumentaryRepository 
{   
    private VideoContext _videoContext;

    public  DocumentaryRepository(VideoContext videoContext)
    { 
        _videoContext = videoContext;
    }

    public async Task<List<PlayListInfo>> GetPlayListsAsync()
    {
        return await this._videoContext.PlayListsInfo.ToListAsync();
    }
    public async Task<PlayListInfo> GetPlayListByNameAsync(string name)
    {
        return await this._videoContext.PlayListsInfo.Where(PlayListInfo => PlayListInfo.Name == name).FirstAsync();
    }

    public async Task<List<VideoInfo>> GetVideoInfoForPlayListAsync(string playListName, int offset, int amount)
    {
        var PlayListInfo = await this._videoContext.PlayListsInfo.Where(PlayListInfo => PlayListInfo.Name == playListName).FirstAsync();

        return await this._videoContext.VideosInfo.Where(VideoInfo => VideoInfo.PlayListInfoId == PlayListInfo.PlayListInfoId)
                                                  .Skip(offset)
                                                  .Take(amount)
                                                  .ToListAsync();
        
    }
    public async Task<VideoInfo> GetVideoInfoByNameAsync(string name)
    {
        return await this._videoContext.VideosInfo.Where(VideosInfo => VideosInfo.Name == name).FirstAsync();
    }
}
