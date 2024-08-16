namespace QuranHub.Domain.Models;

public class Note
{
    public int NoteId { get; set; }
    public int Index { get; set; }
    public int Sura { get; set; }
    public int Aya { get; set; }
    public string Text { get; set; }
    public string? QuranHubUserId { get; set; }
    public QuranHubUser QuranHubUser { get; set; }

    public Note() { }
    public Note(int Index, int Sura, int Aya, string Text, string QuranHubUserId)
    {
        this.Index = Index;
        this.Sura = Sura;
        this.Aya = Aya; 
        this.Text = Text;
        this.QuranHubUserId = QuranHubUserId;
          
    }
}
