 
namespace QuranHub.Web.Controllers;

[ApiController]
public partial class  ExternalAuthenticationController : ControllerBase
{
    private readonly Serilog.ILogger _logger;
    private UserManager<QuranHubUser> _userManager;
    private SignInManager<QuranHubUser> _signInManager;
    private IConfiguration _configuration;

    public ExternalAuthenticationController(
        Serilog.ILogger logger,
        UserManager<QuranHubUser> userManager,
        SignInManager<QuranHubUser> signInManager,
        IConfiguration configuration)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

    }

    [HttpGet(Router.ExternalAuthentication.ExternalSchemas)]
    public async  Task<ActionResult<IEnumerable<ExternalProviderViewModel>>> GetExternalSchemasAsync()
    {
        try
        {
            IEnumerable<AuthenticationScheme> ExternalSchemes = await  _signInManager.GetExternalAuthenticationSchemesAsync();

            return Ok(ExternalSchemes.Select(schema =>new ExternalProviderViewModel 
            {
                name = schema.Name.ToLower(),
                displayName = schema.DisplayName
            }));
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost(Router.ExternalAuthentication.LoginWithExternalProvider)]
    public ActionResult PosLoginWithExternalProviderAsync(string provider) 
    {
        try
        {
            string callbackUrl = GetUrl("auth/login/LoginExternalCallback");

            AuthenticationProperties props =
               _signInManager.ConfigureExternalAuthenticationProperties(provider, callbackUrl);

            return new ChallengeResult(provider, props);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.ExternalAuthentication.LoginWithExternalProviderCallback)]
    public async Task<ActionResult<object>> GetLoginWithExternalProviderCallbackAsync()
    {
        try
        {
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();

            string email = info?.Principal?.FindFirst(ClaimTypes.Email)?.Value;

            foreach(var claim in info?.Principal.Claims)
            {
                Console.WriteLine(claim.Value);
            }

            QuranHubUser user = await _userManager.FindByEmailAsync(email);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = (await _signInManager.CreateUserPrincipalAsync(user)).Identities.First(),

                Expires = DateTime.Now.AddHours(int.Parse(_configuration["BearerTokens:ExpiryHours"])),

                SigningCredentials = new SigningCredentials( new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                                                                   _configuration["BearerTokens:Key"])),
                                                              SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            SecurityToken securityToken = handler.CreateToken(descriptor);

            return Ok(new { success = true, token = handler.WriteToken(securityToken) });
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }

    }

    [HttpPost(Router.ExternalAuthentication.SignupWithExternalProvider)]
    public ActionResult PostSignupWithExternalProvider(string provider) 
    {
        try
        {
            string callbackUrl = GetUrl("auth/signup/SignUpExternalCallback");

            AuthenticationProperties props = _signInManager.ConfigureExternalAuthenticationProperties(provider, callbackUrl);

            return new ChallengeResult(provider, props);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }
     
    [HttpGet(Router.ExternalAuthentication.SignupWithExternalProviderCallback)]
    public async Task<ActionResult> GetSignupWithExternalProviderCallbackAsync() 
    {
        try
        {
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();

            string email = info?.Principal?.FindFirst(ClaimTypes.Email)?.Value;

            foreach (var claim in info?.Principal.Claims)
            {
                Console.WriteLine(claim.Value);
            }

            if (string.IsNullOrEmpty(email))
            {
                Console.WriteLine("External service has not provided an email address.");

                return BadRequest(new {message = "External service has not provided an email address."});  
            }
            else if ((await _userManager.FindByEmailAsync(email)) != null) 
            {
                Console.WriteLine("An account already exists with your email address login instead");

                return BadRequest(new {message = "An account already exists with your email address login instead"});  
            }

            QuranHubUser quranHubUser = new QuranHubUser 
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            IdentityResult result = await _userManager.CreateAsync(quranHubUser);

            if (result.Succeeded) 
            {
                quranHubUser = await _userManager.FindByEmailAsync(email);

                result = await _userManager.AddLoginAsync(quranHubUser, info);

                return  Ok("true");
            }
        
            return BadRequest(new {message = "An account could not be created."});
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    private string GetUrl(string routeName)
    {
        try
        {
            return _configuration["FrontendUrls:Host"] + "/" + routeName;
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return null;
        }
    }
}

