namespace QuranHub.Core.Dtos.Request;

public class EditProfileRequestModel : Request
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string UserName { get; set; }
}