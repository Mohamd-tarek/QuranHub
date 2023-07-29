
namespace QuranHub.Domain.Models;

public class PostReact : React
{
    public int PostId { get; set; }
    public Post Post { get; set;}

    public PostReactNotification PostReactNotification { get; set; }

    public PostReact():base()
    {}
    public PostReact(string quranHubUserId, int postId, int type = 0):base(quranHubUserId, type)
    {
        PostId = postId;
    }

}
