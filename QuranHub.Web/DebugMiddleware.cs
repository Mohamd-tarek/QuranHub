
namespace QuranHub.Web;

public class DebugMiddleWare {
    private RequestDelegate next;
    private ILogger<DebugMiddleWare> logger;

    public DebugMiddleWare() {

    }

    public DebugMiddleWare(RequestDelegate nextDelegate, ILogger<DebugMiddleWare> _logger) {
        next = nextDelegate;
        logger = _logger;
    }

    public async Task Invoke(HttpContext context) {
        
        if (next != null) {
            await next(context);
        }
    }
}


