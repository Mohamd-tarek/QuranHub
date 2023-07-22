
namespace QuranHub.DAL.Repositories;

public class PostRepository : IPostRepository 
{   
    private IdentityDataContext _identityDataContext;

    public  PostRepository(
        IdentityDataContext identityDataContext)
    { 
        _identityDataContext = identityDataContext;
    }

    public async Task<ShareablePost> CreatePostAsync(ShareablePost post)
    {
        post.DateTime = DateTime.Now;

        post = (await this._identityDataContext.ShareablePosts.AddAsync(post)).Entity;

        await this._identityDataContext.SaveChangesAsync();

        post = await this.GetShareablePostByIdAsync(post.PostId);

        return post;
    }



    public async Task<Post> GetPostByIdAsync(int postId)
    {
        if (postId == null)
        {
            throw new ArgumentNullException(nameof(postId));
        }

        Post post = await this._identityDataContext.Posts
                                                   .Include(post => post.QuranHubUser)
                                                   .Include(post => post.Verse)
                                                   .Where(post => post.PostId == postId)
                                                   .FirstAsync();

        post.Comments = await this.GetCommentsAsync(post.PostId);

        return post;
    }

    public async Task<Post> GetPostByIdWithSpecificCommentAsync(int postId, int commentId)
    {
        if (postId == null)
        {
            throw new ArgumentNullException(nameof(postId));
        }

        Post post = await this.GetPostByIdAsync(postId);

        if(!post.Comments.Contains(new Comment(){CommentId = commentId}))
        {
            Comment comment  = await this._identityDataContext.Comments.FindAsync(commentId);
            post.Comments.Add(comment);
        }

        return post;
    }
    public async Task<ShareablePost> GetShareablePostByIdAsync(int postId)
    {
        if(postId == null)
        {
            throw new ArgumentNullException(nameof(postId));
        }

        ShareablePost post = await this._identityDataContext.ShareablePosts
                                                            .Include(post => post.QuranHubUser)
                                                            .Include(post => post.Verse)
                                                            .Where(post => post.PostId == postId)
                                                            .FirstAsync();

        post.Comments = await this.GetCommentsAsync(post.PostId);
       
        return post;
    }


    public async Task<List<ShareablePost>> GetShareablePostsByQuranHubUserIdAsync(string quranHubUserId)
    {
        List<ShareablePost> posts = await this._identityDataContext.ShareablePosts
                                                                   .Include(post => post.QuranHubUser)
                                                                   .Include(post => post.Verse)
                                                                   .Where(post => post.QuranHubUserId == quranHubUserId)
                                                                   .OrderByDescending(Post => Post.DateTime)
                                                                   .ToListAsync();
        foreach(var post in posts)
        {
            post.Comments = await this.GetCommentsAsync(post.PostId);
        }
        return posts;

    }

    public async Task<SharedPost> GetSharedPostByIdAsync(int postId)
    {
        SharedPost Sharedpost = await this._identityDataContext.SharedPosts
                                                               .Include(post => post.QuranHubUser)
                                                               .Include(post => post.Verse)
                                                               .Include(post => post.Share)
                                                               .Where(post => post.PostId == postId)
                                                               .FirstAsync();

        Sharedpost.Comments = await this.GetCommentsAsync(Sharedpost.PostId);
        
        Sharedpost.Share.Post = await GetShareablePostByIdAsync(Sharedpost.Share.PostId);

        return Sharedpost;
    }

    public async Task<List<SharedPost>> GetSharedPostsByQuranHubUserIdAsync(string quranHubUserId)
    {
        List<SharedPost> posts = await this._identityDataContext.SharedPosts
                                                                .Include(post => post.QuranHubUser)
                                                                .Include(post => post.Verse)
                                                                .Include(post => post.Share)
                                                                .Where(post => post.QuranHubUserId == quranHubUserId)
                                                                .OrderByDescending(Post => Post.DateTime)
                                                                .ToListAsync();

        List<SharedPost> postsResult = new List<SharedPost>();

        foreach(var post in posts)
        {
            post.Comments = await this.GetCommentsAsync(post.PostId);
        }

        foreach (var post in posts)
        {
            post.Share.Post = await GetShareablePostByIdAsync(post.Share.PostId);
        }

        return posts;

    }
    public async Task<Verse> GetVerseAsync(int VerseId)
    {
        return await this._identityDataContext.Verses.FindAsync(VerseId);
    }


