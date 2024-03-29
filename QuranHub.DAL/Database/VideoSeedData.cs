﻿
namespace QuranHub.DAL.Database;
using MediaInfo;
public class VideoSeedData
{
    static string baseDir = Directory.GetCurrentDirectory();
    static string thumbnailsDir = Directory.GetParent(baseDir) + @"\QuranHub.DAL\Database\SeedData\Thumbnails";
    static string videosDir = Directory.GetParent(baseDir) + @"\QuranHub.Web\wwwroot\files";

    public static async Task  SeedDatabaseAsync(IServiceProvider provider)
    {
        provider.GetRequiredService<IdentityDataContext>().Database.Migrate();

        IdentityDataContext IdentityDataContext = provider.GetRequiredService<IdentityDataContext>();

        if (IdentityDataContext.PlayListsInfo.Count() == 0 )
        {
            await SeedPlayListsInfoAsync(IdentityDataContext);
        }
    } 

    public static async Task SeedPlayListsInfoAsync(IdentityDataContext IdentityDataContext )
    {
        try
        {
            var thumbnailsfiles = Directory.GetFiles(thumbnailsDir);
            PlayListInfo playListInfo = new PlayListInfo
            {
                Name = "العلم والايمان ",
                ThumbnailImage =  File.ReadAllBytes(thumbnailsfiles[0]),
                NumberOfVideos = thumbnailsfiles.Length
            };

            await IdentityDataContext.PlayListsInfo.AddAsync(playListInfo);

            await IdentityDataContext.SaveChangesAsync();

            var videosFiles = Directory.GetFiles(videosDir);

            foreach (var file in videosFiles)
            {
                await SeedVideoInfoAsync(IdentityDataContext, playListInfo, file);
            }

            await IdentityDataContext.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            return;
        }

    }

    public static async Task  SeedVideoInfoAsync(IdentityDataContext IdentityDataContext, PlayListInfo playListInfo, string path ) 
    {
        try
        {
            var mediaInfo = new MediaInfo();

            mediaInfo.Open(path);

            string name = Path.GetFileNameWithoutExtension(path);

            string dirctory = Path.GetDirectoryName(path);

            var videoInfo = new VideoInfo
            {
                ThumbnailImage = File.ReadAllBytes(thumbnailsDir + @"\" + name + ".jpeg" ),
                Name = name,
                Type = mediaInfo.Get(StreamKind.Video, 0, "Format"),
                Duration = TimeSpan.FromMilliseconds(int.Parse(mediaInfo.Get(StreamKind.Video, 0, "Duration"))),
                Width = int.Parse(mediaInfo.Get(StreamKind.Video, 0, "Width")),
                Height = int.Parse(mediaInfo.Get(StreamKind.Video, 0, "Height")),
                Path = "https://localhost:7046/video/" + name,
                PlayListInfoId = playListInfo.PlayListInfoId
            };

           await IdentityDataContext.VideosInfo.AddAsync(videoInfo);
        }
        catch (Exception ex)
        {
            return;
        }
        
    } 
    
      
}




