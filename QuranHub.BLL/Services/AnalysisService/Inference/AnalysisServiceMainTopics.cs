
namespace QuranHub.BLL.Services;

public partial class AnalysisService : IMainTopics 
{
    public void DFS()
    {
        HashSet<QuranClean> visited = new HashSet<QuranClean>();

        foreach (KeyValuePair<QuranClean, List<QuranClean>> kvp in this._quranGraph)
        {
            if (!visited.Contains(kvp.Key))
            {
                List<QuranClean> topic = new List<QuranClean>();

                DFSUtil(kvp.Key, visited, topic);
            }
        } 
    }

    public void DFSUtil(QuranClean aya, HashSet<QuranClean> visited, List<QuranClean> topic )
    {
        visited.Add(aya);

        topic.Add(aya);

        if (this._quranGraph.ContainsKey(aya))
        {
            foreach (QuranClean neighbourAya in this._quranGraph[aya])
            {
                if (!visited.Contains(neighbourAya))
                {
                    DFSUtil(neighbourAya, visited, topic);
                }
            }
        }
        
    }
     
    public List<List<QuranClean>> GroupMainTopics()
    {
        HashSet<QuranClean> visited = new HashSet<QuranClean>();

        List<QuranClean> quran = this._quranRepository.QuranClean.ToList();

        List<List<QuranClean>> topics = new List<List<QuranClean>>();

        foreach (QuranClean aya in quran)
        {
            if (!visited.Contains(aya))
            {
                List<QuranClean> topic = new List<QuranClean> ();

                DFSUtil(aya, visited, topic);

                topics.Add(topic);
            }
        }

        return topics;
    }  
}
