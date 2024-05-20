
namespace QuranHub.Web.Controllers;

[ApiController]
public class QuranController : ControllerBase
{
    private readonly Serilog.ILogger _logger;
    private IQuranRepository _quranRepository;
    private UserManager<QuranHubUser> _userManager;
    private HttpContext _httpContext;
    private QuranHubUser _currentUser;

    public QuranController(
        Serilog.ILogger logger,
        IQuranRepository quranRepository,
        UserManager<QuranHubUser> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
        _quranRepository = quranRepository ?? throw new ArgumentNullException(nameof(quranRepository));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor));

        ClaimsPrincipal claimsPrincipal = this._httpContext.User;

        if (claimsPrincipal.Identity.IsAuthenticated)
        {
            _currentUser = _userManager.GetUserAsync(this._httpContext.User).Result;
        }
    }

    [HttpGet(Router.Quran.QuranInfo)]
    public ActionResult<IEnumerable<object>> GetQuranInfo(string type) 
    {
        try
        {
            return Ok(_quranRepository.GetQuranInfo(type));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Quran.MindMap)]
    public async Task<ActionResult<byte[]>> GetMindMap(long id) 
    {
        try
        {
            return Ok(await _quranRepository.GetMindMap(id));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet(Router.Quran.Note)]
    public async Task<ActionResult<Note>> GetNote(long index)
    {
        try
        {
            return Ok(await _quranRepository.GetNote(index, _currentUser));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost(Router.Quran.CreateNote)]
    public async  Task<ActionResult> CreateNote([FromBody] Note note)
    {
        try
        {
            if ( await _quranRepository.AddNote(note, _currentUser))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

}
