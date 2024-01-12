
namespace QuranHub.Domain.Repositories;
/// <summary>
/// represent privacy setting repository
/// </summary>
public interface IPrivacySettingRepository 
{
    /// <summary>
    /// get privacy setting
    /// </summary>
    /// <param name="privacySettingId"></param>
    /// <returns></returns>
    public Task<PrivacySetting> GetPrivacySettingByIdAsync(int privacySettingId);
    /// <summary>
    /// get privacy setting for a user 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<PrivacySetting> GetPrivacySettingByUserIdAsync(string userId);
    /// <summary>
    /// edit privacy setting
    /// </summary>
    /// <param name="privacySetting"></param>
    /// <returns></returns>
    public Task<bool> EditPrivacySettingAsync(PrivacySetting privacySetting);
    /// <summary>
    /// edit privacy setting for a user
    /// </summary>
    /// <param name="privacySetting"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<bool> EditPrivacySettingByUserIdAsync(PrivacySetting privacySetting, string userId);


}
