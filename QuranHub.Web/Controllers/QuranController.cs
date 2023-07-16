
namespace QuranHub.Web.Controllers;

[Route("api/[controller]")]
public class QuranController : ControllerBase
{
    private ILogger<QuranController> _logger;
    private IQuranRepository _quranRepository;
    private UserManager<QuranHubUser> _userManager;
    private HttpContext _httpContext;
    private QuranHubUser _currentUser;

    public QuranController(
        ILogger<QuranController> logger,
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

    [HttpGet("QuranInfo/{type}")]
    public IEnumerable<object> GetQuranInfo(string type) 
    {
        return _quranRepository.GetQuranInfo(type);
    }

    [HttpGet("MindMap/{id}")]
    public async Task<byte[]> GetMindMap(long id) 
    {
        return await _quranRepository.GetMindMap(id);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("Note/{index}")]
    public async Task<Note> GetNote(long index)
    {
        return await _quranRepository.GetNote(index, _currentUser);  
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("Note")]
    public async  Task<IActionResult> CreateNote([FromBody] Note note)
    {
        _logger.LogDebug(note.Index,ToString());


        if( await _quranRepository.AddNote(note, _currentUser))
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }   
    }

}
