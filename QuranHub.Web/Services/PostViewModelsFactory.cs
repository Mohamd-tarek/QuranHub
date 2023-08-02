
namespace QuranHub.Web.Services;

public class PostViewModelsFactory :IPostViewModelsFactory
{
    private IdentityDataContext _identityDataContext;
    private QuranHubUser _currentUser;
    private UserManager<QuranHubUser> _userManager;
    private IHttpContextAccessor _contextAccessor;
    private IUserViewModelsFactory _userViewModelsFactory;
    public PostViewModelsFactory(
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
    public async Task<PostViewModel> BuildPostViewModelAsync(Post post)
    {
        if(post is ShareablePost)
        {
            return await this.BuildShareablePostViewModelAsync((ShareablePost)post);
        }
        else
        {
            return await this.BuildSharedPostViewModelAsync((SharedPost)post);
        }
    }
    public async Task<ShareablePostViewModel> BuildShareablePostViewModelAsync(ShareablePost post) 
    {

        ShareablePostViewModel postViewModel = new ()
        {
            PostId = post.PostId,
            DateTime = post.DateTime,
            Privacy = post.Privacy,
            QuranHubUser = this._userViewModelsFactory.BuildPostUserViewModel(post.QuranHubUser), 
            Verse  = BuildVerseViewModel(post.Verse),
            Text  = post.Text,
            ReactedTo = await this.CheckPostReactedToAsync(post.PostId),
            ReactsCount = post.ReactsCount,
            CommentsCount = post.CommentsCount,
            SharesCount = post.SharesCount,
            Comments =  await this.BuildCommentsViewModelAsync(post.PostComments)                      
        };

        return postViewModel;
    }
    public async Task<SharedPostViewModel> BuildSharedPostViewModelAsync(SharedPost sharedPost)
    {

        SharedPostViewModel sharedPostViewModel = new ()
        {
            PostId = sharedPost.PostId,
            DateTime = sharedPost.DateTime,
            Privacy = sharedPost.Privacy,
            QuranHubUser = this._userViewModelsFactory.BuildPostUserViewModel(sharedPost.QuranHubUser),
            Verse = this.BuildVerseViewModel(sharedPost.Verse),
            Text = sharedPost.Text,
            ReactedTo = await this.CheckPostReactedToAsync(sharedPost.PostId),
            ReactsCount = sharedPost.ReactsCount,
            CommentsCount = sharedPost.CommentsCount,
            Comments = await this.BuildCommentsViewModelAsync(sharedPost.PostComments),
            Share = await this.BuildSharedPostShareViewModelAsync(sharedPost.PostShare)
        };

        return sharedPostViewModel;
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

    public  List<ReactViewModel> BuildPostReactsViewModel(List<PostReact> postReacts)
    {
        List<ReactViewModel> postReactViewModels = new ();

        foreach (var postReact in postReacts)
        {
            ReactViewModel postReactViewModel = this.BuildPostReactViewModel(postReact);
            postReactViewModels.Add(postReactViewModel);
        } 

        return postReactViewModels;
    }

    public ReactViewModel BuildPostReactViewModel(PostReact postReact) 
    { 
        ReactViewModel postReactViewModel = new ()
        {
            ReactId = postReact.ReactId,
            DateTime = postReact.DateTime,
            QuranHubUser = this._userViewModelsFactory.BuildPostUserViewModel(postReact.QuranHubUser),
            Type = postReact.Type
        };
                            
        return postReactViewModel;
    }

    public async Task<bool> CheckPostReactedToAsync(int PostId)
    {
        IEnumerable<PostReact> postReacts = await this._identityDataContext.PostReacts
                                            .Where(postReact => postReact.PostId == PostId)
                                            .Include(postReact => postReact.QuranHubUser)
                                            .ToArrayAsync();

        foreach (var react in postReacts)
        {
            if (react.QuranHubUser.Id == _currentUser.Id)
            {
                return true;
            }
        }

        return false;
    }


    public async Task<List<CommentViewModel>> BuildCommentsViewModelAsync(List<PostComment> comments)
    {
        List<CommentViewModel> commentsViewModels = new ();

        foreach (var comment in comments)
        {
           CommentViewModel commentViewModel = await this.BuildCommentViewModelAsync(comment);
           commentsViewModels.Add(commentViewModel);
        }  

        return commentsViewModels;
    }

    public  async Task<CommentViewModel> BuildCommentViewModelAsync(PostComment comment) 
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

    public List<ReactViewModel> BuildCommentReactsViewModel(List<PostCommentReact> commentReacts) 
    {
        List<ReactViewModel> commentReactsViewMdoel = new ();

        foreach (var commentReact in commentReacts)
        {
            ReactViewModel commentReactViewModel = this.BuildCommentReactViewModel(commentReact);
            commentReactsViewMdoel.Add(commentReactViewModel);
        }

        return commentReactsViewMdoel;
     } 

    public ReactViewModel BuildCommentReactViewModel(PostCommentReact commentReact)
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
        List<PostCommentReact> commentReacts = await this._identityDataContext.PostCommentReacts
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

    public  List<PostShareViewModel> BuildSharesViewModel(List<PostShare> shares) 
    {
        List<PostShareViewModel> shareViewModels = new ();

        foreach (var share in shares)
        {
            PostShareViewModel shareViewModel = this.BuildShareViewModel(share);
            shareViewModels.Add(shareViewModel);
        }  

        return shareViewModels;
    }

    public PostShareViewModel BuildShareViewModel(PostShare share)
    {
        PostShareViewModel shareViewModel = new ()
        {
            ShareId = share.ShareId,
            DateTime = share.DateTime,
            QuranHubUser = this._userViewModelsFactory.BuildPostUserViewModel(share.QuranHubUser),
            Post = null
        };
                        
        return shareViewModel;
    }

    public async Task<PostShareViewModel> BuildSharedPostShareViewModelAsync(PostShare share)
    {
        PostShareViewModel shareViewModel = new ()
        {
            ShareId = share.ShareId,
            DateTime = share.DateTime,
            QuranHubUser = this._userViewModelsFactory.BuildPostUserViewModel(share.QuranHubUser),
            Post = await this.BuildShareablePostViewModelAsync(share.ShareablePost)
        };

        return shareViewModel;
    }

}
