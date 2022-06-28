using Microsoft.AspNetCore.Mvc;

namespace ServerApp.Pages.Identity {

    public class SignInCodesWarningModel : UserPageModel {

        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; } = "/";
    }
}
