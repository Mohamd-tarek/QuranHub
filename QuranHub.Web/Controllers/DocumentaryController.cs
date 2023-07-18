
namespace QuranHub.Web.Controllers;

[Route("api/[controller]")]
public class DocumentaryController : ControllerBase
{
    private ILogger<DocumentaryController> _logger;
    private IDocumentaryRepository _documentaryRepository;
    private readonly IWebHostEnvironment _env;

    public DocumentaryController(
        ILogger<DocumentaryController> logger,
        IDocumentaryRepository documentaryRepository,
        IWebHostEnvironment env)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
        _documentaryRepository = documentaryRepository ?? throw new ArgumentNullException(nameof(documentaryRepository));
        _env = env ?? throw new ArgumentNullException(nameof(env));
    }

    [HttpGet("PlayListsInfo")]
    public async Task<IEnumerable<PlayListInfo>> GetPlayListsInfoAsync() 
    {
        return await  this._documentaryRepository.GetPlayListsAsync();
    }

    [HttpGet("PlayListInfo/{PlaylistName}")]
    public async Task<PlayListInfo> GetPlayListsInfoAsync(string PlaylistName)
    {
        return await this._documentaryRepository.GetPlayListByNameAsync(PlaylistName);
    }

    [HttpGet("VideoInfoForPlayList/{playListName}/{offset}/{amount}")]
    public async Task<IEnumerable<VideoInfo>> GetVideoInfoForPlayList(string playListName, int offset = 0, int amount = 20) 
    {
        return await  this._documentaryRepository.GetVideoInfoForPlayListAsync(playListName, offset, amount);
    }

    [HttpGet("VideoInfo/{name}")]
    public async Task<VideoInfo> GetVideoInfoAsync(string name) 
    {
        return await  this._documentaryRepository.GetVideoInfoByNameAsync(name);
    }
}
