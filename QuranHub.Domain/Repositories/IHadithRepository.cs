
namespace QuranHub.Domain.Repositories;
/// <summary>
/// represent quran repository
/// </summary>
public interface IHadithRepository
{
    /// <summary>
    /// get all hadiths
    /// </summary>
    /// <returns></returns>
    public  Task<List<Section>> GetAllHadiths();

    /// <summary>
    /// get section by Id
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public Task<Section> GetSectionById(int Id);
    /// <summary>
    /// get hadith by Id
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public Task<Hadith> GetHadithById(int Id);



}
