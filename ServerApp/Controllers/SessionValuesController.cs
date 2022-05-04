using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ServerApp.Models;

namespace ServerApp.Controllers
{
    [Route("api/Session")]
     [ApiController]
    public class SessionValuesController : Controller
    {
        
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
