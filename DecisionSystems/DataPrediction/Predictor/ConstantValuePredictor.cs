using System.Collections.Generic;

namespace DecisionSystems.DataPrediction.Predictor
{
    public class ConstantValuePredictor : IDataPredictor
    {
        private readonly double predictedValue;

        public ConstantValuePredictor(double predictedValue)
        {
            this.predictedValue = predictedValue;
        }
        public IDataPredictionModel Train(IReadOnlyList<DataPoint> data)
        {
            return new ConstantValuePredictionModel(predictedValue);
        }
    }
}