    public async Task<List<PostReact>> GetPostReactsAsync(int postId)
    {
       return await this._identityDataContext.PostReacts
                                             .Where(postReact => postReact.PostId == postId)
                                             .Include(PostReact => PostReact.QuranHubUser)
                                             .ToListAsync();
    }

    public async Task<List<Comment>> GetCommentsAsync(int postId)
    {
        return await this._identityDataContext.Comments
                                              .Include(comment => comment.Verse)
                                              .Include(comment => comment.QuranHubUser)
                                              .Where(Comment => Comment.PostId == postId)
                                              .Take(5)
                                              .ToListAsync();
    }

    public async Task<Comment> GetCommentByIdAsync(int commentId)
    {
        return await this._identityDataContext.Comments
                                              .Include(comment => comment.Verse)
                                              .Include(comment => comment.QuranHubUser)
                                              .FirstAsync((comment) => comment.CommentId ==  commentId);
    }

    public async Task<List<Share>> GetPostSharesAsync(int postId)
    {
        return await this._identityDataContext.Shares
                                              .Include(share => share.QuranHubUser)
                                              .Where(Share => Share.PostId == postId)
                                              .ToListAsync();
    }

    public async Task<Share> GetSharedPostShareAsync(int shareId)
    {
        Share share =  await this._identityDataContext.Shares.FindAsync(shareId);

        share.Post = await this.GetShareablePostByIdAsync(share.PostId);

        return share;
               
    }

    public async Task<List<Comment>> GetMoreCommentsAsync(int postId, int offset, int amount)
    {
        List<Comment> PostComments = await _identityDataContext.Comments
                                                               .Include(comment => comment.Verse)
                                                               .Include(comment => comment.QuranHubUser)
                                                               .Where(PostComment => PostComment.PostId == postId)
                                                               .AsQueryable()
                                                               .Skip(offset)
                                                               .Take(amount)
                                                               .ToListAsync();
        return PostComments;
    }
    public async Task<List<CommentReact>> GetMoreCommentReactsAsync(int postId, int offset, int amount)
    {
        List<CommentReact> CommentReacts = await _identityDataContext.CommentReacts
                                                                     .Include(commentReact => commentReact.QuranHubUser)
                                                                     .Where(CommentReact => CommentReact.CommentId == postId)
                                                                     .AsQueryable()
                                                                     .Skip(offset)
                                                                     .Take(amount)
                                                                     .ToListAsync();
        return CommentReacts;
    }

    public async Task<List<PostReact>> GetMorePostReactsAsync(int postId, int offset, int amount)
    {
        List<PostReact> PostReacts = await _identityDataContext.PostReacts
                                                               .Include(PostReact => PostReact.QuranHubUser)
                                                               .Where(PostReact => PostReact.PostId == postId)
                                                               .AsQueryable()
                                                               .Skip(offset)
                                                               .Take(amount)
                                                               .ToListAsync();
        return PostReacts;
    }

    public async Task<List<Share>> GetMoreSharesAsync(int postId, int offset, int amount)
    {
        List<Share> Shares = await _identityDataContext.Shares
                                                       .Include(share => share.QuranHubUser)
                                                       .Where(Share => Share.PostId == postId)
                                                       .AsQueryable()
                                                       .Skip(offset)
                                                       .Take(amount)
                                                       .ToListAsync();
        return Shares;
    }



    public async Task<Post> EditPostAsync(Post post)
    {

        Post targetpost = await this._identityDataContext.Posts.FindAsync(post.PostId);

        targetpost.Text = post.Text;
        targetpost.VerseId = post.VerseId;
        targetpost.Privacy = post.Privacy;

        await _identityDataContext.SaveChangesAsync();

        return targetpost;
    }

    public async Task<bool> DeletePostAsync(int postId)
    {
        Post post = await this._identityDataContext.Posts.FindAsync(postId);

        if ((await this._identityDataContext.SharedPosts.FindAsync(postId)) != null)
        {
            SharedPost sharedPost  = await this._identityDataContext.SharedPosts.FindAsync(postId);

            Share share = await this._identityDataContext.Shares
                                                         .Include(Share => Share.Post)
                                                         .Include(Share => Share.ShareNotification)
                                                         .FirstAsync(share => share.ShareId == sharedPost.ShareId);

            share.Post.SharesCount--;

            EntityEntry<Share> shareEntityEntry = this._identityDataContext.Shares.Remove(share);
        }

        EntityEntry<Post> postEntityEntry = this._identityDataContext.Posts.Remove(post);

        await _identityDataContext.SaveChangesAsync();

        if (postEntityEntry.State.Equals(EntityState.Detached))
        {
            return true;
        }

        return false;
    }

