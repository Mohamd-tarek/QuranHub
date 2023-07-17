
namespace QuranHub.DAL.Database;
using MediaInfo;
public class VideoSeedData
{
    static string baseDir = Directory.GetCurrentDirectory();
    static string videosDir = Directory.GetParent(baseDir) + @"\QuranHub.Web\wwwroot\1";
    public static async Task  SeedDatabaseAsync(IServiceProvider provider)
    {
        provider.GetRequiredService<VideoContext>().Database.Migrate();

        VideoContext VideoContext = provider.GetRequiredService<VideoContext>();

        if (VideoContext.PlayListsInfo.Count() == 0 )
        {
            await SeedPlayListsInfoAsync(VideoContext);
        }
    } 

    public static async Task SeedPlayListsInfoAsync(VideoContext videoContext )
    {

        var thumbnailsfiles = Directory.GetFiles(videosDir + "/thumbnails");
        PlayListInfo playListInfo = new PlayListInfo
        {
            Name = "العلم والايمان ",
            ThumbnailImage =  File.ReadAllBytes(thumbnailsfiles[0]),
            NumberOfVideos = thumbnailsfiles.Length
        };

        await videoContext.SaveChangesAsync();
        
        var files = Directory.GetFiles(videosDir);

        foreach(var file in files)
        {
            await SeedVideoInfoAsync(videoContext, playListInfo, file);
        }

        await videoContext.SaveChangesAsync();
          
       
    }

    public static async Task  SeedVideoInfoAsync(VideoContext VideoContext, PlayListInfo playListInfo, string path ) 
    {
        Console.WriteLine("path : " + path);
        var mediaInfo = new MediaInfoWrapper(path);
        string name = Path.GetFileNameWithoutExtension(path);
        var videoInfo = new VideoInfo
        {
            ThumbnailImage = File.ReadAllBytes(path + "/thumbnails/" + name + ".jpeg"),
            Name = name,
            Type = mediaInfo.Format,
            Size = (int)mediaInfo.Size,
            Duration = mediaInfo.Duration,
            Width = mediaInfo.Width,
            Height = mediaInfo.Height,
            Path = path,
            PlayListInfoId = playListInfo.PlayListInfoId
        };

       await VideoContext.VideosInfo.AddAsync(videoInfo);
        
    } 
    
      
}




