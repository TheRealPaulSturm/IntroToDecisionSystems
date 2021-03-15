using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionSystems.DataPrediction.Predictor
{
    public class RandomValuePredictor : IDataPredictor
    {
        public IDataPredictionModel Train(IReadOnlyList<DataPoint> data)
        {
            var minValue = data.Min(dataPoint => dataPoint.IndependentValue);
            var maxValue = data.Max(dataPoint => dataPoint.IndependentValue);

            return new RandomValuePredictionModel(minValue, maxValue);
        }

        public class RandomValuePredictionModel : IDataPredictionModel
        {
            private readonly double min;
            private readonly double max;
            private readonly Random generator = new Random(777);
            public RandomValuePredictionModel(double min, double max)
            {
                this.min = min;
                this.max = max;
            }
            public double Test(double independentValue)
            {
                return generator.NextDouble() * (max - min) + min;
            }
        }
    }
}