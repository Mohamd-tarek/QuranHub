
namespace QuranHub.Domain.Repositories;

public interface IDocumentaryRepository 
{
    public Task<List<PlayListInfo>> GetPlayListsAsync();
    public Task<PlayListInfo> GetPlayListByNameAsync(string name);
    public Task<List<VideoInfo>> GetVideoInfoForPlayListAsync(string playListName, int offset, int amount);
    public Task<VideoInfo> GetVideoInfoByNameAsync(string name);
    public Task<List<VideoInfoReact>> GetVideoInfoReactsAsync(int videoInfoId);
    public Task<List<VideoInfoComment>> GetVideoInfoCommentsAsync(int videoInfoId);
    public Task<VideoInfoComment> GetVideoInfoCommentByIdAsync(int commentId);
    public Task<List<VideoInfoComment>> GetMoreVideoInfoCommentsAsync(int videoInfoId, int offset, int amount);
    public Task<List<VideoInfoCommentReact>> GetMoreVideoInfoCommentReactsAsync(int videoInfoId, int offset, int amount);
    public Task<List<VideoInfoReact>> GetMoreVideoInfoReactsAsync(int videoInfoId, int offset, int amount);
    public Task<VideoInfoReact> AddVideoInfoReactAsync(VideoInfoReact VideoInfoReact, QuranHubUser user);
    public Task<bool> RemoveVideoInfoReactAsync(int VideoInfoId, QuranHubUser user);
    public Task<VideoInfoComment> AddVideoInfoCommentAsync(VideoInfoComment comment, QuranHubUser user);
    public Task<bool> RemoveVideoInfoCommentAsync(int commentId);
    public Task<Tuple<VideoInfoCommentReact, VideoInfoCommentReactNotification>> AddVideoInfoCommentReactAsync(VideoInfoCommentReact commentReact, QuranHubUser user);
    public Task<bool> RemoveVideoInfoCommentReactAsync(int commentId, QuranHubUser user);
    public Task<List<Verse>> GetVersesAsync();


}
