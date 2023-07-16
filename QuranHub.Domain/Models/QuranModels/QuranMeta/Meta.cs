
namespace QuranHub.Domain.Models;

public class Meta
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public int Index { get; set; }
    public int Sura { get; set; }
    public int Aya { get; set; }
}

public class Hizb :Meta
{ }

public class Juz : Meta
{ }

public class Manzil : Meta
{ }

public class Page : Meta
{ }

public class Ruku : Meta
{ }

public class Sajda : Meta
{
    public string Type { get; set; }
}
