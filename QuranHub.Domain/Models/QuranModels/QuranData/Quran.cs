
namespace QuranHub.Domain.Models;

public class QuranBase
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public int Index { get; set; }
    public int Sura { get; set; }
    public int Aya { get; set; }
    public string Text { get; set; }
}
public class Quran : QuranBase
{ }

public class Muyassar: QuranBase
{ }

public class Qortobi : QuranBase
{ }

public class QuranClean : QuranBase
{ }

public class Tabary : QuranBase
{ }

public class Translation : QuranBase
{ }
public class Jalalayn : QuranBase
{ }

public class IbnKatheer : QuranBase
{ }
