
using Microsoft.Extensions.Logging;

namespace QuranHub.DAL.Repositories;
/// <inheritdoc/>
public class HadithRepository : IHadithRepository
{   
    private QuranContext _quranContext;
    private IdentityDataContext _identityDataContext;
    private readonly ILogger<QuranRepository> _logger;

    public HadithRepository(
        QuranContext quranContext,
        IdentityDataContext identityDataContext,
        ILogger<QuranRepository> logger)
    { 
        _quranContext = quranContext;
        _identityDataContext = identityDataContext;
        _logger = logger;

    }
    /// <summary>
    /// get all hadiths
    /// </summary>
    /// <returns></returns>
    public async Task<List<Section>> GetAllHadiths()
    {
        try
        {
            var sections =  await this._quranContext.Sections.Include(Section => Section.Hadiths).ToListAsync();

            foreach ( var section in sections )
            {
                foreach(var hadidth in section.Hadiths)
                {
                    hadidth.Section = null;
                }
            }

            return sections;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }


    /// <summary>
    /// get section by Id
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public async Task<Section> GetSectionById(int Id)
    {
        try
        {
            return await this._quranContext.Sections.Include(Section => Section.Hadiths).FirstOrDefaultAsync(Section => Section.SectionId == Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }
    /// <summary>
    /// get hadith by Id
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public async Task<Hadith> GetHadithById(int Id)
    {
        try
        {
            return await this._quranContext.Hadiths.FirstOrDefaultAsync(Hadith => Hadith.SectionId == Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

}
