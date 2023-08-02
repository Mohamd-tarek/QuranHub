
using QuranHub.Domain.Models;

namespace QuranHub.Web.Services;

public class VideoInfoViewModelsFactory : IVideoInfoViewModelsFactory
{
    private IdentityDataContext _identityDataContext;
    private QuranHubUser _currentUser;
    private UserManager<QuranHubUser> _userManager;
    private IHttpContextAccessor _contextAccessor;
    private IUserViewModelsFactory _userViewModelsFactory;
    public VideoInfoViewModelsFactory(
        IdentityDataContext identityDataContext, 
        UserManager<QuranHubUser> userManager,
        IHttpContextAccessor contextAccessor,
        IUserViewModelsFactory userViewModelsFactory)
    {
        _identityDataContext = identityDataContext ?? throw new ArgumentNullException(nameof(identityDataContext));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        _userViewModelsFactory = userViewModelsFactory ?? throw new ArgumentNullException(nameof(userViewModelsFactory));
       _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor)); ;
       _currentUser = _userManager.GetUserAsync(_contextAccessor.HttpContext.User).Result;
    }
    public async Task<VideoInfoViewModel> BuildVideoInfoViewModelAsync(VideoInfo videoInfo)
    {
        VideoInfoViewModel videoInfoModel = new()
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
            Comments = await this.BuildCommentsViewModelAsync(videoInfo.VideoInfoComments)
        };

        return videoInfoModel;
    }
    
    public VerseViewModel BuildVerseViewModel(Verse Verse)
    {
        VerseViewModel quranPostViewModel = new ()
        {
            Index = Verse.Index,
            Sura = Verse.Sura,
            Aya = Verse.Aya,
            Text = Verse.Text
        };

        return quranPostViewModel;
    }

    public  List<ReactViewModel> BuildVideoInfoReactsViewModel(List<VideoInfoReact> videoInfoReacts)
    {
        List<ReactViewModel> videoInfoReactViewModels = new ();

        foreach (var videoInfoReact in videoInfoReacts)
        {
            ReactViewModel videoInfoReactViewModel = this.BuildVideoInfoReactViewModel(videoInfoReact);
            videoInfoReactViewModels.Add(videoInfoReactViewModel);
        }

        return videoInfoReactViewModels;
    }

    public ReactViewModel BuildVideoInfoReactViewModel(VideoInfoReact videoInfoReact) 
    { 
        ReactViewModel videoInfoReactViewModel = new ()
        {
            ReactId = videoInfoReact.ReactId,
            DateTime = videoInfoReact.DateTime,
            QuranHubUser = this._userViewModelsFactory.BuildPostUserViewModel(videoInfoReact.QuranHubUser),
            Type = videoInfoReact.Type
        };
                            
        return videoInfoReactViewModel;
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


    public async Task<List<CommentViewModel>> BuildCommentsViewModelAsync(List<VideoInfoComment> comments)
    {
        List<CommentViewModel> commentsViewModels = new ();

        foreach (var comment in comments)
        {
           CommentViewModel commentViewModel = await this.BuildCommentViewModelAsync(comment);
           commentsViewModels.Add(commentViewModel);
        }  

        return commentsViewModels;
    }

    public  async Task<CommentViewModel> BuildCommentViewModelAsync(VideoInfoComment comment) 
    {
        CommentViewModel commentViewModel = new ()
        {
            CommentId = comment.CommentId,
            DateTime = comment.DateTime,
            QuranHubUser = this._userViewModelsFactory.BuildPostUserViewModel(comment.QuranHubUser),
            Verse = comment.Verse == null ? null : this.BuildVerseViewModel(comment.Verse),
            ReactedTo = await this.CheckCommentReactedToAsync(comment.CommentId),
            Text = comment.Text,
            ReactsCount = comment.ReactsCount
        };
                           
        return commentViewModel;
    }

    public List<ReactViewModel> BuildCommentReactsViewModel(List<VideoInfoCommentReact> commentReacts) 
    {
        List<ReactViewModel> commentReactsViewMdoel = new ();

        foreach (var commentReact in commentReacts)
        {
            ReactViewModel commentReactViewModel = this.BuildCommentReactViewModel(commentReact);
            commentReactsViewMdoel.Add(commentReactViewModel);
        }

        return commentReactsViewMdoel;
     } 

    public ReactViewModel BuildCommentReactViewModel(VideoInfoCommentReact commentReact)
    {

        ReactViewModel commentReactViewModel = new ()
        {
            ReactId = commentReact.ReactId,
            DateTime = commentReact.DateTime,
            Type = commentReact.Type,
            QuranHubUser = this._userViewModelsFactory.BuildPostUserViewModel(commentReact.QuranHubUser)
        };

        return commentReactViewModel;

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
