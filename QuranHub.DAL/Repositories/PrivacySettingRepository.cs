
namespace QuranHub.DAL.Repositories;

public class PrivacySettingRepository : IPrivacySettingRepository
{
    private IdentityDataContext _identityDataContext;
    public PrivacySettingRepository(IdentityDataContext identityDataContext)
    {
        _identityDataContext = identityDataContext;  
    }

    public async Task<PrivacySetting> GetPrivacySettingByIdAsync(int privacySettingId)
    {
        PrivacySetting privacySetting = await this._identityDataContext.PrivacySettings.FindAsync(privacySettingId);

        privacySetting.QuranHubUser = null;

        return privacySetting;
    }
    public async Task<PrivacySetting> GetPrivacySettingByUserIdAsync(string userId)
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
    public async Task<bool> EditPrivacySettingAsync(PrivacySetting privacySetting)
    {
        PrivacySetting targetPrivacySetting  = await this._identityDataContext.PrivacySettings.FindAsync(privacySetting.PrivacySettingId);
        targetPrivacySetting.AllowFollow = privacySetting.AllowFollow;
        targetPrivacySetting.AllowComment = privacySetting.AllowComment;
        targetPrivacySetting.AllowShare = privacySetting.AllowShare;
        targetPrivacySetting.AppearInSearch = privacySetting.AppearInSearch;

        try
        {
            await this._identityDataContext.SaveChangesAsync();
            return true;
        } 
        catch(Exception ex)
        {
            return false;
        }
    }
    public async Task<bool> EditPrivacySettingByUserIdAsync(PrivacySetting privacySetting, string userId)
    {
        PrivacySetting? targetPrivacySetting = await this._identityDataContext.PrivacySettings.Where(privacySetting => privacySetting.QuranHubUserId == userId).FirstOrDefaultAsync();

        if(targetPrivacySetting == null)
        {
            targetPrivacySetting = new();
        }

        targetPrivacySetting.AllowFollow = privacySetting.AllowFollow;
        targetPrivacySetting.AllowComment = privacySetting.AllowComment;
        targetPrivacySetting.AllowShare = privacySetting.AllowShare;
        targetPrivacySetting.AppearInSearch = privacySetting.AppearInSearch;
        targetPrivacySetting.QuranHubUserId = userId;

        try
        {
            if (targetPrivacySetting.PrivacySettingId == 0)
            {
                await  this._identityDataContext.PrivacySettings.AddAsync(targetPrivacySetting);
            }

            await this._identityDataContext.SaveChangesAsync();

            return true;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

}
