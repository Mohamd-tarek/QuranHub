
using Microsoft.Extensions.Logging;

namespace QuranHub.DAL.Repositories;
/// <inheritdoc/>
public class PrivacySettingRepository : IPrivacySettingRepository
{
    private IdentityDataContext _identityDataContext;
    private readonly ILogger<PrivacySettingRepository> _logger;
    public PrivacySettingRepository(
        IdentityDataContext identityDataContext,
        ILogger<PrivacySettingRepository> logger)
    {
        _identityDataContext = identityDataContext;
        _logger = logger;
    }

    public async Task<PrivacySetting> GetPrivacySettingByIdAsync(int privacySettingId)
    {
        try
        {
            PrivacySetting privacySetting = await this._identityDataContext.PrivacySettings.FindAsync(privacySettingId);

            privacySetting.QuranHubUser = null;

            return privacySetting;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }
    public async Task<PrivacySetting> GetPrivacySettingByUserIdAsync(string userId)
    {
        try
        {
            PrivacySetting? privacySetting =  await this._identityDataContext.PrivacySettings.Where(privacySetting => privacySetting.QuranHubUserId == userId).FirstOrDefaultAsync();

            if(privacySetting == null)
            {
                privacySetting = new PrivacySetting()
                {
                    QuranHubUserId = userId,
                    AllowFollow = true,
                    AllowComment = true,
                    AllowShare = true,
                    AppearInSearch = true,
                };
            }

            privacySetting.QuranHubUser = null;

            return privacySetting;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }
    public async Task<bool> EditPrivacySettingAsync(PrivacySetting privacySetting)
    {
        try
        {
            PrivacySetting targetPrivacySetting  = await this._identityDataContext.PrivacySettings.FindAsync(privacySetting.PrivacySettingId);
            targetPrivacySetting.AllowFollow = privacySetting.AllowFollow;
            targetPrivacySetting.AllowComment = privacySetting.AllowComment;
            targetPrivacySetting.AllowShare = privacySetting.AllowShare;
            targetPrivacySetting.AppearInSearch = privacySetting.AppearInSearch;
            await this._identityDataContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }
    }
    public async Task<bool> EditPrivacySettingByUserIdAsync(bool AllowFollow, bool AllowComment, bool AllowShare, bool AppearInSearch, string userId)
    {

        try
        {
            PrivacySetting? targetPrivacySetting = await this._identityDataContext.PrivacySettings.Where(privacySetting => privacySetting.QuranHubUserId == userId).FirstOrDefaultAsync();

            if(targetPrivacySetting == null)
            {
                targetPrivacySetting = new();
            }

            targetPrivacySetting.AllowFollow = AllowFollow;
            targetPrivacySetting.AllowComment = AllowComment;
            targetPrivacySetting.AllowShare = AllowShare;
            targetPrivacySetting.AppearInSearch = AppearInSearch;
            targetPrivacySetting.QuranHubUserId = userId;
            if (targetPrivacySetting.PrivacySettingId == 0)
            {
                await  this._identityDataContext.PrivacySettings.AddAsync(targetPrivacySetting);
            }

            await this._identityDataContext.SaveChangesAsync();

            return true;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }
    }

}
