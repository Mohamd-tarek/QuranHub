
namespace QuranHub.Core.Dtos.Request;

public class AddCommentReactRequestModel : Request
{
    public int CommentId { get; set; }
    public string QuranHubUserId { get; set;}
    public int Type {get; set;}
}

