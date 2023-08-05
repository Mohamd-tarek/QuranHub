namespace QuranHub.Domain.Models;

public class PrivacySetting
{
    public int PrivacySettingId { get; set; }
    public bool AllowFollow { get; set; }
    public bool AllowComment { get; set; }
    public bool AllowShare { get; set; }
    public bool AppearInSearch { get; set; }
    public string QuranHubUserId { get; set; }
    public QuranHubUser QuranHubUser { get; set; }
}
