using Microsoft.ML;
using Microsoft.ML.Data;
using System.Collections.Generic;
using System.Linq;

namespace DecisionSystems.DataPrediction.Predictor
{
    public interface IDataPredictor
    {
        IDataPredictionModel Train(IReadOnlyList<DataPoint> data);
    }

    public class GradientDescentPredictor : IDataPredictor
    {
        public IDataPredictionModel Train(IReadOnlyList<DataPoint> data)
        {
            var mlContext = new MLContext(seed: 0);

            var dataPoints = data.Select(MLNetDataPoint.FromDomain);    //convert DataPoints to float for ML.Net

            var dataView = mlContext.Data.LoadFromEnumerable(dataPoints);

            var pipeline = mlContext.Transforms.NormalizeMinMax(new[] { new InputOutputColumnPair("Features")})
                .Append(mlContext.Regression.Trainers.OnlineGradientDescent(learningRate: 1));   //normalize the data and then append trainer
            var model = pipeline.Fit(dataView);

            var predictionFunction = mlContext.Model.CreatePredictionEngine<MLNetDataPoint, MLNetDataPointPrediction>(model);
            return new MLNetDataPredictionModel(predictionFunction);    //adapter for converting "model" to IDataPredictionModel
        }

        private class MLNetDataPoint    // ML.Net works with float-values only.
        {
            [VectorType(1)] // number of features is 1

            public float[] Features; // independent values
            public float Label; // dependent value

            public static MLNetDataPoint FromDomain(DataPoint dataPoint)
            {
                return new MLNetDataPoint
                {
                    Features = new[]
                    {
                        (float)dataPoint.IndependentValue
                    },
                    Label = (float)dataPoint.DependentValue
                };
            }
        }
        private class MLNetDataPointPrediction
        {
            public float score; //predicted value
        }

        private class MLNetDataPredictionModel : IDataPredictionModel
        {
            private PredictionEngine<MLNetDataPoint, MLNetDataPointPrediction> predicionFunction;

            public MLNetDataPredictionModel(PredictionEngine<MLNetDataPoint, MLNetDataPointPrediction> predicionFunction)
            {
                this.predicionFunction = predicionFunction;
            }

            public double Test(double independentValue)
            {
                var prediction = predicionFunction.Predict(new MLNetDataPoint { Features = new[] { (float)independentValue} });
                return prediction.score;
            }
        }
    }
}

