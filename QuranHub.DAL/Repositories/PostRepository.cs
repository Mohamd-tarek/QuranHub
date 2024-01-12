
using Microsoft.Extensions.Logging;
using QuranHub.Domain.Models;

namespace QuranHub.DAL.Repositories;
/// <inheritdoc/>
public class PostRepository : IPostRepository 
{   
    private IdentityDataContext _identityDataContext;
    private readonly ILogger<PostRepository> _logger;
    public  PostRepository(
        IdentityDataContext identityDataContext,
        ILogger<PostRepository> logger)
    { 
        _identityDataContext = identityDataContext;
        _logger = logger;
    }

    public async Task<ShareablePost> CreatePostAsync(ShareablePost post)
    {
        try
        {
            post.DateTime = DateTime.Now;

            post = (await this._identityDataContext.ShareablePosts.AddAsync(post)).Entity;

            await this._identityDataContext.SaveChangesAsync();

            post = await this.GetShareablePostByIdAsync(post.PostId);

            return post;
        }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
    }



    public async Task<Post> GetPostByIdAsync(int postId)
    {
        try
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

            post.PostComments = await this.GetPostCommentsAsync(post.PostId);

            return post;
        }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
    }

    public async Task<Post> GetPostByIdWithSpecificCommentAsync(int postId, int commentId)
    {
        try
        {
                if (postId == null)
            {
                throw new ArgumentNullException(nameof(postId));
            }

            Post post = await this.GetPostByIdAsync(postId);

            if(!post.PostComments.Contains(new Comment(){CommentId = commentId}))
            {
                PostComment comment  = await this._identityDataContext.PostComments.FindAsync(commentId);
                post.PostComments.Add(comment);
            }

            return post;
        }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
    }
    public async Task<ShareablePost> GetShareablePostByIdAsync(int postId)
    {
        try
        {
            if (postId == null)
            {
                throw new ArgumentNullException(nameof(postId));
            }

            ShareablePost post = await this._identityDataContext.ShareablePosts
                                                                .Include(post => post.QuranHubUser)
                                                                .Include(post => post.Verse)
                                                                .Where(post => post.PostId == postId)
                                                                .FirstAsync();

            post.PostComments = await this.GetPostCommentsAsync(post.PostId);
       
            return post;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }


    public async Task<List<ShareablePost>> GetShareablePostsByQuranHubUserIdAsync(string quranHubUserId)
    {
        try
        {
            List<ShareablePost> posts = await this._identityDataContext.ShareablePosts
                                                                   .Include(post => post.QuranHubUser)
                                                                   .Include(post => post.Verse)
                                                                   .Where(post => post.QuranHubUserId == quranHubUserId)
                                                                   .OrderByDescending(Post => Post.DateTime)
                                                                   .ToListAsync();
            foreach(var post in posts)
            {
                post.PostComments = await this.GetPostCommentsAsync(post.PostId);
            }
            return posts;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }

    }

    public async Task<SharedPost> GetSharedPostByIdAsync(int postId)
    {
        try
        {
            SharedPost Sharedpost = await this._identityDataContext.SharedPosts
                                                               .Include(post => post.QuranHubUser)
                                                               .Include(post => post.Verse)
                                                               .Include(post => post.PostShare)
                                                               .Where(post => post.PostId == postId)
                                                               .FirstAsync();

            Sharedpost.PostComments = await this.GetPostCommentsAsync(Sharedpost.PostId);
        
            Sharedpost.PostShare.ShareablePost = await GetShareablePostByIdAsync(Sharedpost.PostShare.PostId);

            return Sharedpost;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<List<SharedPost>> GetSharedPostsByQuranHubUserIdAsync(string quranHubUserId)
    {
        try
        {
            List<SharedPost> posts = await this._identityDataContext.SharedPosts
                                                                .Include(post => post.QuranHubUser)
                                                                .Include(post => post.Verse)
                                                                .Include(post => post.PostShare)
                                                                .Where(post => post.QuranHubUserId == quranHubUserId)
                                                                .OrderByDescending(Post => Post.DateTime)
                                                                .ToListAsync();

            List<SharedPost> postsResult = new List<SharedPost>();

            foreach(var post in posts)
            {
                post.PostComments = await this.GetPostCommentsAsync(post.PostId);
            }

            foreach (var post in posts)
            {
                post.PostShare.ShareablePost = await GetShareablePostByIdAsync(post.PostShare.PostId);
            }

            return posts;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }

    }
    public async Task<Verse> GetVerseAsync(int VerseId)
    {
        try
        {
            return await this._identityDataContext.Verses.FindAsync(VerseId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }


    public async Task<List<PostReact>> GetPostReactsAsync(int postId)
    {
        try
        {
            return await this._identityDataContext.PostReacts
                                             .Where(postReact => postReact.PostId == postId)
                                             .Include(PostReact => PostReact.QuranHubUser)
                                             .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<List<PostComment>> GetPostCommentsAsync(int postId)
    {
        try
        {
            return await this._identityDataContext.PostComments
                                              .Include(comment => comment.Verse)
                                              .Include(comment => comment.QuranHubUser)
                                              .Where(Comment => Comment.PostId == postId)
                                              .Take(5)
                                              .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<PostComment> GetPostCommentByIdAsync(int commentId)
    {
        try
        {
            return await this._identityDataContext.PostComments
                                              .Include(comment => comment.Verse)
                                              .Include(comment => comment.QuranHubUser)
                                                  .FirstAsync((comment) => comment.CommentId ==  commentId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<List<PostShare>> GetPostSharesAsync(int postId)
    {
        try
        {
            return await this._identityDataContext.PostShares
                                              .Include(share => share.QuranHubUser)
                                              .Where(Share => Share.PostId == postId)
                                              .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<PostShare> GetSharedPostShareAsync(int shareId)
    {
        try
        {
            PostShare share =  await this._identityDataContext.PostShares.FindAsync(shareId);

            share.ShareablePost = await this.GetShareablePostByIdAsync(share.PostId);

            return share;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
               
    }

    public async Task<List<PostComment>> GetMorePostCommentsAsync(int postId, int offset, int amount)
    {
        try
        {
            List<PostComment> PostComments = await _identityDataContext.PostComments
                                                                   .Include(comment => comment.Verse)
                                                                   .Include(comment => comment.QuranHubUser)
                                                                   .Where(PostComment => PostComment.PostId == postId)
                                                                   .AsQueryable()
                                                                   .Skip(offset)
                                                                   .Take(amount)
                                                                   .ToListAsync();
            return PostComments;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }
    public async Task<List<PostCommentReact>> GetMorePostCommentReactsAsync(int postId, int offset, int amount)
    {
        try
        {
            List<PostCommentReact> CommentReacts = await _identityDataContext.PostCommentReacts
                                                                         .Include(commentReact => commentReact.QuranHubUser)
                                                                         .Where(CommentReact => CommentReact.CommentId == postId)
                                                                         .AsQueryable()
                                                                         .Skip(offset)
                                                                         .Take(amount)
                                                                         .ToListAsync();
            return CommentReacts;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<List<PostReact>> GetMorePostReactsAsync(int postId, int offset, int amount)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<List<PostShare>> GetMorePostSharesAsync(int postId, int offset, int amount)
    {
        try
        {
            List<PostShare> Shares = await _identityDataContext.PostShares
                                                           .Include(share => share.QuranHubUser)
                                                           .Where(Share => Share.PostId == postId)
                                                           .AsQueryable()
                                                           .Skip(offset)
                                                           .Take(amount)
                                                               .ToListAsync();
            return Shares;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }



    public async Task<Post> EditPostAsync(Post post)
    {
        try
        {
            Post targetpost = await this._identityDataContext.Posts.FindAsync(post.PostId);

            targetpost.Text = post.Text;
            targetpost.VerseId = post.VerseId;
            targetpost.Privacy = post.Privacy;

            await _identityDataContext.SaveChangesAsync();

            return targetpost;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<bool> DeletePostAsync(int postId)
    {
        try
        {

            bool deleted = false;

            if ((await this._identityDataContext.SharedPosts.FindAsync(postId)) != null)
            {
                SharedPost sharedPost = await this._identityDataContext.SharedPosts.Include(post => post.PostReacts)
                                                                                   .Include(post => post.PostComments)
                                                                                   .Include(post => post.PostReactNotifications)
                                                                                   .Include(post => post.PostCommentNotifications)
                                                                                   .Include(post => post.PostCommentReactNotifications)
                                                                                   .Include(post => post.PostShare)
                                                                                   .ThenInclude(postshare => postshare.PostShareNotification)
                                                                                   .FirstAsync(post => post.PostId == postId);

                PostShare share = await this._identityDataContext.PostShares
                                                                 .Include(Share => Share.ShareablePost)
                                                                 .Include(Share => Share.PostShareNotification)
                                                                 .FirstAsync(share => share.ShareId == sharedPost.PostShareId);

                share.ShareablePost.SharesCount--;


                EntityEntry<SharedPost> postEntityEntry = this._identityDataContext.SharedPosts.Remove(sharedPost);

                if (postEntityEntry.State.Equals(EntityState.Deleted))
                {
                    deleted = true;
                }
            }
            else
            {
                ShareablePost shareablePost = await this._identityDataContext.ShareablePosts.Include(post => post.PostReacts)
                                                                                            .Include(post => post.PostComments)
                                                                                            .Include(post => post.PostReactNotifications)
                                                                                            .Include(post => post.PostCommentNotifications)
                                                                                            .Include(post => post.PostCommentReactNotifications)
                                                                                            .Include(post => post.PostShareNotifications)
                                                                                            .Include(post => post.PostShares)
                                                                                            .FirstAsync(post => post.PostId == postId);



                foreach(var PostShare in  shareablePost.PostShares)
                {
                    SharedPost  curSharedPost =  await this._identityDataContext.SharedPosts.Include(post => post.PostReacts)
                                                                                            .Include(post => post.PostComments)
                                                                                            .Include(post => post.PostReactNotifications)
                                                                                            .Include(post => post.PostCommentNotifications)
                                                                                            .Include(post => post.PostCommentReactNotifications)
                                                                                            .Include(post => post.PostShare)
                                                                                            .ThenInclude(postshare => postshare.PostShareNotification)
                                                                                            .FirstAsync(post => post.PostShareId == PostShare.ShareId);

                    EntityEntry<SharedPost> curSharedPostEntityEntry = this._identityDataContext.SharedPosts.Remove(curSharedPost);

                }


                EntityEntry<ShareablePost> postEntityEntry = this._identityDataContext.ShareablePosts.Remove(shareablePost);

                if (postEntityEntry.State.Equals(EntityState.Deleted))
                {
                    deleted = true;
                }

            }


            await _identityDataContext.SaveChangesAsync();

            return deleted;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }
    }

    public async Task<Tuple<PostReact, PostReactNotification>> AddPostReactAsync(PostReact postReact, QuranHubUser user)
    {
        try
        {
            Post post = await this._identityDataContext.Posts
                                                   .Include(post => post.QuranHubUser)
                                                   .FirstAsync(post => post.PostId == postReact.PostId);

            PostReact insertedPostReact = post.AddPostReact(user.Id);

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
               reactNotification = post.AddPostReactNotifiaction(user, insertedPostReact.ReactId);

               await this._identityDataContext.SaveChangesAsync();
            }

            return new Tuple<PostReact, PostReactNotification>(insertedPostReact, reactNotification);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<bool> RemovePostReactAsync(int postId, QuranHubUser user)
    {
        try
        {
            Post post = this._identityDataContext.Posts.Find(postId);

            PostReact postReact = await this._identityDataContext.PostReacts
                                                                 .Where(postReact => post.PostId == postReact.PostId && postReact.QuranHubUserId == user.Id)
                                                                 .Include(postReact => postReact.PostReactNotification)
                                                                 .FirstAsync();

            post.RemovePostReact(postReact.ReactId);

            await this._identityDataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }
    }

    public async Task<Tuple<PostComment, PostCommentNotification>> AddPostCommentAsync(PostComment comment, QuranHubUser user)
    {
        try
        {
            Post post = await this._identityDataContext.Posts
                                                   .Include(post => post.QuranHubUser)
                                                   .FirstAsync(post => post.PostId == comment.PostId);

            PostComment insertedComment = post.AddPostComment(user.Id, comment.Text, comment.VerseId);

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
                insertedComment = await this.GetPostCommentByIdAsync(insertedComment.CommentId);
            }

            this._identityDataContext.Comments.Attach(insertedComment);

            PostCommentNotification commentNotification = new();

            if (post.QuranHubUserId != user.Id)
            {
                commentNotification = post.AddPostCommentNotifiaction(user, insertedComment.CommentId);

                await this._identityDataContext.SaveChangesAsync();
            }

            return new Tuple<PostComment, PostCommentNotification>( insertedComment, commentNotification);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<bool> RemovePostCommentAsync(int commentId)
    {
        try
        {
            PostComment comment = await this._identityDataContext.PostComments
                                                             .Include(comment => comment.PostCommentReacts)
                                                             .Include(comment => comment.PostCommentReactNotifications)
                                                             .FirstAsync(comment => comment.CommentId == commentId );

            Post post = this._identityDataContext.Posts.Find(comment.PostId);

            post.RemovePostComment(comment.CommentId);
                                                  

            await this._identityDataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }
    }

    public async Task<Tuple<PostCommentReact, PostCommentReactNotification>> AddPostCommentReactAsync(PostCommentReact commentReact, QuranHubUser user)
    {
        try
        {
            PostComment comment = await this._identityDataContext.PostComments
                                                             .Include(comment => comment.QuranHubUser)
                                                             .Include(comment => comment.Post)
                                                             .FirstAsync(comment => comment.CommentId ==  commentReact.CommentId);

            PostCommentReact insertedCommentReact = comment.AddPostCommentReact(user.Id);
        

            if(comment.QuranHubUserId  != user.Id)
            {
                Follow follow = await this._identityDataContext.Follows
                                                               .Where(follow => follow.FollowedId == comment.QuranHubUserId && follow.FollowerId == user.Id)
                                                               .FirstAsync(); 


                follow.Likes++;                                                     
            }

            await this._identityDataContext.SaveChangesAsync();
            Console.WriteLine(insertedCommentReact.ReactId);

            this._identityDataContext.PostCommentReacts.Attach(insertedCommentReact);

            PostCommentReactNotification commentReactNotification = new();

            if (comment.QuranHubUserId != user.Id)
            {
                commentReactNotification = comment.AddPostCommentReactNotifiaction(user, insertedCommentReact.ReactId);

                await this._identityDataContext.SaveChangesAsync();
            }

            return new Tuple<PostCommentReact, PostCommentReactNotification> (insertedCommentReact, commentReactNotification);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<bool> RemovePostCommentReactAsync(int commentId, QuranHubUser user)
    {
        try
        {
            PostComment comment =  await this._identityDataContext.PostComments.FindAsync(commentId);

            PostCommentReact CommentReact = await this._identityDataContext.PostCommentReacts
                                                                       .Include(postCommentReact => postCommentReact.PostCommentReactNotification)
                                                                       .Where(postCommentReact => postCommentReact.CommentId == comment.CommentId && postCommentReact.QuranHubUserId == user.Id)
                                                                       .FirstAsync();

            comment.RemovePostCommentReact(CommentReact.ReactId);

            await this._identityDataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }
    }
    public async Task<Tuple<PostShare, PostShareNotification>> SharePostAsync(SharedPost sharedPost, QuranHubUser user)
    {
        try
        {

            ShareablePost post = await this._identityDataContext.ShareablePosts
                                                            .Include(post => post.QuranHubUser)
                                                            .FirstAsync(post => post.PostId == sharedPost.PostShare.PostId);

            PostShare insertedShare = post.AddPostShare(user.Id);

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

            sharedPost.PostShare = insertedShare;

            await this._identityDataContext.SharedPosts.AddAsync(sharedPost);

            await this._identityDataContext.SaveChangesAsync();

            this._identityDataContext.SharedPosts.Attach(sharedPost);

            PostShareNotification shareNotification = new();

            if (post.QuranHubUserId != user.Id)
            {
                shareNotification = post.AddPostShareNotification(user, insertedShare.ShareId);

                await this._identityDataContext.SaveChangesAsync();
            }

       

            return new Tuple<PostShare, PostShareNotification>(insertedShare, shareNotification);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }
    public async Task<List<ShareablePost>> SearchShareablePostsAsync(string keyword)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<List<SharedPost>> SearchSharedPostsAsync(string keyword)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<List<Verse>> GetVersesAsync()
    {
        try
        {
            return await this._identityDataContext.Verses.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

}
