namespace QuranHub.Core.Dtos.Request;

public class AddNoteRequestModel : Request
{
    public int NoteId { get; set; }
    public int Index { get; set; }
    public int Sura { get; set; }
    public int Aya { get; set; }
    public string Text { get; set; }

}

