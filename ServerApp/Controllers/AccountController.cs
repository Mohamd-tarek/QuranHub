﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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
               return Ok("true");
            }
            return BadRequest();
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
