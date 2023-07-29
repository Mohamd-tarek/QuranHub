
using System.Reflection.Metadata;

namespace QuranHub.DAL.Database
{
    public class IdentityDataContext : IdentityDbContext<QuranHubUser>
    {
        public  IdentityDataContext(DbContextOptions<IdentityDataContext> opts)
            : base(opts) { }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Verse> Verses { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<ShareablePost> ShareablePosts { get; set; }
        public DbSet<SharedPost> SharedPosts { get; set; }
        public DbSet<React> Reacts { get; set; }
        public DbSet<PostReact> PostReacts { get; set; }
        public DbSet<CommentReact> CommentReacts { get; set; }
        public DbSet<PostCommentReact> PostCommentReacts { get; set; }
        public DbSet<PostReactNotification> PostReactNotifications { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<CommentNotification> CommentNotifications { get; set; }
        public DbSet<PostCommentNotification> PostCommentNotifications { get; set; }
      
        public DbSet<CommentReactNotification> CommentReactNotifications { get; set; }
        
        public DbSet<PostCommentReactNotification> PostCommentReactNotifications { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<ShareNotification> ShareNotifications { get; set; }
        public DbSet<PostShare> PostShares { get; set; }
        public DbSet<PostShareNotification> PostShareNotifications { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<FollowNotification> FollowNotifications { get; set; }
        public DbSet<PrivacySetting> PrivacySettings {get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Follow>(entity =>
            {
                entity.HasOne(d => d.Follower)
                      .WithMany(p => p.Following)
                      .HasForeignKey(d => d.FollowerId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Following_QuranHubUsers");

                entity.HasOne(d => d.Followed)
                      .WithMany(p => p.Followers)
                      .HasForeignKey(d => d.FollowedId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Followed_QuranHubUsers");
            });


            modelBuilder.Entity<PostReact>(entity =>
            {
                entity.HasOne(d => d.Post)
                      .WithMany(p => p.PostReacts)
                      .HasForeignKey(d => d.PostId)
                      .OnDelete(DeleteBehavior.ClientCascade) // to avoid  multiple cascade paths
                      .HasConstraintName("FK_PostReact_Post_PostId");
            });

           

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasOne(d => d.TargetUser)
                      .WithMany(p => p.TargetNotifications)
                      .HasForeignKey(d => d.TargetUserId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Target_QuranHubUsers");

                entity.HasOne(d => d.SourceUser)
                      .WithMany(p => p.SourceNotifications)
                      .HasForeignKey(d => d.SourceUserId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Source_QuranHubUsers");
            });

            modelBuilder.Entity<ShareNotification>(entity =>
            {
                entity.HasOne(d => d.Share)
                      .WithOne(p => p.ShareNotification)
                      .HasForeignKey<ShareNotification>(d => d.ShareId)
                      .OnDelete(DeleteBehavior.ClientCascade) // to avoid  multiple cascade paths
                      .HasConstraintName("FK_ShareNotification_Share_ShareId");
            });

            modelBuilder.Entity<PostShareNotification>(entity =>
            {
                entity.HasOne(d => d.PostShare)
                      .WithOne(p => p.PostShareNotification)
                      .HasForeignKey<PostShareNotification>(d => d.PostShareId)
                      .OnDelete(DeleteBehavior.ClientCascade) // to avoid  multiple cascade paths
                      .HasConstraintName("FK_PostShareNotification_PostShare_PostShareId");
            });
            modelBuilder.Entity<PostShareNotification>(entity =>
            {
                entity.HasOne(d => d.Post)
                      .WithMany(p => p.PostShareNotifications)
                      .HasForeignKey(d => d.PostId)
                      .OnDelete(DeleteBehavior.ClientCascade) // to avoid  multiple cascade paths
                      .HasConstraintName("FK_PostShareNotification_ShareablePost_PostId");
            });


            modelBuilder.Entity<CommentNotification>(entity =>
            {
                entity.HasOne(d => d.Comment)
                      .WithOne(p => p.CommentNotification)
                      .HasForeignKey<CommentNotification>(d => d.CommentId)
                      .OnDelete(DeleteBehavior.Cascade) // to avoid  multiple cascade paths
                      .HasConstraintName("FK_CommentNotification_Comment_CommentId");
            });


            modelBuilder.Entity<PostCommentNotification>(entity =>
            {
                entity.HasOne(d => d.PostComment)
                      .WithOne(p => p.PostCommentNotification)
                      .HasForeignKey<PostCommentNotification>(d => d.PostCommentId)
                      .OnDelete(DeleteBehavior.ClientCascade) // to avoid  multiple cascade paths
                      .HasConstraintName("FK_PostCommentNotification_PostComment_CommentId");
            });

            modelBuilder.Entity<PostCommentNotification>(entity =>
            {
                entity.HasOne(d => d.Post)
                      .WithMany(p => p.PostCommentNotifications)
                      .HasForeignKey(d => d.PostId)
                      .OnDelete(DeleteBehavior.ClientCascade) // to avoid  multiple cascade paths
                      .HasConstraintName("FK_PostCommentNotification_Post_PostId");
            });

            
            modelBuilder.Entity<ReactNotification>(entity =>
            {
                entity.HasOne(d => d.React)
                      .WithOne(p => p.ReactNotification)
                      .HasForeignKey<ReactNotification>(d => d.ReactId)
                      .OnDelete(DeleteBehavior.ClientCascade) // to avoid  multiple cascade paths
                      .HasConstraintName("FK_ReactNotification_React_ReactId");
            });

            modelBuilder.Entity<PostReactNotification>(entity =>
            {
                entity.HasOne(d => d.PostReact)
                      .WithOne(p => p.PostReactNotification)
                      .HasForeignKey<PostReactNotification>(d => d.PostReactId)
                      .OnDelete(DeleteBehavior.ClientCascade) // to avoid  multiple cascade paths
                      .HasConstraintName("FK_PostReactNotification_PostReact_PostReactId");
            });

            modelBuilder.Entity<PostReactNotification>(entity =>
            {
                entity.HasOne(d => d.Post)
                      .WithMany(p => p.PostReactNotifications)
                      .HasForeignKey(d => d.PostId)
                      .OnDelete(DeleteBehavior.ClientCascade) // to avoid  multiple cascade paths
                      .HasConstraintName("FK_PostReactNotification_Post_PostId");
            });

            modelBuilder.Entity<CommentReactNotification>(entity =>
            {
                entity.HasOne(d => d.Comment)
                      .WithMany(p => p.CommentReactNotifications)
                      .HasForeignKey(d => d.CommentId)
                      .OnDelete(DeleteBehavior.ClientCascade) // to avoid  multiple cascade paths
                      .HasConstraintName("FK_CommentReactNotification_Comment_CommentId");
            });

            modelBuilder.Entity<CommentReactNotification>(entity =>
            {
                entity.HasOne(d => d.CommentReact)
                      .WithOne(p => p.CommentReactNotification)
                      .HasForeignKey<CommentReactNotification>(d => d.CommentReactId)
                      .OnDelete(DeleteBehavior.ClientCascade) // to avoid  multiple cascade paths
                      .HasConstraintName("FK_CommentReactNotification_CommentReact_CommentReactId");
            });

            modelBuilder.Entity<PostCommentReactNotification>(entity =>
            {
                entity.HasOne(d => d.Post)
                      .WithMany(p => p.PostCommentReactNotifications)
                      .HasForeignKey(d => d.PostId)
                      .OnDelete(DeleteBehavior.ClientCascade) // to avoid  multiple cascade paths
                      .HasConstraintName("FK_PostCommentReactNotification_Post_PostId");
            });

            modelBuilder.Entity<PostCommentReactNotification>(entity =>
            {
                entity.HasOne(d => d.PostCommentReact)
                      .WithOne(p => p.PostCommentReactNotification)
                      .HasForeignKey<PostCommentReactNotification>(d => d.PostCommentReactId)
                      .OnDelete(DeleteBehavior.ClientCascade) // to avoid  multiple cascade paths
                      .HasConstraintName("FK_PostCommentReactNotification_PostCommentReact_PostCommentReactId");
            });

        }
    }
}
