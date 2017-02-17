using SodiumPlusTraining.ErrorBackPropagation;
using SodiumPlusTraining.UnitActivations;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SodiumPlusIntegrationTests.Training.BackPropagation
{
    public class SolvesXOrWithSigmoidActivationFunction
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

            _errorBackPropagationTraining = XOrSetUp.SetUpXOrTrainingSingleFold<SigmoidUnitActivationTraining>(bias: 0.5d);
        }

        [Test]
        public async Task IntegrationSolvesXOrWithSigmoidActivationFunction()
        {
            var perceptron = await _errorBackPropagationTraining.TrainAsync(_trainingPatterns, errorMax: 0.1d, maxEpochs: 5000);
            foreach (var trainingPattern in _trainingPatterns)
            {
                var xorResult = await perceptron.FireAsync(trainingPattern.InputValues);
                xorResult.First().Should().BeApproximately(trainingPattern.IdealActivations.First(), 0.1d);
            }
        }
    }
}
