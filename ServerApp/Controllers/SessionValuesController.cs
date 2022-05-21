using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ServerApp.Models;
using Microsoft.Extensions.Logging;

namespace ServerApp.Controllers
{
    [Route("api/Session")]
     [ApiController]
    public class SessionValuesController : Controller
    {

       private readonly ILogger<SessionValuesController> logger;
       
       public SessionValuesController(ILogger<SessionValuesController> _logger){
             logger = _logger;
        }

        [HttpGet("state")]
        public IActionResult GetState(){
            return Ok(HttpContext.Session.GetString("state"));
        }

        [HttpPost("state")]
        public void StoreState([FromBody] State state){
            var jsonData = JsonConvert.SerializeObject(state);
            HttpContext.Session.SetString("state", jsonData);
        }
    }
}
