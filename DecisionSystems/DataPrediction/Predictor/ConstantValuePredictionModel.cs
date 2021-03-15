namespace DecisionSystems.DataPrediction.Predictor
{
    public class ConstantValuePredictionModel : IDataPredictionModel
    {
        private readonly double predictedValue;

        public ConstantValuePredictionModel(double predictedValue)
        {
            this.predictedValue = predictedValue;
        }
        public double Test(double independentValue)
        {
            return predictedValue;
        }
    }
}