
using QuranHub.Domain.Models;

namespace QuranHub.DAL.Repositories;

public class DocumentaryRepository : IDocumentaryRepository 
{   
    private IdentityDataContext _identityDataContext;

    public  DocumentaryRepository(IdentityDataContext IdentityDataContext)
    { 
        _identityDataContext = IdentityDataContext;
    }

    public async Task<List<PlayListInfo>> GetPlayListsAsync()
    {
        return await this._identityDataContext.PlayListsInfo.ToListAsync();
    }
    public async Task<PlayListInfo> GetPlayListByNameAsync(string name)
    {
        return await this._identityDataContext.PlayListsInfo.Where(PlayListInfo => PlayListInfo.Name == name).FirstAsync();
    }

    public async Task<List<VideoInfo>> GetVideoInfoForPlayListAsync(string playListName, int offset, int amount)
    {
        var PlayListInfo = await this._identityDataContext.PlayListsInfo.Where(PlayListInfo => PlayListInfo.Name == playListName).FirstAsync();

        return await this._identityDataContext.VideosInfo.Where(VideoInfo => VideoInfo.PlayListInfoId == PlayListInfo.PlayListInfoId)
                                                  .Skip(offset)
                                                  .Take(amount)
                                                  .ToListAsync();
        
    }
    public async Task<VideoInfo> GetVideoInfoByNameAsync(string name)
    {
        VideoInfo videoInfo = await this._identityDataContext.VideosInfo.Where(VideosInfo => VideosInfo.Name == name).FirstAsync();
        videoInfo.VideoInfoComments = await this.GetVideoInfoCommentsAsync(videoInfo.VideoInfoId);
        return videoInfo;
    }

    public async Task<List<VideoInfoReact>> GetVideoInfoReactsAsync(int videoInfoId)
    {
        return await this._identityDataContext.VideoInfoReacts
                                              .Where(VideoInfoReact => VideoInfoReact.VideoInfoId == videoInfoId)
                                              .Include(VideoInfoReact => VideoInfoReact.QuranHubUser)
                                              .ToListAsync();
    }

    public async Task<List<VideoInfoComment>> GetVideoInfoCommentsAsync(int videoInfoId)
    {
        return await this._identityDataContext.VideoInfoComments
                                              .Include(comment => comment.Verse)
                                              .Include(comment => comment.QuranHubUser)
                                              .Where(Comment => Comment.VideoInfoId == videoInfoId)
                                              .Take(5)
                                              .ToListAsync();
    }

    public async Task<VideoInfoComment> GetVideoInfoCommentByIdAsync(int commentId)
    {
        return await this._identityDataContext.VideoInfoComments
                                              .Include(comment => comment.Verse)
                                              .Include(comment => comment.QuranHubUser)
                                              .FirstAsync((comment) => comment.CommentId == commentId);
    }





    public async Task<List<VideoInfoComment>> GetMoreVideoInfoCommentsAsync(int videoInfoId, int offset, int amount)
    {
       return await _identityDataContext.VideoInfoComments
                                        .Include(comment => comment.Verse)
                                        .Include(comment => comment.QuranHubUser)
                                        .Where( comment => comment.VideoInfoId == videoInfoId)
                                        .AsQueryable()
                                        .Skip(offset)
                                        .Take(amount)
                                        .ToListAsync();
    }
    public async Task<List<VideoInfoCommentReact>> GetMoreVideoInfoCommentReactsAsync(int videoInfoId, int offset, int amount)
    {
        return  await _identityDataContext.VideoInfoCommentReacts
                                          .Include(commentReact => commentReact.QuranHubUser)
                                          .Where(CommentReact => CommentReact.VideoInfoId == videoInfoId)
                                          .AsQueryable()
                                          .Skip(offset)
                                          .Take(amount)
                                          .ToListAsync();
    }

    public async Task<List<VideoInfoReact>> GetMoreVideoInfoReactsAsync(int videoInfoId, int offset, int amount)
    {
          return  await _identityDataContext.VideoInfoReacts
                                            .Include(VideoInfoReact => VideoInfoReact.QuranHubUser)
                                            .Where(VideoInfoReact => VideoInfoReact.VideoInfoId == videoInfoId)
                                            .AsQueryable()
                                            .Skip(offset)
                                            .Take(amount)
                                            .ToListAsync();
    }

