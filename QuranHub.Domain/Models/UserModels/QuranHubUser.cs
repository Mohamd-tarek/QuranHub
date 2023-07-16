
namespace QuranHub.Domain.Models;

public class QuranHubUser : IdentityUser 
{
    public QuranHubUser (): base(){}
    public QuranHubUser (string userName): base(userName){}
    public QuranHubUser (string userName, string email): base(userName) {
        this.Email = email;
    }

    [PersonalData]
    public byte[] ProfilePicture { get; set; } = new byte[0];

    [PersonalData]
    public byte[] CoverPicture {get; set;} = new byte[0];

    [PersonalData]
    public DateTime DateOfBirth {get; set;}

    [PersonalData]
    public Gender Gender {get; set;}

    [PersonalData]
    public Religion Religion {get; set;}

    [PersonalData]
    public string? AboutMe {get; set;}

    [PersonalData]
    public bool Online { get; set; }

    [PersonalData]
    public string? ConnectionId { get; set; }

    [PersonalData]
    public PrivacySetting PrivacySetting { get; set; }

    [PersonalData]
    public List<Note> Notes { get; set; }

    [PersonalData]
    public List<Post> Posts { get; set; }

    [PersonalData]
    public List<Comment> Commnets { get; set; }

    [PersonalData]
    public List<CommentReact> CommnetReacts { get; set; }

    [PersonalData]
    public List<PostReact> PostReacts { get; set; }

    [PersonalData]
    public List<Share> Shares { get; set; }

    [PersonalData]
    public List<Follow> Followers { get; set; } 

    [PersonalData]
    public List<Follow> Following { get; set; }

    [PersonalData]
    public List<Notification> TargetNotifications { get; set; }

    [PersonalData]
    public List<Notification> SourceNotifications { get; set; }

}