    public async Task<Tuple<PostReact, PostReactNotification>> AddPostReactAsync(PostReact postReact, QuranHubUser user)
    {
        Post post = await this._identityDataContext.Posts
                                                   .Include(post => post.QuranHubUser)
                                                   .FirstAsync(post => post.PostId == postReact.PostId);

        PostReact insertedPostReact = post.AddReact(user.Id);

        if(post.QuranHubUserId  != user.Id)
        {
            Follow follow = await this._identityDataContext.Follows
                                                        .Where(follow => follow.FollowedId == post.QuranHubUserId && follow.FollowerId == user.Id)
                                                        .FirstAsync(); 

            follow.Likes++;     
        }                                                

        await this._identityDataContext.SaveChangesAsync();

        this._identityDataContext.PostReacts.Attach(insertedPostReact);

        PostReactNotification reactNotification = new();

        if (post.QuranHubUserId != user.Id)
        {
           reactNotification = post.AddReactNotifiaction(user, insertedPostReact.ReactId);

           await this._identityDataContext.SaveChangesAsync();
        }

       

        return new Tuple<PostReact, PostReactNotification>(insertedPostReact, reactNotification);
    }

    public async Task<bool> RemovePostReactAsync(int postId, QuranHubUser user)
    {
        Post post = this._identityDataContext.Posts.Find(postId);

        PostReact postReact = await this._identityDataContext.PostReacts
                                                             .Where(postReact => post.PostId == postReact.PostId && postReact.QuranHubUserId == user.Id)
                                                             .Include(postReact => postReact.ReactNotification)
                                                             .FirstAsync();

        post.RemoveReact(postReact.ReactId);

        await this._identityDataContext.SaveChangesAsync();

        return true;
    }

    public async Task<Tuple<Comment, CommentNotification>> AddCommentAsync(Comment comment, QuranHubUser user)
    {
        Post post = await this._identityDataContext.Posts
                                                   .Include(post => post.QuranHubUser)
                                                   .FirstAsync(post => post.PostId == comment.PostId);

        Comment insertedComment = post.AddComment(user.Id, comment.Text, comment.VerseId);

       if(post.QuranHubUserId != user.Id)
       {
            Follow follow = await this._identityDataContext.Follows
                                                        .Where(follow => follow.FollowedId == post.QuranHubUserId && follow.FollowerId == user.Id)
                                                        .FirstAsync(); 

            follow.Comments++;   
       }                                                  

        await this._identityDataContext.SaveChangesAsync();

        if(insertedComment.VerseId != null)
        {
            insertedComment = await this.GetCommentByIdAsync(insertedComment.CommentId);
        }

        this._identityDataContext.Comments.Attach(insertedComment);

        CommentNotification commentNotification = new();

        if (post.QuranHubUserId != user.Id)
        {
            commentNotification = post.AddCommentNotifiaction(user, insertedComment.CommentId);

            await this._identityDataContext.SaveChangesAsync();
        }

        return new Tuple<Comment, CommentNotification>( insertedComment, commentNotification);
    }

    public async Task<bool> RemoveCommentAsync(int commentId)
    {
        Comment comment = await this._identityDataContext.Comments
                                                         .Include(comment => comment.CommentNotifications)
                                                         .FirstAsync(comment => comment.CommentId == commentId );

        Post post = this._identityDataContext.Posts.Find(comment.PostId);

        post.RemoveComment(comment.CommentId);
                                                  

        await this._identityDataContext.SaveChangesAsync();

        return true;
    }

