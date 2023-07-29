
namespace QuranHub.Domain.Models;

public class React :IEquatable<React>
{
    public int ReactId { get; set; }
    public DateTime DateTime {get; set;}
    public int Type { get; set; }
    public string? QuranHubUserId { get; set; }
    public QuranHubUser QuranHubUser{ get; set;}
    public ReactNotification ReactNotification { get; set; }
    public React(){}
    public React(string quranHubUserId, int type = 0 )
    {
        Type = type;

        QuranHubUserId = quranHubUserId;

        DateTime = DateTime.Now;
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;

        React objAsPostReact = obj as React;

        if (objAsPostReact == null) return false;

        else return Equals(objAsPostReact);
    }

    public override int GetHashCode()
    {
        return ReactId;
    }

    public bool Equals(React other)
    {
        if (other == null) return false;

        return (this.ReactId.Equals(other.ReactId));
    }
}
