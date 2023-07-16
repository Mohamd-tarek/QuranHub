namespace QuranHub.Web.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class NotificationHub : Hub
{
    private static Dictionary<string, string> UsersToConnections = new();
    private UserManager<QuranHubUser> _userManager;
    private HttpContext _httpContext;

    public NotificationHub(
        UserManager<QuranHubUser> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContext = httpContextAccessor.HttpContext;
    }

    public override async Task OnConnectedAsync()
    {
        var user = await _userManager.GetUserAsync(this._httpContext.User);

        user.Online = true;

        user.ConnectionId = Context.ConnectionId;

        UsersToConnections[Context.ConnectionId] = user.Id;

        await this._userManager.UpdateAsync(user);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = UsersToConnections[Context.ConnectionId];

        QuranHubUser user = await this._userManager.FindByIdAsync(userId);

        user.Online = false;

        user.ConnectionId = null;

        await this._userManager.UpdateAsync(user);

        await base.OnDisconnectedAsync(exception);
    }
}
