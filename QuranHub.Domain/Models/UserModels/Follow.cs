
namespace QuranHub.Domain.Models;

public class Follow : IEquatable<Follow>
{
    public int FollowId { get; set; }
    public DateTime DateTime {get; set;}
    public string FollowerId { get; set; }
    public QuranHubUser Follower { get; set; }
    public string FollowedId { get; set; }
    public QuranHubUser Followed { get; set; }
    public FollowNotification FollowNotification { get; set; }  
    public int Likes { get; set;}
    public int Comments { get; set;}
    public int Shares { get; set;}  

     public override bool Equals(object obj)
    {
        if (obj == null) return false;
        Follow objAsFollow = obj as Follow;
        if (objAsFollow == null) return false;
        else return Equals(objAsFollow);
    }

    public override int GetHashCode()
    {
        return FollowId;
    }

    public bool Equals(Follow follow)
    {
        if (follow == null) return false;
        return FollowId.Equals(follow.FollowId);
    }
}
