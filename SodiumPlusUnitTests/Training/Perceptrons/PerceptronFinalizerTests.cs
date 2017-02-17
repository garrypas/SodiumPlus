using SodiumPlus.Perceptrons;
using SodiumPlus.UnitActivations;
using SodiumPlusTraining.Topology.Builder;
using SodiumPlusTraining.UnitActivations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SodiumPlus.Topology;
using SodiumPlusTraining.Topology;

namespace SodiumPlusUnitTests.Training.Perceptrons
{
    using SodiumPlusTraining.Perceptrons;
    using FluentAssertions;
    using TNetwork = ICollection<ICollection<ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>>;
    public class PerceptronFinalizerTests
    {
        private const double Bias = 0.2d;
        private const double SlopeMultiplier = 0.2d;
        private TNetwork _network;
        private PerceptronFinalizer _perceptronFinalizer;

        [SetUp]
        public void SetUp()
        {
            _network = new StandardNetworkBuilderTraining()
                .With.ANewLayerOfInputUnits(2)
                .ConnectedTo.ANewLayerOfHiddenUnits(3).With.UnitActivationMultiFold<SoftmaxUnitActivationTraining>()
                .ConnectedTo.ANewLayerOfOutputUnits(2).With.OutputUnitActivationMultiFold<SoftmaxUnitActivationTraining>()
                .And.Bias(Bias)
                .And.SlopeMultiplier(SlopeMultiplier)
                .And.Build()
                .And.WithNamesAssignedToEverything()
                .And.GetNetwork();

            var connections = _network.SelectMany(u => u).SelectMany(u => u.OutgoingConnections);
            var weight = 0.0001d;
            connections.Enumerate(c =>
            {
                weight += 0.01d;
                c.Properties.Weight = weight;
            });

            _perceptronFinalizer = new PerceptronFinalizer();
        }

        [Test]
        public void PerceptronFinalizerCopiesNetworkLayers()
        {
            var newNetwork = _perceptronFinalizer.Clean(_network);
            newNetwork.Should().HaveCount(3);
        }

        [Test]
        public void PerceptronFinalizerCopiesUnits()
        {
            var newNetwork = _perceptronFinalizer.Clean(_network);
            var inputLayer = newNetwork.First();
            var hiddenLayer = newNetwork.ElementAt(1);
            var outputLayer = newNetwork.Last();

            inputLayer.Should().HaveCount(3);
            hiddenLayer.Should().HaveCount(4);
            outputLayer.Should().HaveCount(2);
        }

        [Test]
        public void PerceptronFinalizerConnectsUnits()
        {
            var newNetwork = _perceptronFinalizer.Clean(_network);
            var inputLayer = newNetwork.First();
            var hiddenLayer = newNetwork.ElementAt(1);
            var outputLayer = newNetwork.Last();

            inputLayer.EnumerateCartesian(hiddenLayer, (i, h) => i.UnitsAbove.Should().Contain(h));
            hiddenLayer.EnumerateCartesian(outputLayer, (h, o) => h.UnitsAbove.Should().Contain(o));
        }

        [Test]
        public void PerceptronFinalizerCopiesSlopeMultipliers()
        {
            var newUnits = _perceptronFinalizer.Clean(_network).SelectMany(u => u).Where(u => u.UnitActivation.UnitType == UnitType.NormalUnit);
            var oldUnits = _network.SelectMany(u => u).Where(u => u.UnitActivation.UnitType == UnitType.NormalUnit);
            newUnits.EnumerateCartesian(oldUnits, (newUnit, oldUnit) =>
            {
                var newSlopeMultiplier = newUnit.UnitActivation.Properties.SlopeMultiplier;
                var oldSlopeMultiplier = oldUnit.UnitActivation.Properties.SlopeMultiplier;

                newSlopeMultiplier.Should().Be(oldSlopeMultiplier);
            });
        }

        [Test]
        public void PerceptronFinalizerCopiesBiasUnits()
        {
            var newUnits = _perceptronFinalizer.Clean(_network).SelectMany(u => u).Where(u => u.UnitActivation.UnitType == UnitType.BiasUnit);
            var oldUnits = _network.SelectMany(u => u).Where(u => u.UnitActivation.UnitType == UnitType.BiasUnit);
            
            newUnits.Should().HaveSameCount(oldUnits);
            newUnits.EnumerateCartesian(oldUnits, (newUnit, oldUnit) =>
            {
                var neWBias = newUnit.UnitActivation.Properties.NetInput;
                var oldBias = oldUnit.UnitActivation.Properties.NetInput;

                neWBias.Should().Be(oldBias);
            });
        }

        [Test]
        public void PerceptronFinalizerCopiesNetworkReferenceToMultiFoldUnits()
        {
            var newNetwork = _perceptronFinalizer.Clean(_network);
            var multiFoldUnits = newNetwork.SelectMany(u => u).Where(u => u.UnitActivation is IUnitActivationMultiFold<IUnit, IConnection, IUnitActivation<IUnit>>);
            multiFoldUnits.Should().NotBeEmpty(because: "Empty lists will give false positives");
            multiFoldUnits.Select(u => ((IUnitActivationMultiFold<IUnit, IConnection, IUnitActivation<IUnit>>) u.UnitActivation).Network).Should().NotContainNulls();
        }

        [Test]
        public async Task PerceptronFinalizerNetworksReturnsMatchingOutputs()
        {
            var newNetwork = _perceptronFinalizer.Clean(_network);
            var inputs = new [] { 0.34d, 0.44d };

            var perceptronFromOld = new PerceptronUnderTraining(_network.First());
            var perceptronFromNew = new Perceptron(newNetwork.First());
            
            var oldOutputsTask = perceptronFromOld.FireAsync(inputs);
            var newOutputsTask = perceptronFromNew.FireAsync(inputs);
            
            var oldOutputs = await oldOutputsTask;
            var newOutputs = await newOutputsTask;

            oldOutputs.ShouldBeEquivalentTo(newOutputs);
        }
    }
}
