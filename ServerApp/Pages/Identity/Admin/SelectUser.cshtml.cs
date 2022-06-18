using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;


namespace ServerApp.Pages.Identity.Admin {

    public class SelectUserModel : AdminPageModel {

        public SelectUserModel(UserManager<IdentityUser> mgr, ILogger<SelectUserModel> _logger){
             UserManager = mgr;
             logger = _logger;
        }

        public UserManager<IdentityUser> UserManager { get; set; }
        public IEnumerable<IdentityUser> Users { get; set; }
        public ILogger<SelectUserModel> logger;

        [BindProperty(SupportsGet = true)]
        public string Label { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Callback { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

        public void OnGet() {
            Users = UserManager.Users
                .Where(u => Filter == null || u.UserName.Contains(Filter))
                .OrderBy(u => u.Email).ToList();
        }

        public IActionResult OnPost() => RedirectToPage(new { Filter, Callback });
    }
}
