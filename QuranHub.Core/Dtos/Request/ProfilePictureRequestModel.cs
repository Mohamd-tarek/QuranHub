namespace QuranHub.Core.Dtos.Request;


public class ProfilePictureRequestModel : Request
{
    public IFormFile ProfilePictureFile { get; set; }
}