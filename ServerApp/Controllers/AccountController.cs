using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Collections.Generic;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
       private UserManager<IdentityUser> userManager;
       private SignInManager<IdentityUser> signInManager;

        public AccountController(  UserManager<IdentityUser> userMgr,
                                   SignInManager<IdentityUser> signInMgr)
        {
           userManager = userMgr;
           signInManager = signInMgr;
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginViewModel creds){
            if(ModelState.IsValid && await DoLogin(creds)){
                this.updateState();
               return Ok("true");
            }
            return BadRequest();
        }

        private void updateState(){
            Dictionary<string, object> state = this.Deserialize(HttpContext.Session.GetString("state"));
            state["authenticated"] = true;
            this.Serialize(state);
        }

        private Dictionary<string, object> Deserialize(string state){
                 var  options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };
                return JsonSerializer.Deserialize<Dictionary<string, object>>(state); 
        }

         public void Serialize(Dictionary<string, object> state){
          var  options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };
            var jsonData = JsonSerializer.Serialize(state, options);
            HttpContext.Session.SetString("state", jsonData);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> logout(){
            await signInManager.SignOutAsync();
            return Ok("true");
        }

        private async Task<bool> DoLogin(LoginViewModel creds){
            
               await signInManager.SignOutAsync();
               Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(creds.Email, creds.Password, false, false);
               return result.Succeeded;
        }
    }

    public class LoginViewModel {
        [Required]
        public string Email{ get; set;}
        [Required]
        public string Password{ get; set;}
    }
}
