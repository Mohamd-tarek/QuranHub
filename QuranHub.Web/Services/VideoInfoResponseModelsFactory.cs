
using QuranHub.Domain.Models;

namespace QuranHub.Web.Services;

public class VideoInfoResponseModelsFactory : IVideoInfoResponseModelsFactory
{
    private IdentityDataContext _identityDataContext;
    private QuranHubUser _currentUser;
    private UserManager<QuranHubUser> _userManager;
    private IHttpContextAccessor _contextAccessor;
    private IUserResponseModelsFactory _userResponseModelsFactory;
    public VideoInfoResponseModelsFactory(
        IdentityDataContext identityDataContext, 
        UserManager<QuranHubUser> userManager,
        IHttpContextAccessor contextAccessor,
        IUserResponseModelsFactory userResponseModelsFactory)
    {
        _identityDataContext = identityDataContext ?? throw new ArgumentNullException(nameof(identityDataContext));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        _userResponseModelsFactory = userResponseModelsFactory ?? throw new ArgumentNullException(nameof(userResponseModelsFactory));
       _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor)); ;
       _currentUser = _userManager.GetUserAsync(_contextAccessor.HttpContext.User).Result;
    }
    public async Task<VideoInfoResponseModel> BuildVideoInfoResponseModelAsync(VideoInfo videoInfo)
    {
        VideoInfoResponseModel videoInfoModel = new()
        {
            VideoInfoId = videoInfo.VideoInfoId,
            ThumbnailImage = videoInfo.ThumbnailImage,
            Name = videoInfo.Name,
            Type = videoInfo.Type,
            Duration = videoInfo.Duration,
            Width = videoInfo.Width,
            Height = videoInfo.Height,
            Path = videoInfo.Path,
            Views = videoInfo.Views,
            PlayListInfoId = videoInfo.PlayListInfoId,
            ReactedTo = await this.CheckVideoInfoReactedToAsync(videoInfo.VideoInfoId),
            ReactsCount = videoInfo.ReactsCount,
            CommentsCount = videoInfo.CommentsCount,
            Comments = await this.BuildCommentsResponseModelAsync(videoInfo.VideoInfoComments)
        };

        return videoInfoModel;
    }
    
    public VerseResponseModel BuildVerseResponseModel(Verse Verse)
    {
        VerseResponseModel quranPostResponseModel = new ()
        {
            Index = Verse.Index,
            Sura = Verse.Sura,
            Aya = Verse.Aya,
            Text = Verse.Text
        };

        return quranPostResponseModel;
    }

    public  List<ReactResponseModel> BuildVideoInfoReactsResponseModel(List<VideoInfoReact> videoInfoReacts)
    {
        List<ReactResponseModel> videoInfoReactResponseModels = new ();

        foreach (var videoInfoReact in videoInfoReacts)
        {
            ReactResponseModel videoInfoReactResponseModel = this.BuildVideoInfoReactResponseModel(videoInfoReact);
            videoInfoReactResponseModels.Add(videoInfoReactResponseModel);
        }

        return videoInfoReactResponseModels;
    }

    public ReactResponseModel BuildVideoInfoReactResponseModel(VideoInfoReact videoInfoReact) 
    { 
        ReactResponseModel videoInfoReactResponseModel = new ()
        {
            ReactId = videoInfoReact.ReactId,
            DateTime = videoInfoReact.DateTime,
            QuranHubUser = this._userResponseModelsFactory.BuildPostUserResponseModel(videoInfoReact.QuranHubUser),
            Type = videoInfoReact.Type
        };
                            
        return videoInfoReactResponseModel;
    }

    public async Task<bool> CheckVideoInfoReactedToAsync(int VideoInfoId)
    {
        IEnumerable<VideoInfoReact> videoInfoReacts = await this._identityDataContext.VideoInfoReacts
                                            .Where(VideoInfoReact => VideoInfoReact.VideoInfoId == VideoInfoId)
                                            .Include(VideoInfoReact => VideoInfoReact.QuranHubUser)
                                            .ToArrayAsync();

        foreach (var react in videoInfoReacts)
        {
            if (react.QuranHubUser.Id == _currentUser.Id)
            {
                return true;
            }
        }

        return false;
    }


    public async Task<List<CommentResponseModel>> BuildCommentsResponseModelAsync(List<VideoInfoComment> comments)
    {
        List<CommentResponseModel> commentsResponseModels = new ();

        foreach (var comment in comments)
        {
           CommentResponseModel commentResponseModel = await this.BuildCommentResponseModelAsync(comment);
           commentsResponseModels.Add(commentResponseModel);
        }  

        return commentsResponseModels;
    }

    public  async Task<CommentResponseModel> BuildCommentResponseModelAsync(VideoInfoComment comment) 
    {
        CommentResponseModel commentResponseModel = new ()
        {
            CommentId = comment.CommentId,
            DateTime = comment.DateTime,
            QuranHubUser = this._userResponseModelsFactory.BuildPostUserResponseModel(comment.QuranHubUser),
            Verse = comment.Verse == null ? null : this.BuildVerseResponseModel(comment.Verse),
            ReactedTo = await this.CheckCommentReactedToAsync(comment.CommentId),
            Text = comment.Text,
            ReactsCount = comment.ReactsCount
        };
                           
        return commentResponseModel;
    }

    public List<ReactResponseModel> BuildCommentReactsResponseModel(List<VideoInfoCommentReact> commentReacts) 
    {
        List<ReactResponseModel> commentReactsViewMdoel = new ();

        foreach (var commentReact in commentReacts)
        {
            ReactResponseModel commentReactResponseModel = this.BuildCommentReactResponseModel(commentReact);
            commentReactsViewMdoel.Add(commentReactResponseModel);
        }

        return commentReactsViewMdoel;
     } 

    public ReactResponseModel BuildCommentReactResponseModel(VideoInfoCommentReact commentReact)
    {

        ReactResponseModel commentReactResponseModel = new ()
        {
            ReactId = commentReact.ReactId,
            DateTime = commentReact.DateTime,
            Type = commentReact.Type,
            QuranHubUser = this._userResponseModelsFactory.BuildPostUserResponseModel(commentReact.QuranHubUser)
        };

        return commentReactResponseModel;

    } 

    public async Task<bool> CheckCommentReactedToAsync(int CommentId){
        List<VideoInfoCommentReact> commentReacts = await this._identityDataContext.VideoInfoCommentReacts
                                               .Where(commentReact => commentReact.CommentId == CommentId)
                                               .Include(commentReact => commentReact.QuranHubUser)
                                               .ToListAsync();

        foreach (var commentReact in commentReacts)
        {
            if (commentReact.QuranHubUser.Id == _currentUser.Id)
            {
                return true;
            }
        }
        
        return false;
    }     

}
