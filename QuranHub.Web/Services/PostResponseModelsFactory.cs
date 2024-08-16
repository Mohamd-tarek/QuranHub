
namespace QuranHub.Web.Services;

public class PostResponseModelsFactory :IPostResponseModelsFactory
{
    private IdentityDataContext _identityDataContext;
    private QuranHubUser _currentUser;
    private UserManager<QuranHubUser> _userManager;
    private IHttpContextAccessor _contextAccessor;
    private IUserResponseModelsFactory _userResponseModelsFactory;
    public PostResponseModelsFactory(
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
    public async Task<PostResponseModel> BuildPostResponseModelAsync(Post post)
    {
        if(post is ShareablePost)
        {
            return await this.BuildShareablePostResponseModelAsync((ShareablePost)post);
        }
        else
        {
            return await this.BuildSharedPostResponseModelAsync((SharedPost)post);
        }
    }
    public async Task<ShareablePostResponseModel> BuildShareablePostResponseModelAsync(ShareablePost post) 
    {

        ShareablePostResponseModel postResponseModel = new ()
        {
            PostId = post.PostId,
            DateTime = post.DateTime,
            Privacy = post.Privacy,
            QuranHubUser = this._userResponseModelsFactory.BuildPostUserResponseModel(post.QuranHubUser), 
            Verse  = BuildVerseResponseModel(post.Verse),
            Text  = post.Text,
            ReactedTo = await this.CheckPostReactedToAsync(post.PostId),
            ReactsCount = post.ReactsCount,
            CommentsCount = post.CommentsCount,
            SharesCount = post.SharesCount,
            Comments =  await this.BuildCommentsResponseModelAsync(post.PostComments)                      
        };

        return postResponseModel;
    }
    public async Task<SharedPostResponseModel> BuildSharedPostResponseModelAsync(SharedPost sharedPost)
    {

        SharedPostResponseModel sharedPostResponseModel = new ()
        {
            PostId = sharedPost.PostId,
            DateTime = sharedPost.DateTime,
            Privacy = sharedPost.Privacy,
            QuranHubUser = this._userResponseModelsFactory.BuildPostUserResponseModel(sharedPost.QuranHubUser),
            Verse = this.BuildVerseResponseModel(sharedPost.Verse),
            Text = sharedPost.Text,
            ReactedTo = await this.CheckPostReactedToAsync(sharedPost.PostId),
            ReactsCount = sharedPost.ReactsCount,
            CommentsCount = sharedPost.CommentsCount,
            Comments = await this.BuildCommentsResponseModelAsync(sharedPost.PostComments),
            Share = await this.BuildSharedPostShareResponseModelAsync(sharedPost.PostShare)
        };

        return sharedPostResponseModel;
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

    public  List<ReactResponseModel> BuildPostReactsResponseModel(List<PostReact> postReacts)
    {
        List<ReactResponseModel> postReactResponseModels = new ();

        foreach (var postReact in postReacts)
        {
            ReactResponseModel postReactResponseModel = this.BuildPostReactResponseModel(postReact);
            postReactResponseModels.Add(postReactResponseModel);
        } 

        return postReactResponseModels;
    }

    public ReactResponseModel BuildPostReactResponseModel(PostReact postReact) 
    { 
        ReactResponseModel postReactResponseModel = new ()
        {
            ReactId = postReact.ReactId,
            DateTime = postReact.DateTime,
            QuranHubUser = this._userResponseModelsFactory.BuildPostUserResponseModel(postReact.QuranHubUser),
            Type = postReact.Type
        };
                            
        return postReactResponseModel;
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


    public async Task<List<CommentResponseModel>> BuildCommentsResponseModelAsync(List<PostComment> comments)
    {
        List<CommentResponseModel> commentsResponseModels = new ();

        foreach (var comment in comments)
        {
           CommentResponseModel commentResponseModel = await this.BuildCommentResponseModelAsync(comment);
           commentsResponseModels.Add(commentResponseModel);
        }  

        return commentsResponseModels;
    }

    public  async Task<CommentResponseModel> BuildCommentResponseModelAsync(PostComment comment) 
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

    public List<ReactResponseModel> BuildCommentReactsResponseModel(List<PostCommentReact> commentReacts) 
    {
        List<ReactResponseModel> commentReactsViewMdoel = new ();

        foreach (var commentReact in commentReacts)
        {
            ReactResponseModel commentReactResponseModel = this.BuildCommentReactResponseModel(commentReact);
            commentReactsViewMdoel.Add(commentReactResponseModel);
        }

        return commentReactsViewMdoel;
     } 

    public ReactResponseModel BuildCommentReactResponseModel(PostCommentReact commentReact)
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

    public  List<PostShareResponseModel> BuildSharesResponseModel(List<PostShare> shares) 
    {
        List<PostShareResponseModel> shareResponseModels = new ();

        foreach (var share in shares)
        {
            PostShareResponseModel shareResponseModel = this.BuildShareResponseModel(share);
            shareResponseModels.Add(shareResponseModel);
        }  

        return shareResponseModels;
    }

    public PostShareResponseModel BuildShareResponseModel(PostShare share)
    {
        PostShareResponseModel shareResponseModel = new ()
        {
            ShareId = share.ShareId,
            DateTime = share.DateTime,
            QuranHubUser = this._userResponseModelsFactory.BuildPostUserResponseModel(share.QuranHubUser),
            Post = null
        };
                        
        return shareResponseModel;
    }

    public async Task<PostShareResponseModel> BuildSharedPostShareResponseModelAsync(PostShare share)
    {
        PostShareResponseModel shareResponseModel = new ()
        {
            ShareId = share.ShareId,
            DateTime = share.DateTime,
            QuranHubUser = this._userResponseModelsFactory.BuildPostUserResponseModel(share.QuranHubUser),
            Post = await this.BuildShareablePostResponseModelAsync(share.ShareablePost)
        };

        return shareResponseModel;
    }

}
