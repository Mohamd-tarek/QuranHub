
namespace QuranHub.Domain.Repositories;
/// <summary>
/// represent quran repository
/// </summary>
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
    /// <summary>
    /// get quran info
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public IEnumerable<object> GetQuranInfo(string type);
    /// <summary>
    /// get note 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<Note> GetNote(long id, QuranHubUser user);
    /// <summary>
    /// get mind map
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public  Task<byte[]> GetMindMap(long id);
    /// <summary>
    /// add note 
    /// </summary>
    /// <param name="note"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public  Task<bool> AddNote(int NoteId,  int Index, int Sura, int Aya, string Text , QuranHubUser user);
    /// <summary>
    /// edit weight vector
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public Task EditWeightVectorAsync(Dictionary<string, double> values);
}
