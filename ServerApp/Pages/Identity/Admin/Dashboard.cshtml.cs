using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace ServerApp.Pages.Identity.Admin {

    public class DashboardModel : AdminPageModel {

        public DashboardModel(UserManager<IdentityUser> userMgr)
            => UserManager = userMgr;

        public UserManager<IdentityUser> UserManager { get; set; }

        public int UsersCount { get; set; } = 0;
        public int UsersUnconfirmed { get; set; } = 0;
        public int UsersLockedout { get; set; } = 0;
        public int UsersTwoFactor { get; set; } = 0;

         private readonly string[] emails = {
            "mohamed@example.com", "ahmed@example.com", "ali@example.com"
        };

        public void OnGet(){
            UsersCount = UserManager.Users.Count();
            UsersUnconfirmed = UserManager.Users.Where(u => !u.EmailConfirmed).Count();
            UsersLockedout = UserManager.Users
                .Where(u => u.LockoutEnabled && u.LockoutEnd > System.DateTimeOffset.Now)
                .Count();
            UsersTwoFactor = UserManager.Users.Where(u => u.TwoFactorEnabled).Count();
        }

         public async Task<IActionResult> OnPostAsync() {
            foreach (IdentityUser existingUser in UserManager.Users.ToList()) {
                IdentityResult result = await UserManager.DeleteAsync(existingUser);
                result.Process(ModelState);
            }
            foreach (string email in emails) {
                IdentityUser userObject = new IdentityUser {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };
                IdentityResult result = await UserManager.CreateAsync(userObject);
                if (result.Process(ModelState)) {
                    result = await UserManager.AddPasswordAsync(userObject, "Admin123$");
                    result.Process(ModelState);
                }
            }
            if (ModelState.IsValid) {
                return RedirectToPage();
            }
            return Page();
        }

    }
}
