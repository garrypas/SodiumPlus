using System;
using System.Diagnostics;
using SodiumPlusTraining.ErrorBackPropagation;
using SodiumPlusTraining.ErrorBackPropagation.Builder;
using SodiumPlusTraining.UnitActivations;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SodiumPlusIntegrationTests.Training.BackPropagation
{
    public class SolvesXOrWithReluActivationFunction
    {
        private IErrorBackPropagationTraining _errorBackPropagationTraining;
        private List<TrainingPattern> _trainingPatterns;

        [SetUp]
        public void SetUp()
        {
            _trainingPatterns = new List<TrainingPattern>
            {
                new TrainingPattern(new [] { 0d, 0d }, new [] { 0d }),
                new TrainingPattern(new [] { 1d, 0d }, new [] { 1d }),
                new TrainingPattern(new [] { 0d, 1d }, new [] { 1d }),
                new TrainingPattern(new [] { 1d, 1d }, new [] { 0d }),
            };

            var inventoryAndChaining = new ErrorBackPropagationBuilder()
                .With.ANewLayerOfInputUnits(2)
                .ConnectedTo.ANewLayerOfHiddenUnits(3).With.UnitActivation<SigmoidUnitActivationTraining>()
                .ConnectedTo.ANewLayerOfOutputUnits(1).With.OutputUnitActivation<ReluUnitActivationTraining>();

            _errorBackPropagationTraining = XOrSetUp.SetUpXOrTraining(inventoryAndChaining, learningRate: 0.8d);
        }

        [Test]
        public async Task IntegrationSolvesXOrWithReluActivationFunction()
        {
            var timer = new Stopwatch();
            timer.Start();
            var perceptron = await _errorBackPropagationTraining.TrainAsync(_trainingPatterns, errorMax: 0.01d, maxEpochs: 10000);
            timer.Stop();
            Console.WriteLine(timer.ElapsedMilliseconds);
            for (var t = 0; t < _trainingPatterns.Count; t++)
            {
                var trainingPattern = _trainingPatterns.ElementAt(t);
                var xorResult = await perceptron.FireAsync(trainingPattern.InputValues);
                xorResult.ElementAt(0).Should().BeApproximately(trainingPattern.IdealActivations.ElementAt(0), 0.1d, "Pattern " + t);
            }
        }
    }
}