    public async Task<Tuple<CommentReact, CommentReactNotification>> AddCommentReactAsync(CommentReact commentReact, QuranHubUser user)
    {
        Comment comment = await this._identityDataContext.Comments
                                                         .Include(comment => comment.QuranHubUser)
                                                         .Include(comment => comment.Post)
                                                         .FirstAsync(comment => comment.CommentId ==  commentReact.CommentId);

        CommentReact insertedCommentReact = comment.AddCommentReact(user.Id);

        if(comment.QuranHubUserId  != user.Id)
        {
            Follow follow = await this._identityDataContext.Follows
                                                        .Where(follow => follow.FollowedId == comment.QuranHubUserId && follow.FollowerId == user.Id)
                                                        .FirstAsync(); 


            follow.Likes++;                                                     
        }

        await this._identityDataContext.SaveChangesAsync();

        this._identityDataContext.CommentReacts.Attach(insertedCommentReact);

        CommentReactNotification commentReactNotification = new();

        if (comment.QuranHubUserId != user.Id)
        {
            commentReactNotification = comment.AddCommentReactNotifiaction(user, insertedCommentReact.ReactId);

            await this._identityDataContext.SaveChangesAsync();
        }

        return new Tuple<CommentReact, CommentReactNotification> (insertedCommentReact, commentReactNotification);
    }

    public async Task<bool> RemoveCommentReactAsync(int commentId, QuranHubUser user)
    {
        Comment comment =  await this._identityDataContext.Comments.FindAsync(commentId);

        CommentReact CommentReact = await this._identityDataContext.CommentReacts
                                                                   .Include(commentReact => commentReact.ReactNotification)
                                                                   .Where(CommentReact => CommentReact.CommentId == comment.CommentId && CommentReact.QuranHubUserId == user.Id)
                                                                   .FirstAsync();

        comment.RemoveCommentReact(CommentReact.ReactId);

        await this._identityDataContext.SaveChangesAsync();

        return true;
    }
    public async Task<Tuple<Share, ShareNotification>> SharePostAsync(SharedPost sharedPost, QuranHubUser user)
    {

        ShareablePost post = await this._identityDataContext.ShareablePosts
                                                            .Include(post => post.QuranHubUser)
                                                            .FirstAsync(post => post.PostId == sharedPost.Share.PostId);

        Share insertedShare = post.AddShare(user.Id);

       if(post.QuranHubUserId  != user.Id)
       {
            Follow follow = await this._identityDataContext.Follows
                                                        .Where(follow => follow.FollowedId == post.QuranHubUserId && follow.FollowerId == user.Id)
                                                        .FirstAsync(); 

            follow.Shares++;  
       }                                                   

        await this._identityDataContext.SaveChangesAsync();

        this._identityDataContext.Shares.Attach(insertedShare);


        sharedPost.DateTime = DateTime.Now;

        sharedPost.Share = insertedShare;

        await this._identityDataContext.SharedPosts.AddAsync(sharedPost);

        await this._identityDataContext.SaveChangesAsync();

        this._identityDataContext.SharedPosts.Attach(sharedPost);

        ShareNotification shareNotification = new();

        if (post.QuranHubUserId != user.Id)
        {
            shareNotification = post.AddShareNotification(user, insertedShare.ShareId);

            await this._identityDataContext.SaveChangesAsync();
        }

       

        return new Tuple<Share, ShareNotification>( insertedShare,shareNotification);
    }
    public async Task<List<ShareablePost>> SearchShareablePostsAsync(string keyword)
    {
        List<ShareablePost> posts = await this._identityDataContext.ShareablePosts
                                                                   .Where(post => post.Text.Contains(keyword))
                                                                   .ToListAsync();

        List<ShareablePost> fullPosts = new List<ShareablePost>();

        foreach(var post in posts)
        {
            ShareablePost fullPost = await this.GetShareablePostByIdAsync(post.PostId);
            fullPosts.Add(fullPost);
        }

        return fullPosts;
    }

    public async Task<List<SharedPost>> SearchSharedPostsAsync(string keyword)
    {
        List<SharedPost> sharedPosts = await this._identityDataContext.SharedPosts
                                                                      .Where(sharedPosts => sharedPosts.Text.Contains(keyword))
                                                                      .ToListAsync();

        List<SharedPost> fullPosts = new List<SharedPost>();

        foreach (var post in sharedPosts)
        {
            SharedPost fullPost = await this.GetSharedPostByIdAsync(post.PostId);
            fullPosts.Add(fullPost);
        }

        return fullPosts;
    }

    public async Task<List<Verse>> GetVersesAsync()
    {
        return await this._identityDataContext.Verses.ToListAsync();
    }

}
