using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;



namespace ServerApp.Pages.Identity {

    public class IndexModel : UserPageModel {

        public IndexModel(UserManager<IdentityUser> userMgr)
            => UserManager = userMgr;

        public UserManager<IdentityUser> UserManager { get; set; }

        
        public IdentityUser IdentityUser { get; set; }

        public IEnumerable<string> PropertyNames
            => typeof(IdentityUser).GetProperties()
                .Select(prop => prop.Name);

        public string GetValue(string name) =>
            typeof(IdentityUser).GetProperty(name)
                 .GetValue(IdentityUser)?.ToString();

       
        public async Task OnGetAsync() {
            IdentityUser = await UserManager.GetUserAsync(User);
         
        }
    }
}
