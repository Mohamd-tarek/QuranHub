using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ServerApp.Controllers
{
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

        [HttpGet]
        public IActionResult login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> login(LoginViewModel creds, string returnURL){
            if(ModelState.IsValid){
                if(await DoLogin(creds))
                {
                    return Redirect(returnURL ?? "/");
                }else{
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }
            return View(creds);
        }

        [HttpPost]
        public async Task<IActionResult> logout(string returnURL){
            await signInManager.SignOutAsync();
            return Redirect(returnURL ?? "/");
        }

        [HttpPost("/api/account/login")]
        public async Task<IActionResult> login([FromBody] LoginViewModel creds){
            if(ModelState.IsValid && await DoLogin(creds)){
               return Ok("true");
            }
            return BadRequest();
        }

        [HttpPost("/api/account/logout")]
        public async Task<IActionResult> logout(){
            await signInManager.SignOutAsync();
            return Ok("true");
        }

        private async Task<bool> DoLogin(LoginViewModel creds){
            IdentityUser user = await userManager.FindByNameAsync(creds.Name);
            if(user != null){
               await signInManager.SignOutAsync();
               Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, creds.Password, false, false);
               return result.Succeeded;
            }
            return false;
        }
    }

    public class LoginViewModel {
        [Required]
        public string Name{ get; set;}
        [Required]
        public string Password{ get; set;}
    }
}
