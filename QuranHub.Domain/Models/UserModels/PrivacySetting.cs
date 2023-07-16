namespace QuranHub.Domain.Models;

public class PrivacySetting
{
    public int PrivacySettingId { get; set; }
    public int FollowResolution { get; set; }
    public int CommentResolution { get; set; }
    public int ShareResolution { get; set; }
    public bool AppearInSearch { get; set; }
    public string QuranHubUserId { get; set; }
    public QuranHubUser QuranHubUser { get; set; }
}
