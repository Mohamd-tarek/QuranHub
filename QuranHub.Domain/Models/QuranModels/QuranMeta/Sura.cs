namespace QuranHub.Domain.Models;

public class Sura
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int SuraId { get; set; }
    public int Index { get; set; }
    public int Ayas { get; set; }
    public int Start { get; set; }
    public string Name { get; set; }
    public string Tname { get; set; }
    public string Ename { get; set; }
    public string Type { get; set; }
    public int Order { get; set; }
    public int Rukus { get; set; }
}
