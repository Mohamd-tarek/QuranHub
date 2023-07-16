
namespace QuranHub.Web.Services;

public class IdentityEmailService :IEmailService
{
    private IEmailSender _emailSender;
    private UserManager<QuranHubUser> _userManager;
    private IHttpContextAccessor _contextAccessor;
    private LinkGenerator _linkGenerator;
    private TokenUrlEncoderService _tokenEncoder;
    private IConfiguration _configuration;
    public IdentityEmailService(
        IEmailSender sender,
        UserManager<QuranHubUser> userManager,
        IHttpContextAccessor contextAccessor,
        LinkGenerator generator,
        TokenUrlEncoderService encoder,
        IConfiguration configuration)
    {
        _emailSender = sender ?? throw new ArgumentNullException(nameof(sender));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        _linkGenerator = generator ?? throw new ArgumentNullException(nameof(generator));
        _tokenEncoder = encoder ?? throw new ArgumentNullException(nameof(encoder));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }


    private string GetUrl(string emailAddress, string token,  string routeName)
    {
        string safeToken = _tokenEncoder.EncodeToken(token);

        return _configuration["FrontendUrls:Host"] + "/" + routeName + "?email=" + emailAddress + "&token=" + safeToken;
        
    }
    
    public async Task SendPasswordRecoveryEmail(QuranHubUser user, string confirmationURL)
    {
        string token = await _userManager.GeneratePasswordResetTokenAsync(user);

        string url = GetUrl(user.Email, token, confirmationURL);

        await _emailSender.SendEmailAsync(user.Email, "Set Your Password",
            $"Please set your password by <a href={url}>clicking here</a>.");
    }

    public async Task SendAccountConfirmEmail(QuranHubUser user, string confirmationURL)
    {
        string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        string url = GetUrl(user.Email, token, confirmationURL);

        await _emailSender.SendEmailAsync(user.Email,
            "Complete Your Account Setup",
            $"Please set up your account by <a href={url}>clicking here</a>.");
    }

    public async Task SendChangeEmailConfirmEmail(QuranHubUser user, string newEmail, string confirmationURL)
    {
        string token = await _userManager.GenerateChangeEmailTokenAsync (user, newEmail);

        string url = GetUrl(user.Email, token, confirmationURL);

        await _emailSender.SendEmailAsync(user.Email,
            "Confirm Your Email Change ",
            $"Please confirm your new email by <a href={url}>clicking here</a>.");
    }

}
