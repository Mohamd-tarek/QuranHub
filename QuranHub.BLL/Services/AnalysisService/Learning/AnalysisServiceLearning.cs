namespace QuranHub.BLL.Services;

public partial class AnalysisService : ILearning 
{
    public double LossFunction(List<(QuranClean, QuranClean, double)> dataSet)
    {
        int dataSetSize = dataSet.Count;

        double trainLoss = 0;

        foreach (var pair in dataSet)
        {
           double ComputedScore = ComputeSimilarity(pair.Item1.Text, pair.Item2.Text);

           trainLoss += Math.Pow(ComputedScore - pair.Item3, 2);
        }

        double averageDataSetLoss = trainLoss / dataSetSize;

        return averageDataSetLoss;
    }

    public double LossFunctionDerivative(List<(QuranClean, QuranClean, double)> dataSet)
    {
        int dataSetSize = dataSet.Count;

        double trainLoss = 0;

        foreach (var pair in dataSet)
        {
           trainLoss += (2 * ComputeSimilarity(pair.Item1.Text, pair.Item2.Text) - pair.Item3); // * mutual feature vector
        }

        double averageLossDeviation = trainLoss / dataSetSize;

        return averageLossDeviation;
    }

    public async Task GradientDescentAsync(List<(QuranClean, QuranClean, double)> dataSet)
    {
        double eta = 0.01;

        for (var iteration = 0; iteration < 100; ++iteration)
        {
            double gradient = LossFunctionDerivative(dataSet);

            double editValue = eta * gradient;

            await this.EditWeightVectorAsync(editValue);
        }
    }

    public async Task  EditWeightVectorAsync(double value)
    {
        foreach (var dim in this._weightVector) 
        {
            this._weightVector[dim.Key] -= value;
        }

       await this._quranRepository.EditWeightVectorAsync(this._weightVector);
    }
} 
