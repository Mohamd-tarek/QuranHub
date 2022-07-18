using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;


namespace ServerApp.Pages.Identity {

    [AllowAnonymous]
    public class SignOutModel : UserPageModel {

        public SignOutModel(SignInManager<IdentityUser> signMgr)
            => SignInManager = signMgr;

        public SignInManager<IdentityUser> SignInManager { get; set; }

        public async Task<IActionResult> OnPostAsync() {
            this.updateState();
            await SignInManager.SignOutAsync();
            return RedirectToPage();
        }
        
        private void updateState(){
            Dictionary<string, object> state = this.Deserialize(HttpContext.Session.GetString("state"));
            state["authenticated"] = false;
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
    }
}
