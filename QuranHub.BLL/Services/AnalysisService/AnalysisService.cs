using Microsoft.Extensions.Logging;

namespace QuranHub.BLL.Services;
/// <inheritdoc/>
public partial class AnalysisService 
{
    private IQuranRepository _quranRepository;
    private Dictionary<QuranClean, List<QuranClean>> _quranGraph;
    private Dictionary<string, double> _weightVector;
    ILogger<AnalysisService> _logger;
    protected virtual double Accuracy { get; set;} = .7;

    public  AnalysisService(IQuranRepository quranRepository, ILogger<AnalysisService> logger)
    {
        _quranRepository = quranRepository;
        this.BuildWeightVector();
        _logger = logger;
        // this.BuildGraph();
    }

    private void BuildGraph()
    {
        try
        {
            List<QuranClean> quran = this._quranRepository.QuranClean.ToList();

            this._quranGraph = new Dictionary<QuranClean, List<QuranClean>>();

            for (int aya = 0; aya < quran.Count; ++aya)
            {
                for (int nxtAya = aya + 1; nxtAya < quran.Count; ++nxtAya)
                {
                    if (this.ComputeSimilarity(quran[aya].Text, quran[nxtAya].Text) > 0)
                    {
                        this.InsertEdges(quran[aya], quran[nxtAya]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return;
        }
    }

    private void InsertEdges(QuranClean aya1, QuranClean aya2)
    {
        try
        {
            if (!this._quranGraph.ContainsKey(aya1))
            {
               this._quranGraph[aya1] = new List<QuranClean>();
            }

            if (!this._quranGraph.ContainsKey(aya2))
            {
               this._quranGraph[aya2] = new List<QuranClean>();
            }

            this._quranGraph[aya1].Add(aya2);

            this._quranGraph[aya2].Add(aya1);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return;
        }
    }

    private void BuildWeightVector()
    {
        try
        {
            this._weightVector = new Dictionary<string, double>();

            List<WeightVectorDimention> WeightVectorDimentions = this._quranRepository.WeightVectorDimentions.ToList();

            foreach(var weightVectorDimention in WeightVectorDimentions)
            {
                this._weightVector[weightVectorDimention.Word] = weightVectorDimention.Value;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return;
        }
    }

    public List<QuranClean> GetUniqueAyas()
    {
        try
        {
            List<QuranClean> ans = new List<QuranClean>();

            List<QuranClean> quran = this._quranRepository.QuranClean.ToList();

            SortedDictionary<double, List<QuranClean>> similarAyasSortedByScore = new (new DescendingComparer<double>());

            foreach (var aya in quran) 
            {
                List<QuranClean> result = (List<QuranClean>)GetSimilarAyas(aya.Index);

                if(result.Count == 0)
                {
                    ans.Add(aya);
                }
            }

            return ans;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }
}

class DescendingComparer<T> : IComparer<T> where T : IComparable<T> 
{
    public int Compare(T x, T y) 
    {
        return y.CompareTo(x);
    }
}

public static class LinqExtension
{
     public static List<QuranClean>  Reduce(this IEnumerable<List<QuranClean>> lists)
     {
        List<QuranClean> result = new List<QuranClean>();

        foreach (var list in lists)
        {
            foreach (var el in list)
            {
                result.Add(el);
            }
        }

        return result;

     }
}
