namespace QuranHub.BLL.Abstraction;
public interface ILearning 
    {
        public double LossFunction(List<(QuranClean, QuranClean, double)> dataSet);
        public double LossFunctionDerivative(List<(QuranClean, QuranClean, double)> dataSet);
        public Task GradientDescentAsync(List<(QuranClean, QuranClean, double)> dataSet);
        public Task EditWeightVectorAsync(double value);
    }

