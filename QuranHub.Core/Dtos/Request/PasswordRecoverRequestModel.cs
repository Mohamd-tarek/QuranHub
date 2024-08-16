namespace QuranHub.Core.Dtos.Request;

public class PasswordRecoverRequestModel : Request
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Token { get; set; }

    [Required]
    public string NewPassword { get; set; }

    [Required]
    [Compare(nameof(NewPassword))]
    public string ConfirmPassword { get; set; }
}

