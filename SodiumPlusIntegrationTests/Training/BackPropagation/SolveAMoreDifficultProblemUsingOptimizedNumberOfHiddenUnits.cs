using SodiumPlus.Topology;
using SodiumPlusTraining.ErrorBackPropagation;
using SodiumPlusTraining.ErrorBackPropagation.Builder;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SodiumPlusIntegrationTests.Training.BackPropagation
{
    using TNetwork = ICollection<ICollection<ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>>;
    public class SolveAMoreDifficultProblemUsingOptimizedNumberOfHiddenUnits
    {
        private List<TrainingPattern> _trainingPatterns;

        [SetUp]
        public void SetUp()
        {
            _trainingPatterns = new List<TrainingPattern>
            {
                new TrainingPattern(new [] { 0.1d, 0.2d }, new [] { 0.1d }),
                new TrainingPattern(new [] { 0.3d, 0.4d }, new [] { 0.2d }),
                new TrainingPattern(new [] { 0.5d, 0.6d }, new [] { 0.3d }),
                new TrainingPattern(new [] { 0.7d, 0.8d }, new [] { 0.4d }),
                new TrainingPattern(new [] { 0.9d, 1d }, new [] { 0.5d }),
                new TrainingPattern(new [] { 0.2d, 0.1d }, new [] { 0.6d }),
                new TrainingPattern(new [] { 0.4d, 0.3d }, new [] { 0.7d }),
                new TrainingPattern(new [] { 1d, 1d }, new [] { 0.8d }),
            };
        }

        [Test]
        public async Task SolveAMoreDifficultProblemUsingOptimalNumberOfHiddenUnit()
        {
            var errorBackPropagationTraining = SetUpXOrTraining(learningRate: 0.2d, bias: 0.5d, momentum: 0.25d);

            var perceptron = await errorBackPropagationTraining.TrainAsync(_trainingPatterns, errorMax: 0.5d, maxEpochs: 50000);
            foreach (var trainingPattern in _trainingPatterns)
            {
                var xorResult = await perceptron.FireAsync(trainingPattern.InputValues);
                xorResult.First().Should().BeApproximately(trainingPattern.IdealActivations.First(), 0.2d);
            }
        }

        public IErrorBackPropagationTraining SetUpXOrTraining(double learningRate = 0.5d, double bias = 0d, double momentum = 0d, double slopeMultiplier = 1d)
        {
            var weightSetterMock = new Mock<IWeightSetter>();
            var stepsMock = new Mock<ErrorBackPropagationStepsDependencyFactory>().As<IErrorBackPropagationStepsDependencyFactory>();
            stepsMock.CallBase = true;
            stepsMock.Setup(s => s.CreateWeightSetter(It.IsAny<double>(), It.IsAny<double>())).Returns(weightSetterMock.Object);
            var dependencyFactory = new ErrorBackPropagationDependencyFactory(stepsMock.Object);

            var errorBackPropagationTraining = new ErrorBackPropagationBuilder()
                .With.ANewLayerOfInputUnits(2)
                .ConnectedTo.ANewLayerOfHiddenUnitsOptimizedForTrainingPatterns(_trainingPatterns)
                .With.UnitActivation<IdentityUnitActivationTraining>()
                .ConnectedTo.ANewLayerOfOutputUnits(1)
                .With.OutputUnitActivation<IdentityUnitActivationTraining>()
                .And.Bias(bias)
                .And.LearningRate(learningRate)
                .And.Momentum(momentum)
                .And.SlopeMultiplier(slopeMultiplier)
                .And.SlopeMultiplier(1d)
                .And.SetupNetwork()
                .And.NameEverything()
                .And.ReadyForTraining(dependencyFactory);

            var randomWeights = new Queue<double>(new[] { -1d, -0.5d, 0.5d, 1d, 0.3, 0.2, 0.1, 0.5, 0.2, 0.3, 0.4, -1, -0.9 });
            weightSetterMock.Setup(wi => wi.SetWeight(It.IsAny<IConnectionUnderTraining>())).Callback<IConnectionUnderTraining>(c =>
            {
                var weight = randomWeights.Dequeue();
                c.Weight = weight;
                randomWeights.Enqueue(weight);
            });

            return errorBackPropagationTraining;
        }
    }
}
