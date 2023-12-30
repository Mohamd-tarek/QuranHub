
namespace QuranHub.Web.Controllers;

[Route("api/[controller]")]
public class SessionController : ControllerBase
{
    private readonly Serilog.ILogger _logger;

    public SessionController(Serilog.ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 

    }

    [HttpGet("state")]
    public ActionResult GetState()
    {
        try
        {
            return Ok(HttpContext.Session.GetString("state"));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost("state")]
    public ActionResult StoreState([FromBody] Dictionary<string, object> state)
    {
        try
        {
            HttpContext.Session.SetSession("state", state);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }
}
