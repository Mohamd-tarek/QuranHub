namespace QuranHub.Core.Dtos.Request;


public class PasswordChangeRequestModel : Request
{
    [Required]
    public string Current { get; set; }

    [Required]
    public string NewPassword { get; set; }

    [Required]
    [Compare(nameof(NewPassword))]
    public string ConfirmPassword { get; set; }
}