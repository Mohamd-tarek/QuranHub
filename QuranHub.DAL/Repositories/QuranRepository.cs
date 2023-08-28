
namespace QuranHub.DAL.Repositories;

public class QuranRepository : IQuranRepository 
{   
    private QuranContext _quranContext;
    private IdentityDataContext _identityDataContext;

    public  QuranRepository(
        QuranContext quranContext,
        IdentityDataContext identityDataContext)
    { 
        _quranContext = quranContext;
        _identityDataContext = identityDataContext;

    }
    public IEnumerable<Quran> Quran => _quranContext.Quran.AsNoTracking();
    public IEnumerable<Muyassar> Muyassar => _quranContext.Muyassar.AsNoTracking();
    public IEnumerable<IbnKatheer> IbnKatheer => _quranContext.IbnKatheer.AsNoTracking();
    public IEnumerable<Tabary> Tabary => _quranContext.Tabary.AsNoTracking();
    public IEnumerable<Qortobi> Qortobi => _quranContext.Qortobi.AsNoTracking();
    public IEnumerable<Jalalayn> Jalalayn => _quranContext.Jalalayn.AsNoTracking();
    public IEnumerable<Translation> Translation => _quranContext.Translation.AsNoTracking();
    public IEnumerable<QuranClean> QuranClean => _quranContext.QuranClean.AsNoTracking();
    public IEnumerable<MindMap> MindMaps => _quranContext.MindMaps.AsNoTracking();
    public IEnumerable<Note> Notes => _identityDataContext.Notes;

    // meta-data
    public IEnumerable<Sura> Suras => _quranContext.Suras.AsNoTracking();
    public IEnumerable<Juz> Juzs => _quranContext.Juzs.AsNoTracking();
    public IEnumerable<Hizb>  Hizbs => _quranContext.Hizbs.AsNoTracking();
    public IEnumerable<Manzil> Manzils => _quranContext.Manzils.AsNoTracking();
    public IEnumerable<Ruku> Rukus => _quranContext.Rukus.AsNoTracking();
    public IEnumerable<Page> Pages => _quranContext.Pages.AsNoTracking();
    public IEnumerable<Sajda> Sajdas => _quranContext.Sajdas.AsNoTracking();
    public IEnumerable<WeightVectorDimention> WeightVectorDimentions => _quranContext.WeightVectorDimentions;

    public IEnumerable<object> GetQuranInfo(string type) 
    {
        switch (type)
        {
            case "Quran": return Quran; break;
            case "Muyassar": return Muyassar; break;
            case "Jalalayn": return Jalalayn; break;
            case "IbnKatheer": return IbnKatheer; break;
            case "Tabary": return Tabary; break;
            case "Qortobi": return Qortobi; break;
            case "Translation": return Translation; break;
            case "QuranClean": return QuranClean; break;
            case "Hizbs": return Hizbs; break;
            case "Juzs": return Juzs; break;
            case "Manzils": return Manzils; break;
            case "Pages": return Pages; break;
            case "Rukus": return Rukus; break;
            case "Sajdas": return Sajdas; break;
            case "Suras": return Suras; break;
            default: return Quran; break;
        }
    }

    public async Task<Note> GetNote(long id, QuranHubUser user) 
    {

        Note? note =  await _identityDataContext.Notes
                                                .Where(d=> d.Index == id && d.QuranHubUserId == user.Id)
                                                .FirstOrDefaultAsync();

        if(note != null)
        {
            note.QuranHubUser = null;
        }

        return note;
    }
    public async Task<bool> AddNote(Note note, QuranHubUser user)
    {

        if (this.Notes.Any((d => d.Index == note.Index && d.QuranHubUserId ==user.Id)))
        {
            Note cur = await this.GetNote(note.Index,user);
            
            cur.Text = note.Text;

            if(cur.QuranHubUser == null)
            {
                cur.QuranHubUser = user;
            }
        }
        else
        {
            note.QuranHubUserId =user.Id;

            await _identityDataContext.AddAsync(note);

        }
        try
        {
            await _identityDataContext.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            return false;
        }

        return true;
    }
    public async Task<byte[]> GetMindMap(long id)
    {
        try
        {

          return await _quranContext.MindMaps
                                    .Where(d => d.Index == id)
                                    .Select(m => m.MapImage)
                                    .SingleAsync();

        } 
        catch (Exception ex)
        {
            return new byte[0];
        }

    }

    public async Task EditWeightVectorAsync(Dictionary<string, double> values)
    {
        foreach(var weightVectorDimention in _quranContext.WeightVectorDimentions)
        {
            weightVectorDimention.Value = values[weightVectorDimention.Word];
        }

        await _quranContext.SaveChangesAsync();
    }
}
