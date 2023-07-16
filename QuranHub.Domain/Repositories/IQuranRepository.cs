
namespace QuranHub.Domain.Repositories;

public interface IQuranRepository 
{

    public IEnumerable<Quran> Quran { get; }
    public IEnumerable<Muyassar> Muyassar { get; }
    public IEnumerable<IbnKatheer> IbnKatheer { get; }
    public IEnumerable<Tabary> Tabary { get; }
    public IEnumerable<Qortobi> Qortobi { get; }
    public IEnumerable<Jalalayn> Jalalayn { get; }
    public IEnumerable<Translation> Translation { get; }
    public IEnumerable<QuranClean> QuranClean { get; }
    public IEnumerable<MindMap> MindMaps { get; }
    public IEnumerable<Note> Notes { get; }

    // meta-data
    public IEnumerable<Sura> Suras  { get; }
    public IEnumerable<Juz> Juzs  { get; }
    public IEnumerable<Hizb>  Hizbs  { get; }
    public IEnumerable<Manzil> Manzils  { get; }
    public IEnumerable<Ruku> Rukus  { get; }
    public IEnumerable<Page> Pages  { get; }
    public IEnumerable<Sajda> Sajdas  { get; }

    public IEnumerable<WeightVectorDimention> WeightVectorDimentions { get; }

    public IEnumerable<object> GetQuranInfo(string type) ;
    public Task<Note> GetNote(long id, QuranHubUser user);
    public  Task<byte[]> GetMindMap(long id) ;
    public  Task<bool> AddNote(Note note, QuranHubUser user);
    public Task EditWeightVectorAsync(Dictionary<string, double> values);



}
