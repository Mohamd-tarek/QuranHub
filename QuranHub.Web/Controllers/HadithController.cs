
namespace QuranHub.Web.Controllers;

[ApiController]
public class HadithController : ControllerBase
{
    private readonly Serilog.ILogger _logger;
    private IHadithRepository _hadithRepository;
    private UserManager<QuranHubUser> _userManager;
    private HttpContext _httpContext;
    private QuranHubUser _currentUser;

    public HadithController(
        Serilog.ILogger logger,
        IHadithRepository hadithRepository,
        UserManager<QuranHubUser> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _hadithRepository = hadithRepository ?? throw new ArgumentNullException(nameof(hadithRepository));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor));

        ClaimsPrincipal claimsPrincipal = this._httpContext.User;

        if (claimsPrincipal.Identity.IsAuthenticated)
        {
            _currentUser = _userManager.GetUserAsync(this._httpContext.User).Result;
        }
    }

    [HttpGet(Router.Hadith.GetAll)]
    public async Task<ActionResult<List<Section>>> GetAllHadith() 
    {
        try
        {
            return Ok(await _hadithRepository.GetAllHadiths());
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest(); 
        }
    }

    [HttpGet(Router.Hadith.GetSectionById)]
    public async Task<ActionResult<Section>> GetSectionById(int Id)
    {
        try
        {
            return Ok(await _hadithRepository.GetSectionById(Id));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Hadith.GetHadithById)]
    public async Task<ActionResult<Hadith>> GetHadithById(int Id)
    {
        try
        {
            return Ok(await _hadithRepository.GetHadithById(Id));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }





}
