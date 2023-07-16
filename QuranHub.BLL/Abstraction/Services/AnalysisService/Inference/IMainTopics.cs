
namespace QuranHub.BLL.Abstraction;

public interface IMainTopics 
{
   
    public void DFS();
    public void DFSUtil(QuranClean aya, HashSet<QuranClean> visited, List<QuranClean> topic );   
    public List<List<QuranClean>> GroupMainTopics();

}