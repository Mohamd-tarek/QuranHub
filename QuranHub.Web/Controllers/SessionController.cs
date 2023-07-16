
namespace QuranHub.Web.Controllers;

[Route("api/[controller]")]
public class SessionController : ControllerBase
{
    private ILogger<SessionController> _logger;

    public SessionController(ILogger<SessionController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 

    }

    [HttpGet("state")]
    public IActionResult GetState()
    {
        return Ok(HttpContext.Session.GetString("state"));
    }

    [HttpPost("state")]
    public void StoreState([FromBody] Dictionary<string, object> state)
    {
      HttpContext.Session.SetSession("state", state);
    }
}
