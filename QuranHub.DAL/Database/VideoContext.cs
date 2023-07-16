

namespace QuranHub.DAL.Database;

public class VideoContext : DbContext
{
    public  VideoContext(DbContextOptions<VideoContext> opts) : base(opts)
    { }
    public DbSet<VideoInfo> VideosInfo { get; set; }
    public DbSet<PlayListInfo> PlayListsInfo { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
}
