using System;
using SodiumPlus.Topology;
using SodiumPlusTraining.ErrorBackPropagation;
using SodiumPlusTraining.ErrorBackPropagation.Builder;
using SodiumPlusTraining.ErrorBackPropagation.NetworkErrorFunctions;
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

    public class SolvesXOrWithSoftmaxActivationFunction
    {
        private IErrorBackPropagationTraining _errorBackPropagationTraining;
        private List<TrainingPattern> _trainingPatterns;

        [SetUp]
        public void SetUp()
        {
            _trainingPatterns = new List<TrainingPattern>
            {
                new TrainingPattern(new [] { 0d, 0d }, new [] { 0d, 1d }),
                new TrainingPattern(new [] { 1d, 0d }, new [] { 1d, 0d }),
                new TrainingPattern(new [] { 0d, 1d }, new [] { 1d, 0d }),
                new TrainingPattern(new [] { 1d, 1d }, new [] { 0d, 1d }),
            };
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task IntegrationSolvesXOrWithSoftmaxActivationFunction(bool oneHot)
        {
            _errorBackPropagationTraining = SetUpXOrTraining(bias: 0d, learningRate: 1d, momentum: 0.3d, slopeMultiplier: 1d, oneHot: oneHot);
            var perceptron = await _errorBackPropagationTraining.TrainAsync(_trainingPatterns, errorMax: 0.01d, maxEpochs: 1500);
            for (var t = 0; t < _trainingPatterns.Count; t++)
            {
                var trainingPattern = _trainingPatterns.ElementAt(t);
                var xorResult = await perceptron.FireAsync(trainingPattern.InputValues);
                xorResult.ElementAt(0).Should().BeApproximately(trainingPattern.IdealActivations.ElementAt(0), 0.1d, "Pattern " + t);
                xorResult.ElementAt(1).Should().BeApproximately(trainingPattern.IdealActivations.ElementAt(1), 0.1d, "Pattern " + t);
            }
        }

        private static IErrorBackPropagationTraining SetUpXOrTraining(double learningRate = 0.5d, double bias = 0d, double momentum = 0d, double slopeMultiplier = 1d, bool oneHot = false, Action<TNetwork> networkCallback = null)
        {
            var weightSetterMock = new Mock<IWeightSetter>();
            var stepsMock = new Mock<ErrorBackPropagationStepsDependencyFactory>().As<IErrorBackPropagationStepsDependencyFactory>();
            stepsMock.CallBase = true;
            stepsMock.Setup(s => s.CreateWeightSetter(It.IsAny<double>(), It.IsAny<double>())).Returns(weightSetterMock.Object);
            var dependencyFactory = new ErrorBackPropagationDependencyFactory(stepsMock.Object);

            TNetwork network = null;
            var chain = new ErrorBackPropagationBuilder()
                .With.ANewLayerOfInputUnits(2)
                .ConnectedTo.ANewLayerOfHiddenUnits(3).With.UnitActivationMultiFold<SoftmaxUnitActivationTraining>()
                .ConnectedTo.ANewLayerOfOutputUnits(2).With.OutputUnitActivationMultiFold<SoftmaxUnitActivationTraining>()
                .And.NetworkErrorFunction<CrossEntropyErrorFunction>()
                .And.Bias(bias)
                .And.LearningRate(learningRate)
                .And.Momentum(momentum)
                .And.SlopeMultiplier(slopeMultiplier);

            if (oneHot)
            {
                chain = chain.And.UseOneHotEncoding();
            }

            var errorBackPropagationTraining = chain
                .And.NetworkErrorFunction<DifferenceErrorFunction>()
                .And.SetupNetwork(n =>
                {
                    network = n;
                    if (networkCallback != null)
                    {
                        networkCallback(n);
                    }
                })
                .And.NameEverything()
                .And.ReadyForTraining(dependencyFactory);

            // This whole ceremony ensures weight setting is deterministic (same weight to same connection every time even if weights are set asynchronously)
            var connections = network.Skip(1).Select(units => units.SelectMany(u => u.IncomingConnections.Select(ic => ic.Properties)));
            var randomWeights = new List<double> { -1d, -0.5d, 0.5d, 1d, 0.3, 0.2, 0.1, 0.5, 0.2, 0.3, 0.4, -1, -0.9, 0.1, 0.5, 1.2, 1.3, 1.1, 0.35, 1.1, 0.1, 0.2, 0.3, -1d, -0.5d, 0.5d, 1d, 0.3, 0.2, 0.1, 0.5, 0.2, 0.3, 0.4, -1, -0.9, 0.1, 0.5, 1.2, 1.3, 1.1, 0.35, 1.1, 0.1, 0.2, 0.3 };
            weightSetterMock.Setup(wi => wi.SetWeight(It.IsAny<IConnectionUnderTraining>())).Callback<IConnectionUnderTraining>(c =>
            {
                var randomWeightIndex = connections.SelectMany(x => x).ToList().IndexOf(c);
                c.Weight = randomWeights[randomWeightIndex];
            });

            return errorBackPropagationTraining;
        }
    }
}
