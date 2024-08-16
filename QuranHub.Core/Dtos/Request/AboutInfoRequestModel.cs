namespace QuranHub.Core.Dtos.Request;

public class AboutInfoRequestModel : Request
{

    public DateTime DateOfBirth { get; set; }

    public Gender Gender { get; set; }

    public Religion Religion { get; set; }

    public string AboutMe { get; set; }

}
