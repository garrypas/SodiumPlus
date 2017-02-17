using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SodiumPlusSerialization;
using SodiumPlusTraining.ErrorBackPropagation;
using SodiumPlusTraining.ErrorBackPropagation.Builder;
using SodiumPlusTraining.ErrorBackPropagation.NetworkErrorFunctions;
using SodiumPlusTraining.UnitActivations;

namespace TrainingExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Run(() => XOrClassificationWithSoftmax()).Wait();
            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private static async Task XOrClassificationWithSoftmax()
        {
            var trainingPatterns = new List<TrainingPattern>
            {
                new TrainingPattern(new [] { 0d, 0d }, new [] { 0d, 1d }),
                new TrainingPattern(new [] { 1d, 0d }, new [] { 1d, 0d }),
                new TrainingPattern(new [] { 0d, 1d }, new [] { 1d, 0d }),
                new TrainingPattern(new [] { 1d, 1d }, new [] { 0d, 1d }),
            };

            var perceptron = await new ErrorBackPropagationBuilder()
                .With.ANewLayerOfInputUnits(2)
                .ConnectedTo.ANewLayerOfHiddenUnits(3).With.SigmoidActivation()
                .ConnectedTo.ANewLayerOfOutputUnits(2).With.SoftmaxActivation()
                .And.UseCrossEntropyErrorFunction()
                .And.Bias(0.5)
                .And.LearningRate(1d)
                .And.Momentum(0d)
                .And.SlopeMultiplier(1d)
                .And.UseOneHotEncoding()
                .And.SetupNetwork()
                .And.NameEverything()
                .And.ReadyForTraining()
                .TrainAsync(trainingPatterns, 0.1d, 1000);

            // Go through serialization/deserialization loop
            File.WriteAllText("perceptron.txt", new PerceptronSerializer().SerializeJson(perceptron));
            perceptron = new PerceptronSerializer().DeserializeJson(File.ReadAllText("perceptron.txt"));

            // Show XOR working
            var result1 = await perceptron.FireAsync(new[] { 0d, 0d });
            Console.WriteLine("(0, 0) -> (" + string.Join(",", result1) + ")");

            var result2 = await perceptron.FireAsync(new[] { 1d, 0d });
            Console.WriteLine("(1, 0) -> (" + string.Join(",", result2) + ")");

            var result3 = await perceptron.FireAsync(new[] { 0d, 1d });
            Console.WriteLine("(0, 1) -> (" + string.Join(",", result3) + ")");

            var result4 = await perceptron.FireAsync(new[] { 1d, 1d });
            Console.WriteLine("(1, 1) -> (" + string.Join(",", result4) + ")");
        }
    }
}
