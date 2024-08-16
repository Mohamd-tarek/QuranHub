namespace QuranHub.Core.Dtos.Request;

public class SignUpConfirmRequestModel : Request
{
    public string Email { get; set; }
    public string Token { get; set; }
}