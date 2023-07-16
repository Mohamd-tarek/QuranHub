

namespace QuranHub.Web.Models;

public class EditProfileModel 
{
    [Required]
    public string Email{ get; set;}
    [Required]
    public string UserName{ get; set;}
}