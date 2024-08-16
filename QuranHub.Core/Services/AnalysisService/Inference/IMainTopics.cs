
namespace QuranHub.Core;
/// <summary>
/// main topics in quran logic 
/// </summary>
public interface IMainTopics 
{
    /// <summary>
    /// DFS in quran
    /// </summary>
    public void DFS();
    /// <summary>
    /// DFS utilization
    /// </summary>
    /// <param name="aya"></param>
    /// <param name="visited"></param>
    /// <param name="topic"></param>
    public void DFSUtil(QuranClean aya, HashSet<QuranClean> visited, List<QuranClean> topic );  
    /// <summary>
    /// group main topics together
    /// </summary>
    /// <returns></returns>
    public List<List<QuranClean>> GroupMainTopics();

}