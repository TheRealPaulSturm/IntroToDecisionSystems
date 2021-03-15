using System.Collections.Generic;

namespace DecisionSystems.DataPrediction.Predictor
{
    public class InterpolateFromLeftToRightValuePredictor : IDataPredictor
    {
        public IDataPredictionModel Train(IReadOnlyList<DataPoint> data)
        {
            //k und d ausrechnen
            //DataPredictionModel mit k und d erstellen und zurückgeben

            var left = data.BestBy(dataPoint => dataPoint.IndependentValue, (a, b) => a < b);
            var right = data.BestBy(dataPoint => dataPoint.IndependentValue, (a, b) => a > b);

            //k = dy / dx
            double k = (right.DependentValue - left.DependentValue) / (right.IndependentValue - left.IndependentValue);
            double d = left.DependentValue - k * left.IndependentValue; // d = y - k * x

            return new LinearPredictionModel(k, d);
        }
        public class LinearPredictionModel : IDataPredictionModel
        {
            private readonly double k;
            private readonly double d;

            public LinearPredictionModel(double k, double d)
            {
                this.k = k;
                this.d = d;
            }

            public double Test(double independentValue)
            {
                return k * independentValue + d;
            }
        }
    }
}

