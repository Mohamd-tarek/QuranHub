﻿

namespace QuranHub.Core.Dtos.Response;

public class VerseResponseModel : Response
{
    public int VerseId { get; set; }
    public int Index { get; set; }
    public int Sura { get; set; }
    public int Aya { get; set; }
    public string Text { get; set; }
}
