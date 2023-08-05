
namespace QuranHub.Domain.Repositories;

public interface IPrivacySettingRepository 
{
    public Task<PrivacySetting> GetPrivacySettingByIdAsync(int privacySettingId);
    public Task<PrivacySetting> GetPrivacySettingByUserIdAsync(string userId);
    public Task<bool> EditPrivacySettingAsync(PrivacySetting privacySetting);
    public Task<bool> EditPrivacySettingByUserIdAsync(PrivacySetting privacySetting, string userId);


}
