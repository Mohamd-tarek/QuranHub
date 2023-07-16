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
}
