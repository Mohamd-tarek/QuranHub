namespace QuranHub.BLL.Services;

public partial class AnalysisService 
{
    private IQuranRepository _quranRepository;
    private Dictionary<QuranClean, List<QuranClean>> _quranGraph;
    private Dictionary<string, double> _weightVector;
    protected virtual double Accuracy { get; set;} = .7;

    public  AnalysisService(IQuranRepository quranRepository)
    {
        _quranRepository = quranRepository;
        this.BuildWeightVector();
        // this.BuildGraph();
    }

    private void BuildGraph()
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

    private void InsertEdges(QuranClean aya1, QuranClean aya2)
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

    private void BuildWeightVector()
    {
        this._weightVector = new Dictionary<string, double>();

        List<WeightVectorDimention> WeightVectorDimentions = this._quranRepository.WeightVectorDimentions.ToList();

        foreach(var weightVectorDimention in WeightVectorDimentions)
        {
            this._weightVector[weightVectorDimention.Word] = weightVectorDimention.Value;
        }
    }

    public List<QuranClean> GetUniqueAyas()
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
