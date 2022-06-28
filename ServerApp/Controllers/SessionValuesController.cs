using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
//using Newtonsoft.Json;
using ServerApp.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

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
        public void StoreState([FromBody] Dictionary<string, object> state){
          var  options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };
            var jsonData = JsonSerializer.Serialize(state, options);
            logger.LogDebug(jsonData);
            HttpContext.Session.SetString("state", jsonData);
        }
    }
}
