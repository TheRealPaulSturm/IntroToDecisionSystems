using System.Collections.Generic;
using System.Linq;

namespace DecisionSystems.DataPrediction.Predictor
{
    public class AverageValuePredictor : IDataPredictor
    {
        public IDataPredictionModel Train(IReadOnlyList<DataPoint> data)
        {
            //double sum = 0;
            //foreach (var item in data)
            //{
            //    sum += item.DependentValue;
            //}
            //return new ConstantValuePredictionModel(sum / data.Count);
            var average = data.Average(dataPoint => dataPoint.DependentValue);
            return new ConstantValuePredictionModel(average);
        }
    }
}