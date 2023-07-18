
namespace QuranHub.DAL.Database;
using MediaInfo;
public class VideoSeedData
{
    static string baseDir = Directory.GetCurrentDirectory();
    static string videosDir = Directory.GetParent(baseDir) + @"\QuranHub.Web\wwwroot\files";
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

        await videoContext.PlayListsInfo.AddAsync(playListInfo);

        await videoContext.SaveChangesAsync();


        var files = Directory.GetFiles(videosDir);

        foreach (var file in files)
        {
            await SeedVideoInfoAsync(videoContext, playListInfo, file);
        }

        await videoContext.SaveChangesAsync();


    }

    public static async Task  SeedVideoInfoAsync(VideoContext VideoContext, PlayListInfo playListInfo, string path ) 
    {

         var mediaInfo = new MediaInfo();
         mediaInfo.Open(path);

        string name = Path.GetFileNameWithoutExtension(path);

        string dirctory = Path.GetDirectoryName(path);

        var videoInfo = new VideoInfo
        {
            ThumbnailImage = File.ReadAllBytes(dirctory + @"\thumbnails\" + name + ".jpeg" ),
            Name = name,
            Type = mediaInfo.Get(StreamKind.Video, 0, "Format"),
            Duration = TimeSpan.FromMilliseconds(int.Parse(mediaInfo.Get(StreamKind.Video, 0, "Duration"))),
            Width = int.Parse(mediaInfo.Get(StreamKind.Video, 0, "Width")),
            Height = int.Parse(mediaInfo.Get(StreamKind.Video, 0, "Height")),
            Path = "https://localhost:7046/video/" + name,
            PlayListInfoId = playListInfo.PlayListInfoId
        };

       await VideoContext.VideosInfo.AddAsync(videoInfo);
        
    } 
    
      
}




