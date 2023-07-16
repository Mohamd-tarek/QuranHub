
namespace QuranHub.Web.Controllers;

[Route("api/[controller]")]
public  class AnalysisController : ControllerBase
{
    private ILogger<AnalysisController> _logger;
    private IQuranRepository _quranRepository;
    private IMemoryCache _cache;
    private AnalysisService _analysis;

    public  AnalysisController(
        ILogger<AnalysisController> logger,
        IQuranRepository quranRepository,
        AnalysisService analysis,
        IMemoryCache memoryCache)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
        _quranRepository = quranRepository ?? throw new ArgumentNullException(nameof(quranRepository)); 
        _cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        _analysis = analysis ?? throw new ArgumentNullException(nameof(analysis));
    }

    [HttpGet("topics")]
    public List<List<QuranClean>> GroupMainTopics()
    {
        return _analysis.GroupMainTopics();
    }

    [HttpGet("{id}")]
    public IEnumerable<QuranClean> GetSimilarAyas(long id) 
    {
        return _analysis.GetSimilarAyas(id);
    }

    [HttpGet("uniques")]
    public IEnumerable<QuranClean> GetUniqueAyas() 
    {            
        List<QuranClean> ans;

        if (this._cache.TryGetValue("uniques", out ans))
        {
            return ans;
        }

        ans = _analysis.GetUniqueAyas();

        this._cache.Set("uniques" , ans);
        
        return ans;
    } 
}