    public async Task<VideoInfoReact> AddVideoInfoReactAsync(VideoInfoReact videoInfoReact, QuranHubUser user)
    {
        VideoInfo videoInfo = await this._identityDataContext.VideosInfo
                                                   .FirstAsync(VideoInfo => VideoInfo.VideoInfoId == videoInfoReact.VideoInfoId);

        VideoInfoReact insertedVideoInfoReact = videoInfo.AddVideoInfoReact(user.Id);

       

        await this._identityDataContext.SaveChangesAsync();




        return insertedVideoInfoReact;
    }

    public async Task<bool> RemoveVideoInfoReactAsync(int videoInfoId, QuranHubUser user)
    {
        VideoInfo videoInfo = this._identityDataContext.VideosInfo.Find(videoInfoId);

        VideoInfoReact VideoInfoReact = await this._identityDataContext.VideoInfoReacts
                                                             .Where(VideoInfoReact => VideoInfoReact.VideoInfoId == videoInfoId && VideoInfoReact.QuranHubUserId == user.Id)
                                                             .FirstAsync();

        videoInfo.RemoveVideoInfoReact(VideoInfoReact.ReactId);

        await this._identityDataContext.SaveChangesAsync();

        return true;
    }

    public async Task<VideoInfoComment> AddVideoInfoCommentAsync(VideoInfoComment comment, QuranHubUser user)
    {
        VideoInfo videoInfo = await this._identityDataContext.VideosInfo
                                                   .FirstAsync(VideoInfo => VideoInfo.VideoInfoId == comment.VideoInfoId);

        VideoInfoComment insertedComment = videoInfo.AddVideoInfoComment(user.Id, comment.Text, comment.VerseId);


        await this._identityDataContext.SaveChangesAsync();

        if (insertedComment.VerseId != null)
        {
            insertedComment = await this.GetVideoInfoCommentByIdAsync(insertedComment.CommentId);
        }

        this._identityDataContext.Comments.Attach(insertedComment);



        return insertedComment;
    }

    public async Task<bool> RemoveVideoInfoCommentAsync(int commentId)
    {
        VideoInfoComment comment = await this._identityDataContext.VideoInfoComments
                                                             .FirstAsync(comment => comment.CommentId == commentId);

        VideoInfo videoInfo = this._identityDataContext.VideosInfo.Find(comment.VideoInfoId);

        videoInfo.RemoveVideoInfoComment(comment.CommentId);


        await this._identityDataContext.SaveChangesAsync();

        return true;
    }

    public async Task<Tuple<VideoInfoCommentReact, VideoInfoCommentReactNotification>> AddVideoInfoCommentReactAsync(VideoInfoCommentReact commentReact, QuranHubUser user)
    {
        VideoInfoComment comment = await this._identityDataContext.VideoInfoComments
                                                             .Include(comment => comment.QuranHubUser)
                                                             .Include(comment => comment.VideoInfo)
                                                             .FirstAsync(comment => comment.CommentId == commentReact.CommentId);

        VideoInfoCommentReact insertedCommentReact = comment.AddVideoInfoCommentReact(user.Id);


        if (comment.QuranHubUserId != user.Id)
        {
            Follow follow = await this._identityDataContext.Follows
                                                           .Where(follow => follow.FollowedId == comment.QuranHubUserId && follow.FollowerId == user.Id)
                                                           .FirstAsync();


            follow.Likes++;
        }

        await this._identityDataContext.SaveChangesAsync();

        this._identityDataContext.VideoInfoCommentReacts.Attach(insertedCommentReact);

        VideoInfoCommentReactNotification commentReactNotification = new();

        if (comment.QuranHubUserId != user.Id)
        {
            commentReactNotification = comment.AddVideoInfoCommentReactNotifiaction(user, insertedCommentReact.ReactId);

            await this._identityDataContext.SaveChangesAsync();
        }

        return new Tuple<VideoInfoCommentReact, VideoInfoCommentReactNotification>(insertedCommentReact, commentReactNotification);
    }

    public async Task<bool> RemoveVideoInfoCommentReactAsync(int commentId, QuranHubUser user)
    {
        VideoInfoComment comment = await this._identityDataContext.VideoInfoComments.FindAsync(commentId);

        VideoInfoCommentReact CommentReact = await this._identityDataContext.VideoInfoCommentReacts
                                                                   .Include(VideoInfoCommentReact => VideoInfoCommentReact.VideoInfoCommentReactNotification)
                                                                   .Where(postCommentReact => postCommentReact.CommentId == comment.CommentId && postCommentReact.QuranHubUserId == user.Id)
                                                                   .FirstAsync();

        comment.RemoveVideoInfoCommentReact(CommentReact.ReactId);

        await this._identityDataContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<Verse>> GetVersesAsync()
    {
        return await this._identityDataContext.Verses.ToListAsync();
    }


}
