
namespace QuranHub.Web.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public partial class  AccountController : ControllerBase
{
    private ILogger<AccountController> _logger;
    private IPostRepository _postRepository;
    private IUserViewModelsFactory _userViewModelsFactory;
    private UserManager<QuranHubUser> _userManager;
    private SignInManager<QuranHubUser> _signInManager;
    private IEmailService _emailService;
    private TokenUrlEncoderService _tokenUrlEncoder;
    private IPrivacySettingRepository _privacySettingRepository;

    public AccountController(
        ILogger<AccountController> logger,
        UserManager<QuranHubUser> userManager,
        IEmailService emailService,
        SignInManager<QuranHubUser> signInManager,
        IPostRepository postRepository,
        IPrivacySettingRepository privacySettingRepository,
        TokenUrlEncoderService tokenUrlEncoder,
        IUserViewModelsFactory userViewModelsFactory)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _tokenUrlEncoder = tokenUrlEncoder ?? throw new ArgumentNullException(nameof(tokenUrlEncoder));
        _userViewModelsFactory = userViewModelsFactory ?? throw new ArgumentNullException(nameof(userViewModelsFactory));
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _privacySettingRepository = privacySettingRepository ?? throw new ArgumentNullException(nameof(privacySettingRepository));

    }

    [HttpGet("userInfo")]
    public async Task<UserBasicInfoViewModel> GetUserInfoAsync()
    {
        QuranHubUser user  = await _userManager.GetUserAsync(User);
        UserBasicInfoViewModel userBasicInfoViewModel = _userViewModelsFactory.BuildUserBasicInfoViewModel(user);

        return userBasicInfoViewModel;   
    }

    [HttpPost("editUserInfo")]
    public async Task<IActionResult> PostEditProfile([FromBody] EditProfileModel data)
    {
        QuranHubUser user = await _userManager.GetUserAsync(User);

        IdentityResult result = await _userManager.SetUserNameAsync(user, data.UserName);

        if (!result.Process(ModelState))
        {
            return BadRequest("invalid user name");
        }

        if (user.Email != data.Email)
        {
            await PostChangeEmail(user, data.Email);
        }

        return Ok("true");
    }

    [HttpGet("aboutInfo")]
    public async Task<AboutInfoViewModel> GetAboutInfoAsync()
    {
        QuranHubUser user  = await _userManager.GetUserAsync(User);

        AboutInfoViewModel aboutInfoViewModel = _userViewModelsFactory.BuildAboutInfoViewModel(user);

        return aboutInfoViewModel;   
    }

    [HttpGet("privacySetting")]
    public async Task<PrivacySetting> GetPrivacySettingAsync()
    {
        QuranHubUser user = await _userManager.GetUserAsync(User);

        PrivacySetting privacySetting = await this._privacySettingRepository.GetPrivacySettingByUserIdAsync(user.Id);

        return privacySetting;
    }

    [HttpPost("privacySetting")]
    public async Task<IActionResult> PostEditAboutInfoAsync([FromBody] PrivacySetting privacySetting)
    {
        QuranHubUser user = await _userManager.GetUserAsync(User);

        if (await this._privacySettingRepository.EditPrivacySettingByUserIdAsync(privacySetting, user.Id))
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpPost("editAboutInfo")]
    public async Task<IActionResult> PostEditAboutInfoAsync([FromBody] AboutInfoViewModel aboutInfo)
    {
        QuranHubUser user  = await _userManager.GetUserAsync(User);
        user.DateOfBirth  = aboutInfo.DateOfBirth;
        user.Gender = aboutInfo.Gender;
        user.Religion = aboutInfo.Religion; 
        user.AboutMe = aboutInfo.AboutMe;

        IdentityResult result =  await _userManager.UpdateAsync(user);  

        if (!result.Succeeded)
        {
            return BadRequest();
        }
        return Ok();   
    }

    [HttpPost("changeEmail")]
    public async  Task PostChangeEmail(QuranHubUser user, string Email)
    {
        await _emailService.SendChangeEmailConfirmEmail(user, Email, "changeEmailConfirm");
    }

    [HttpGet("changeEmailConfirm")]
    public async Task<IActionResult> ChangeEmailConfirmAsync(string Email, string Token)
    {          
        if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Token))
        {
            QuranHubUser user = await _userManager.GetUserAsync(User);

            if (user != null) 
            {
                string decodedToken = this._tokenUrlEncoder.DecodeToken(Token);

                IdentityResult result  = await _userManager.ChangeEmailAsync (user, Email, Token);

                if (!result.Process(ModelState)) 
                {
                    return BadRequest();
                }
            }
        }

        return Ok("true");
    }

    [HttpPost("changePassword")]
    public async Task<IActionResult> PostChangePasswordAsync([FromBody] PasswordChangeModel data)
    {
        if (ModelState.IsValid) 
        {
            QuranHubUser user = await _userManager.GetUserAsync(User);

            IdentityResult result = await _userManager.ChangePasswordAsync(user, data.Current, data.NewPassword);

            if (!result.Process(ModelState))
            {
                return BadRequest();
            }
        }

        return Ok();
    }

    [HttpPost("recoverPassword")]
    public async Task<IActionResult> PostRecoverPasswordAsync([FromBody] string email)
    {
        if (ModelState.IsValid)
        {
            QuranHubUser user = await _userManager.FindByEmailAsync(email);

            if (user != null) 
            {
                await _emailService.SendPasswordRecoveryEmail(user, "recoverPasswordConfirm");
                return Ok("true");
            }
            else
            {
                return BadRequest();
            }
        }

        return BadRequest();
    }

    [HttpPost("recoverPasswordConfirm")]
    public async Task<IActionResult> PostRecoverPasswordConfirmAsync([FromBody] PasswordRecoverModel data)
    {
        if (ModelState.IsValid) 
        {
            QuranHubUser user = await _userManager.FindByEmailAsync(data.Email);

            string decodedToken = this._tokenUrlEncoder.DecodeToken(data.Token);

            IdentityResult result = await _userManager.ResetPasswordAsync(user, decodedToken, data.NewPassword);

            if (!result.Process(ModelState)) 
            {
                return BadRequest();
            }
        }

        return Ok("true");
    }

    [HttpPost("deleteAccount")]
    public async Task<IActionResult> PostDeleteAccountAsync()
    {

        QuranHubUser quranHubUser = await _userManager.GetUserAsync(User);

        IdentityResult result = await _userManager.DeleteAsync(quranHubUser);
        
        if (!result.Process(ModelState))
        {
            return BadRequest();
            
        }

        List<ShareablePost> shareablePostts = await _postRepository.GetShareablePostsByQuranHubUserIdAsync(quranHubUser.Id);

        List<SharedPost> sharedPosts = await _postRepository.GetSharedPostsByQuranHubUserIdAsync(quranHubUser.Id);

        foreach (var post in shareablePostts) 
        {
            await _postRepository.DeletePostAsync(post.PostId);
        }

        foreach (var post in sharedPosts)
        {
            await _postRepository.DeletePostAsync(post.PostId);
        }

        await _signInManager.SignOutAsync();

        return Ok("true"); 
    }

}

