using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace QuranHub.Web.Controllers;

[Route("api/[controller]")]
public partial class  AuthenticationController : ControllerBase
{
    private ILogger<AuthenticationController> _logger;
    private UserManager<QuranHubUser> _userManager;
    private SignInManager<QuranHubUser> _signInManager;
    private IEmailService _emailService;
    private TokenUrlEncoderService _tokenUrlEncoder;
    private IConfiguration _configuration;

    public AuthenticationController(
        ILogger<AuthenticationController> logger,
        UserManager<QuranHubUser> userManager,
        IEmailService emailService,
        SignInManager<QuranHubUser> signInManager,
        TokenUrlEncoderService tokenUrlEncoder,
        IConfiguration configuration)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _tokenUrlEncoder = tokenUrlEncoder ?? throw new ArgumentNullException(nameof(tokenUrlEncoder));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    [HttpPost("LoginWithPassword")]
    public async Task<object> LoginWithPassword([FromBody] LoginModel creds)
    {
        QuranHubUser user = await this._userManager.FindByEmailAsync(creds.Email);

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, creds.Password, true);

        if (result.Succeeded) 
        {
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = (await _signInManager.CreateUserPrincipalAsync(user)).Identities.First(),

                Expires = DateTime.Now.AddHours(int.Parse( _configuration["BearerTokens:ExpiryHours"])),

                SigningCredentials = new SigningCredentials( new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                                                                _configuration["BearerTokens:Key"])),
                                                                 SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            SecurityToken securityToken = handler.CreateToken(descriptor);

            Console.WriteLine(securityToken.ToString());

            return new {success = true , token = handler.WriteToken(securityToken) };
        }

        return new { success = false};
    }
          
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] LoginModel creds)
    {
        if (ModelState.IsValid)
        {
            QuranHubUser user = await _userManager.FindByEmailAsync(creds.Email);
            if (user != null) 
            {
                return BadRequest(new { message = "Email already exist" });
            }

            if (user != null && !await _userManager.IsEmailConfirmedAsync(user))
            {
                return BadRequest(new { message = "Email already exist but not confirmed" });
            }

            user = new QuranHubUser
            {
                UserName = creds.Email,
                Email = creds.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user);

            if (result.Process(ModelState)) 
            {
                result = await _userManager.AddPasswordAsync(user, creds.Password);

                if (result.Process(ModelState))
                {
                    await _emailService.SendAccountConfirmEmail(user, "auth/signupConfirm");

                    return Ok("true");
                } 
                else
                {
                    await _userManager.DeleteAsync(user);

                    return BadRequest(new { message = "password error happened" });
                }
            }
        }

        return BadRequest(new { message = "unkown error occured" });
    }
    
    [HttpPost("signUpConfirm")]
    public async Task<IActionResult> SignUpConfirmAsync([FromBody]SignUpConfirmModel data) 
    {      
        if (!string.IsNullOrEmpty(data.Email) && !string.IsNullOrEmpty(data.Token))
        {
            QuranHubUser user = await _userManager.FindByEmailAsync(data.Email);

            if (user != null) 
            {
                string decodedToken = this._tokenUrlEncoder.DecodeToken(data.Token);

                IdentityResult result = await _userManager.ConfirmEmailAsync(user, decodedToken);

                if (result.Process(ModelState))
                {
                    return Ok("true");
                }
            }
        }

        return BadRequest();
    }

    [HttpPost("signupResend")]
    public async Task<IActionResult> SignUpResend([FromBody] string Email) 
    {         
            QuranHubUser user = await _userManager.FindByEmailAsync(Email);

            if (user != null && !await _userManager.IsEmailConfirmedAsync(user))
            {
                await _emailService.SendAccountConfirmEmail(user, "signupConfirm");
                return Ok("true");
            }
            else
            {               
                return BadRequest();
            }
    }
}

