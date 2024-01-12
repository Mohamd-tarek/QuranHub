namespace QuranHub.Web.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class NotificationHub : Hub
{
    private static Dictionary<string, string> UsersToConnections = new();
    private UserManager<QuranHubUser> _userManager;
    private HttpContext _httpContext;
    private readonly Serilog.ILogger _logger;

    public NotificationHub(
        UserManager<QuranHubUser> userManager,
        IHttpContextAccessor httpContextAccessor,
        Serilog.ILogger logger
        )
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager)); ;
        _httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor)); ;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override async Task OnConnectedAsync()
    {
        try
        {
            var user = await _userManager.GetUserAsync(this._httpContext.User);

            user.Online = true;

            user.ConnectionId = Context.ConnectionId;

            UsersToConnections[Context.ConnectionId] = user.Id;

            await this._userManager.UpdateAsync(user);

            await base.OnConnectedAsync();
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        try
        {
            var userId = UsersToConnections[Context.ConnectionId];

            QuranHubUser user = await this._userManager.FindByIdAsync(userId);

            user.Online = false;

            user.ConnectionId = null;

            await this._userManager.UpdateAsync(user);

            await base.OnDisconnectedAsync(exception);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);

        }
    }
}
