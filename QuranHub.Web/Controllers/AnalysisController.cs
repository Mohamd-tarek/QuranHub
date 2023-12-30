
namespace QuranHub.Web.Controllers;

[Route("api/[controller]")]
public  class AnalysisController : ControllerBase
{
    private readonly Serilog.ILogger _logger;
    private IQuranRepository _quranRepository;
    private IMemoryCache _cache;
    private AnalysisService _analysis;

    public  AnalysisController(
        Serilog.ILogger logger,
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
    public ActionResult<List<List<QuranClean>>> GroupMainTopics()
    {
        try
        {
            return Ok(_analysis.GroupMainTopics());
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet("{id}")]
    public ActionResult<IEnumerable<QuranClean>> GetSimilarAyas(long id) 
    {
        try
        {
            return Ok(_analysis.GetSimilarAyas(id));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet("uniques")]
    public ActionResult<IEnumerable<QuranClean>> GetUniqueAyas() 
    {
        try
        {
            List<QuranClean> ans;

            if (this._cache.TryGetValue("uniques", out ans))
            {
                return Ok(ans);
            }

            ans = _analysis.GetUniqueAyas();

            this._cache.Set("uniques" , ans);
        
            return Ok(ans);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    } 
}
