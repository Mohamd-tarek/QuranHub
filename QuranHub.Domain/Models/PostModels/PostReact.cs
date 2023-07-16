
namespace QuranHub.Domain.Models;

public class PostReact : React
{
    public int PostId { get; set; }
    public Post Post { get; set;}

    public PostReactNotification ReactNotification { get; set; }

    public PostReact():base()
    {}
    public PostReact(int type, string quranHubUserId, int postId ):base(type, quranHubUserId)
    {
        PostId = postId;

    }

}
