
namespace QuranHub.Domain.Models;

public class Verse
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int VerseId { get; set; }
    public int Index { get; set; }
    public int Sura { get; set; }
    public int Aya { get; set; }
    public string Text { get; set; }
    public List<Post> Posts { get; set;}
    public List<Comment> Comments { get; set; }
}
