
namespace QuranHub.Domain.Repositories;
/// <summary>
/// represent documentary repository 
/// </summary>
public interface IDocumentaryRepository 
{
    /// <summary>
    /// used to get play list info 
    /// </summary>
    /// <returns> list of paly list info </returns>
    public Task<List<PlayListInfo>> GetPlayListsAsync();
    /// <summary>
    /// used to get paly list by name 
    /// </summary>
    /// <param name="name">nae of the required palylist </param>
    /// <returns>play list </returns>
    public Task<PlayListInfo> GetPlayListByNameAsync(string name);
    /// <summary>
    /// used to get video info for a play list
    /// </summary>
    /// <param name="playListName"> name of the play list </param>
    /// <param name="offset">number of video info offset</param>
    /// <param name="amount"> required amount</param>
    /// <returns>list of video info for play list</returns>
    public Task<List<VideoInfo>> GetVideoInfoForPlayListAsync(string playListName, int offset, int amount);
    /// <summary>
    /// used to get video info by name 
    /// </summary>
    /// <param name="name">name of video</param>
    /// <returns> video info </returns>
    public Task<VideoInfo> GetVideoInfoByNameAsync(string name);
    /// <summary>
    /// used to get video reacts
    /// </summary>
    /// <param name="videoInfoId"> video info id</param>
    /// <returns>list of video reacts</returns>
    public Task<List<VideoInfoReact>> GetVideoInfoReactsAsync(int videoInfoId);
    /// <summary>
    /// used to get video comments
    /// </summary>
    /// <param name="videoInfoId"> video info id</param>
    /// <returns>list of video comments</returns>
    public Task<List<VideoInfoComment>> GetVideoInfoCommentsAsync(int videoInfoId);
    /// <summary>
    /// used to get video comment
    /// </summary>
    /// <param name="commentId"> comment Id</param>
    /// <returns>video comment</returns>
    public Task<VideoInfoComment> GetVideoInfoCommentByIdAsync(int commentId);
    /// <summary>
    /// used to get more video comments
    /// </summary>
    /// <param name="videoInfoId">video info id</param>
    /// <param name="offset">offset</param>
    /// <param name="amount">amount</param>
    /// <returns>list of video comment</returns>
    public Task<List<VideoInfoComment>> GetMoreVideoInfoCommentsAsync(int videoInfoId, int offset, int amount);
    /// <summary>
    /// used to get more video comment reacts
    /// </summary>
    /// <param name="videoInfoId">video info id</param>
    /// <param name="offset">offset</param>
    /// <param name="amount">amount</param>
    /// <returns>list of video comment reacts</returns>
    public Task<List<VideoInfoCommentReact>> GetMoreVideoInfoCommentReactsAsync(int videoInfoId, int offset, int amount);
    /// <summary>
    /// used to get video reacts
    /// </summary>
    /// <param name="videoInfoId">video info id</param>
    /// <param name="offset">offset</param>
    /// <param name="amount">amount</param>
    /// <returns>list of video  reacts</returns>
    public Task<List<VideoInfoReact>> GetMoreVideoInfoReactsAsync(int videoInfoId, int offset, int amount);
    /// <summary>
    /// used to add video react
    /// </summary>
    /// <param name="VideoInfoReact">video info react</param>
    /// <param name="user">user</param>
    /// <returns>inserted video react</returns>
    public Task<VideoInfoReact> AddVideoInfoReactAsync(VideoInfoReact VideoInfoReact, QuranHubUser user);
    /// <summary>
    /// used to remove video react
    /// </summary>
    /// <param name="VideoInfoId"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<bool> RemoveVideoInfoReactAsync(int VideoInfoId, QuranHubUser user);
    /// <summary>
    /// used to add video comment
    /// </summary>
    /// <param name="comment"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<VideoInfoComment> AddVideoInfoCommentAsync(VideoInfoComment comment, QuranHubUser user);
    /// <summary>
    /// used to remove video comment
    /// </summary>
    /// <param name="commentId"></param>
    /// <returns></returns>
    public Task<bool> RemoveVideoInfoCommentAsync(int commentId);
    /// <summary>
    /// used to add video comment react
    /// </summary>
    /// <param name="commentReact"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<Tuple<VideoInfoCommentReact, VideoInfoCommentReactNotification>> AddVideoInfoCommentReactAsync(VideoInfoCommentReact commentReact, QuranHubUser user);
    /// <summary>
    /// used to remove video comment react
    /// </summary>
    /// <param name="commentId"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<bool> RemoveVideoInfoCommentReactAsync(int commentId, QuranHubUser user);
    /// <summary>
    /// used to get verses 
    /// </summary>
    /// <returns></returns>
    public Task<List<Verse>> GetVersesAsync();


}
