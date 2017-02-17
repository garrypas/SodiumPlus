using SodiumPlusTraining.ErrorBackPropagation;
using SodiumPlusTraining.UnitActivations;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SodiumPlusIntegrationTests.Training.BackPropagation
{
    public class SolvesXOrWithVariousConfigurations
    {
        private List<TrainingPattern> _trainingPatterns;

        [SetUp]
        public void SetUp()
        {
            _trainingPatterns = new List<TrainingPattern>
            {
                new TrainingPattern(new [] { 0d, 0d }, new [] { 0d }),
                new TrainingPattern(new [] { 0d, 1d }, new [] { 1d }),
                new TrainingPattern(new [] { 1d, 0d }, new [] { 1d }),
                new TrainingPattern(new [] { 1d, 1d }, new [] { 0d }),
            };
        }

        [Test]
        public async Task SolvesXOrWithSolvesXOrWithMomentumConstantDefined()
        {
            var errorBackPropagationTraining = XOrSetUp.SetUpXOrTrainingSingleFold<SigmoidUnitActivationTraining>(learningRate: 0.5d, bias: 0.5d, momentum: 0.5d);

            var perceptron = await errorBackPropagationTraining.TrainAsync(_trainingPatterns, errorMax: 0.1d, maxEpochs: 50000);
            foreach (var trainingPattern in _trainingPatterns)
            {
                var xorResult = await perceptron.FireAsync(trainingPattern.InputValues);
                xorResult.First().Should().BeApproximately(trainingPattern.IdealActivations.First(), 0.1d);
            }
        }

        [Test]
        public async Task SolvesXOrWithSolvesXOrWithSlopedMultiplier()
        {
            var errorBackPropagationTraining = XOrSetUp.SetUpXOrTrainingSingleFold<SigmoidUnitActivationTraining>(learningRate: 0.5d, bias: 0.5d, slopeMultiplier: 2d);

            var perceptron = await errorBackPropagationTraining.TrainAsync(_trainingPatterns, errorMax: 0.1d, maxEpochs: 50000);
            foreach (var trainingPattern in _trainingPatterns)
            {
                var xorResult = await perceptron.FireAsync(trainingPattern.InputValues);
                xorResult.First().Should().BeApproximately(trainingPattern.IdealActivations.First(), 0.1d);
            }
        }
    }
}
