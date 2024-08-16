
namespace QuranHub.Core;
/// <summary>
/// represnet similarity find logic in quran
/// </summary>
public interface ISimilarity 
{
    /// <summary>
    /// compute similarity between two verses
    /// </summary>
    /// <param name="text1"></param>
    /// <param name="text2"></param>
    /// <returns></returns>
    public  double ComputeSimilarity(string text1, string text2);
    /// <summary>
    /// calculate a feature vector for a verse
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public Dictionary<string, double>  CalculateFeatureVector(string text);
    /// <summary>
    /// calculate mutual vector
    /// </summary>
    /// <param name="featureVector1"></param>
    /// <param name="featureVector2"></param>
    /// <returns></returns>
    public Dictionary<string, double> CalculateMutualVector(Dictionary<string, double> featureVector1, Dictionary<string, double> featureVector2);
    /// <summary>
    /// compute feature relative value
    /// </summary>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <returns></returns>
    public double FeatureRelativeValue(double value1, double value2);
    /// <summary>
    /// calculate score for similarity 
    /// </summary>
    /// <param name="featureVector"></param>
    /// <returns></returns>
    public double CalculateScore( Dictionary<string, double> featureVector);
    /// <summary>
    /// get similar aya for aya by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public IEnumerable<QuranClean> GetSimilarAyas(long id);
    /// <summary>
    /// inser score for a verse
    /// </summary>
    /// <param name="similarAyasSortedByScore"></param>
    /// <param name="score"></param>
    /// <param name="aya"></param>
    public void InsertScore( SortedDictionary<double, List<QuranClean>> similarAyasSortedByScore, double score, QuranClean aya);
}