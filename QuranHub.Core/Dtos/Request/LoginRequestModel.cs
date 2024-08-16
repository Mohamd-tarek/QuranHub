namespace QuranHub.Core.Dtos.Request;

public class LoginRequestModel : Request
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}