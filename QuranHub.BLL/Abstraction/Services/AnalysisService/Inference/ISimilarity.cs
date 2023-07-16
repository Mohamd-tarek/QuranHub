
namespace QuranHub.BLL.Abstraction;

public interface ISimilarity 
{
    public  double ComputeSimilarity(string text1, string text2);
    public Dictionary<string, double>  CalculateFeatureVector(string text);
    public Dictionary<string, double> CalculateMutualVector(Dictionary<string, double> featureVector1, Dictionary<string, double> featureVector2);
    public double FeatureRelativeValue(double value1, double value2);
    public double CalculateScore( Dictionary<string, double> featureVector);
    public IEnumerable<QuranClean> GetSimilarAyas(long id);
    public void InsertScore( SortedDictionary<double, List<QuranClean>> similarAyasSortedByScore, double score, QuranClean aya);
}