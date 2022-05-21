using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace ServerApp {

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
            logger.LogDebug(context.Request.Path.ToString()) ;
            if (next != null) {
                await next(context);
            }
        }
    }
}
    

   