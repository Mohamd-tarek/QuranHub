﻿namespace QuranHub.Web.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public partial class  AccountController : ControllerBase
{
    private readonly Serilog.ILogger _logger;
    private IPostRepository _postRepository;
    private IUserResponseModelsFactory _userResponseModelsFactory;
    private UserManager<QuranHubUser> _userManager;
    private SignInManager<QuranHubUser> _signInManager;
    private IEmailService _emailService;
    private TokenUrlEncoderService _tokenUrlEncoder;
    private IPrivacySettingRepository _privacySettingRepository;

    public AccountController(
        Serilog.ILogger logger,
        UserManager<QuranHubUser> userManager,
        IEmailService emailService,
        SignInManager<QuranHubUser> signInManager,
        IPostRepository postRepository,
        IPrivacySettingRepository privacySettingRepository,
        TokenUrlEncoderService tokenUrlEncoder,
        IUserResponseModelsFactory userResponseModelsFactory)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _tokenUrlEncoder = tokenUrlEncoder ?? throw new ArgumentNullException(nameof(tokenUrlEncoder));
        _userResponseModelsFactory = userResponseModelsFactory ?? throw new ArgumentNullException(nameof(userResponseModelsFactory));
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _privacySettingRepository = privacySettingRepository ?? throw new ArgumentNullException(nameof(privacySettingRepository));

    }

    [HttpGet(Router.Account.UserInfo)]
    public async Task<ActionResult<UserBasicInfoResponseModel>> GetUserInfoAsync()
    {
        try
        {

            QuranHubUser user  = await _userManager.GetUserAsync(User);
            UserBasicInfoResponseModel userBasicInfoResponseModel = _userResponseModelsFactory.BuildUserBasicInfoResponseModel(user);

            return Ok(userBasicInfoResponseModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost(Router.Account.EditUserInfo)]
    public async Task<ActionResult> EditProfile([FromBody] EditProfileRequestModel data)
    {
        try
        {
            QuranHubUser user = await _userManager.GetUserAsync(User);

            IdentityResult result = await _userManager.SetUserNameAsync(user, data.UserName);

            if (!result.Process(ModelState))
            {
                string errors = ModelState.ConcatError();

                return BadRequest(errors);
            }

            if (user.Email != data.Email)
            {
                await ChangeEmail(user, data.Email);
            }

            return Ok("true");
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

   
    [HttpGet(Router.Account.AboutInfo)]
    public async Task<ActionResult<AboutInfoRequestModel>> GetAboutInfoAsync()
    {
        try
        {
            QuranHubUser user  = await _userManager.GetUserAsync(User);

            AboutInfoRequestModel aboutInfoResponseModel = _userResponseModelsFactory.BuildAboutInfoResponseModel(user);

            return Ok(aboutInfoResponseModel);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Account.PrivacySetting)]
    public async Task<ActionResult<PrivacySetting>> GetPrivacySettingAsync()
    {
        try
        {
            QuranHubUser user = await _userManager.GetUserAsync(User);

            PrivacySetting privacySetting = await this._privacySettingRepository.GetPrivacySettingByUserIdAsync(user.Id);

            return Ok(privacySetting);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost(Router.Account.PrivacySetting)]
    public async Task<ActionResult> EditPrivacySettingAsync([FromBody] EditPrivacySettingRequestModel privacySetting)
    {
        try
        {
            QuranHubUser user = await _userManager.GetUserAsync(User);

            if (await this._privacySettingRepository.EditPrivacySettingByUserIdAsync(privacySetting.AllowFollow, privacySetting.AllowComment, privacySetting.AllowShare, privacySetting.AppearInSearch, user.Id))
            {
                return Ok();
            }
            return BadRequest();
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost(Router.Account.EditAboutInfo)]
    public async Task<ActionResult> EditAboutInfoAsync([FromBody] AboutInfoRequestModel aboutInfo)
    {
        try
        {
            QuranHubUser user  = await _userManager.GetUserAsync(User);
            user.DateOfBirth  = aboutInfo.DateOfBirth;
            user.Gender = aboutInfo.Gender;
            user.Religion = aboutInfo.Religion; 
            user.AboutMe = aboutInfo.AboutMe;

            IdentityResult result =  await _userManager.UpdateAsync(user);  

            if (!result.Process(ModelState))
            {
                string errors = ModelState.ConcatError();

                return BadRequest(errors);
            }

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost(Router.Account.ChangeEmail)]
    public async  Task<ActionResult> ChangeEmail(QuranHubUser user, string Email)
    {
        try
        {
            await _emailService.SendChangeEmailConfirmEmail(user, Email, "changeEmailConfirm");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet(Router.Account.ChangeEmailConfirm)]
    public async Task<ActionResult> ChangeEmailConfirmAsync(string Email, string Token)
    {
        try
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
                        string errors = ModelState.ConcatError();
                    
                        return BadRequest(errors);
                    }
                }
            }

            return Ok("true");
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost(Router.Account.ChangePassword)]
    public async Task<ActionResult> ChangePasswordAsync([FromBody] PasswordChangeRequestModel data)
    {
        try
        {
            if (ModelState.IsValid) 
            {
                QuranHubUser user = await _userManager.GetUserAsync(User);

                IdentityResult result = await _userManager.ChangePasswordAsync(user, data.Current, data.NewPassword);

                if (!result.Process(ModelState))
                {
                    string errors = ModelState.ConcatError();
                
                    return BadRequest(errors);
                }
            }

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost(Router.Account.RecoverPassword)]
    public async Task<ActionResult> RecoverPasswordAsync([FromBody] string email)
    {
        try
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
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost(Router.Account.RecoverPasswordConfirm)]
    public async Task<ActionResult> RecoverPasswordConfirmAsync([FromBody] PasswordRecoverRequestModel data)
    {
        try
        {
            if (ModelState.IsValid) 
            {
                QuranHubUser user = await _userManager.FindByEmailAsync(data.Email);

                string decodedToken = this._tokenUrlEncoder.DecodeToken(data.Token);

                IdentityResult result = await _userManager.ResetPasswordAsync(user, decodedToken, data.NewPassword);

                if (!result.Process(ModelState)) 
                {
                    string errors = ModelState.ConcatError();
                
                    return BadRequest(errors);
                }
            }

            return Ok("true");
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost(Router.Account.DeleteAccount)]
    public async Task<ActionResult> DeleteAccountAsync()
    {
        try
        {

            QuranHubUser quranHubUser = await _userManager.GetUserAsync(User);

            IdentityResult result = await _userManager.DeleteAsync(quranHubUser);
        
            if (!result.Process(ModelState))
            {
                string errors = ModelState.ConcatError();
            
                return BadRequest(errors);
            
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
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return BadRequest();
        }
    }

}

