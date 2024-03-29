﻿using Microsoft.Extensions.Logging;
namespace QuranHub.BLL.Services;

public partial class AnalysisService : ISimilarity 
{
    public virtual double ComputeSimilarity(string text1, string text2)
    {
        try
        {
            Dictionary<string, double> featureVector1 = this.CalculateFeatureVector(text1);

            Dictionary<string, double> featureVector2 = this.CalculateFeatureVector(text2); 
        
            Dictionary<string, double> mutualVector = this.CalculateMutualVector(featureVector1, featureVector2);

            return this.CalculateScore(mutualVector);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return 0;
        }
    }

    public Dictionary<string, double>  CalculateFeatureVector(string text)
    {
        try
        {
            Dictionary<string, double> featureVector = new Dictionary<string, double> ();

            string[] words = text.Split(" ");

            foreach (string word in words)
            {
                if(featureVector.ContainsKey(word))
                {
                    featureVector[word]++;
                }
                else
                {
                    featureVector.Add(word, 1);
                }
            }

            return featureVector;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public Dictionary<string, double> CalculateMutualVector(Dictionary<string, double> featureVector1, Dictionary<string, double> featureVector2)
    {
        try
        {
            Dictionary<string, double> MutualVector = new Dictionary<string, double>();

            foreach (var feature in featureVector1) 
            {
                if (featureVector2.ContainsKey(feature.Key))
                {
                    double value1 = featureVector1[feature.Key];

                    double value2 = featureVector2[feature.Key];

                    double mutualValue = this.FeatureRelativeValue(value1, value2);

                    MutualVector[feature.Key] = mutualValue;
                }
            }

            return MutualVector;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public double FeatureRelativeValue(double value1, double value2)
    {
        try
        {
            if (value1 < value2)
            {
              return value1 / value2;
            }
            else
            {
              return value2 / value1;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return 0 ;
        }
    }

    public double CalculateScore(Dictionary<string, double> featureVector)
    {
        try
        {
            double result = 0;

            foreach (var feature in featureVector) 
            {
               featureVector[feature.Key] *= this._weightVector[feature.Key];

               result += featureVector[feature.Key];
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return 0 ;
        }
    }

    public IEnumerable<QuranClean> GetSimilarAyas(long id)
    {
        try
        {
            QuranClean requestedAya = this._quranRepository.QuranClean.FirstOrDefault(d=> d.Index == id);

            List<QuranClean> ans = new List<QuranClean>();

            List<QuranClean> quran = this._quranRepository.QuranClean.Where(d=> d.Index != id).ToList();

            SortedDictionary<double, List<QuranClean>> similarAyasSortedByScore = new (new DescendingComparer<double>());

            object dictionaryLock = new object();

            Parallel.ForEach(quran,
                             aya =>
                             { 
                                double score = ComputeSimilarity(requestedAya.Text, aya.Text);

                                lock(dictionaryLock)

                                if(score > 0)
                                {
                                     this.InsertScore( similarAyasSortedByScore, score, aya);
                                }
                             });

            ans  = similarAyasSortedByScore.Take(5).Select(ele => ele.Value).Reduce();

            return ans;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public void InsertScore( SortedDictionary<double, List<QuranClean>> similarAyasSortedByScore, double score, QuranClean aya)
    {
        try
        {
            // thread-unsafe zone start                     
            if (similarAyasSortedByScore.ContainsKey(score))
            {
                similarAyasSortedByScore[score].Add(aya);
            }
            else
            {
                List<QuranClean> list = new List<QuranClean>();

                list.Add(aya);

                similarAyasSortedByScore.Add(score, list);
            }
            // thread-unsafe zone end  
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return;
        }
    }      
}
