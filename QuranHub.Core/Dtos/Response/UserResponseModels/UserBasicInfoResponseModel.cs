namespace QuranHub.Core.Dtos.Response;

public class UserBasicInfoResponseModel : Response
{
    public string Id{ get; set;}
    public string Email{ get; set;}
    public string UserName{ get; set;} 
    public byte[] ProfilePicture {get; set;}
}