using System;
using System.Xml.Serialization;
using SodiumPlus.Topology;
using SodiumPlusTraining.ErrorBackPropagation.Builder;
using System.Collections.Generic;
using System.Linq;
using SodiumPlusTraining.ErrorBackPropagation;
using SodiumPlusTraining.ErrorBackPropagation.NetworkErrorFunctions;
using Moq;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;

namespace SodiumPlusIntegrationTests.Training.BackPropagation
{
    using TNetwork = ICollection<ICollection<ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>>;
    public static class XOrSetUp
    {
        public static IErrorBackPropagationTraining SetUpXOrTraining(IInventoryAndChaining inventoryAndChaining, double learningRate = 0.5d, double bias = 0d, double momentum = 0d, double slopeMultiplier = 1d, bool bactch = false, Action<TNetwork> networkCallback = null)
        {
            var weightSetterMock = new Mock<IWeightSetter>();
            var stepsMock = new Mock<ErrorBackPropagationStepsDependencyFactory>().As<IErrorBackPropagationStepsDependencyFactory>();
            stepsMock.CallBase = true;
            stepsMock.Setup(s => s.CreateWeightSetter(It.IsAny<double>(), It.IsAny<double>())).Returns(weightSetterMock.Object);
            var dependencyFactory = new ErrorBackPropagationDependencyFactory(stepsMock.Object);

            TNetwork network = null;
            var chain = inventoryAndChaining
                .And.Bias(bias)
                .And.LearningRate(learningRate)
                .And.Momentum(momentum)
                .And.SlopeMultiplier(slopeMultiplier)
                .And.NetworkErrorFunction<DifferenceErrorFunction>();

            if (bactch)
            {
                chain = chain.And.Batch();
            }

            var errorBackPropagationTraining = chain.And.SetupNetwork(n =>
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

        public static IErrorBackPropagationTraining SetUpXOrTrainingSingleFold<TUnitActivation>(double learningRate = 0.5d, double bias = 0d, double momentum = 0d, double slopeMultiplier = 1d, bool batch = false, Action<TNetwork> networkCallback = null)
            where TUnitActivation : IUnitActivationTrainingSingleFold, new()
        {
            var inventoryAndChaining = new ErrorBackPropagationBuilder()
                .With.ANewLayerOfInputUnits(2)
                .ConnectedTo.ANewLayerOfHiddenUnits(2).With.UnitActivation<TUnitActivation>()
                .ConnectedTo.ANewLayerOfOutputUnits(1).With.OutputUnitActivation<TUnitActivation>();

            return SetUpXOrTraining(inventoryAndChaining, learningRate, bias, momentum, slopeMultiplier, batch, networkCallback);
        }

        public static IErrorBackPropagationTraining SetUpXOrTrainingMultiFold<TUnitActivation>(double learningRate = 0.5d, double bias = 0d, double momentum = 0d, double slopeMultiplier = 1d, bool batch = false, Action<TNetwork> networkCallback = null)
            where TUnitActivation : IUnitActivationTrainingMultiFold, new()
        {
            var inventoryAndChaining = new ErrorBackPropagationBuilder()
                .With.ANewLayerOfInputUnits(2)
                .ConnectedTo.ANewLayerOfHiddenUnits(3).With.UnitActivationMultiFold<TUnitActivation>()
                .ConnectedTo.ANewLayerOfOutputUnits(2).With.OutputUnitActivationMultiFold<TUnitActivation>()
                .And.NetworkErrorFunction<CrossEntropyErrorFunction>();

            return SetUpXOrTraining(inventoryAndChaining, learningRate, bias, momentum, slopeMultiplier, batch, networkCallback);
        }
    }
}
