using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace ServerApp.Pages.Identity {

    [Authorize]
    public class UserPageModel : PageModel {

        // no methods or properties required
    }
}
