using System.Collections.Generic;
using System.Linq;
using SodiumPlus.Topology;
using SodiumPlus.Topology.Builder;
using SodiumPlus.UnitActivations;
using FluentAssertions;
using NUnit.Framework;

namespace SodiumPlusUnitTests.Topology.Builder
{
    public class StandardNetworkBuilderTests
    {
        const int NumberOfInputUnits = 4;
        const int NumberOfHiddenUnits = 3;
        const int NumberOfOutputUnits = 2;

        [Test]
        public void StandardNetworkBuilderBuildsANetwork()
        {
            var network = CreateNetwork();

            network.Count.Should().Be(3, because: "There should be 3 layers");

            network.ElementAt(0).Count.Should().Be(NumberOfInputUnits);
            network.ElementAt(1).Count.Should().Be(NumberOfHiddenUnits);
            network.ElementAt(2).Count.Should().Be(NumberOfOutputUnits);
        }

        [Test]
        public void StandardNetworkBuilderBuildsANetworkWithBiasUnits()
        {
            var network = CreateNetwork(0.1d);

            network.Count.Should().Be(3, because: "There should be 3 layers");

            network.ElementAt(0).Count.Should().Be(NumberOfInputUnits + 1, because: "There should be a bias unit in the input layer");
            network.ElementAt(1).Count.Should().Be(NumberOfHiddenUnits + 1, because: "There should be a bias unit in the hidden layer");
            network.ElementAt(2).Count.Should().Be(NumberOfOutputUnits, because: "There should be no bias unit in the output layer");
        }

        [Test]
        public void StandardNetworkBuilderBuildsANetworkWithInputUnitActivationsSetupCorrectly()
        {
            var network = CreateNetwork();

            network.Count.Should().Be(3, because: "There should be 3 layers");

            network.ElementAt(0).Select(u => u.UnitActivation).Should().AllBeAssignableTo<InputUnitActivation<IUnit>>();
        }

        [Test]
        public void StandardNetworkBuilderBuildsANetworkWithUnitActivationsSetupCorrectly()
        {
            var network = CreateNetwork();

            network.Count.Should().Be(3, because: "There should be 3 layers");

            network.ElementAt(0).Select(u => u.UnitActivation).Should().AllBeAssignableTo<InputUnitActivation<IUnit>>();
        }

        [TestCase(UnitType.BiasUnit, 1d)] // No slopes on Bias
        [TestCase(UnitType.InputUnit, 1d)] // No slopes on Inputs
        [TestCase(UnitType.NormalUnit, 1.5d)] // Slopes on normal units
        public void StandardNetworkBuilderBuildsANetworkWithSlopeMultipliersSetupCorrectl(UnitType unitType, double expectedSlopeMultiplier)
        {
            var network = CreateNetwork(bias: 0.1, slopeMultiplier: 1.5d);

            var units = network.SelectMany(layer => layer).Where(u => u.UnitActivation.UnitType == unitType);

            units.Should().NotBeEmpty(because: "Empty result will give a false positive");

            units.Select(u => u.UnitActivation.Properties.SlopeMultiplier).ShouldAllBeEquivalentTo(expectedSlopeMultiplier);
        }

        [Test]
        public void StandardNetworkBuilderConnectsAllInputUnitsToAllHiddenUnits()
        {
            var network = CreateNetwork();

            var inputLayer = network.First();
            var hiddenLayer = network.ElementAt(1);
            
            var inputLayerConnections = inputLayer.SelectMany(u => u.OutgoingConnections);
            for (var i = 0; i < inputLayer.Count; i++)
            {
                for (var h = 0; h < hiddenLayer.Count; h++)
                {
                    var inputUnit = inputLayer.ElementAt(i);
                    var hiddenUnit = hiddenLayer.ElementAt(h);
                    inputLayerConnections.Should().Contain(connection => connection.InputUnit == inputUnit && connection.OutputUnit == hiddenUnit);
                }
            }
        }

        [Test]
        public void StandardNetworkBuilderConnectsAllHiddenUnitsToAllOutputUnits()
        {
            var network = CreateNetwork();

            var hiddenLayer = network.ElementAt(1);
            var outputLayer = network.Last();

            var hiddenLayerConnections = hiddenLayer.SelectMany(u => u.OutgoingConnections);

            for (var h = 0; h < hiddenLayer.Count; h++)
            {
                for (var o = 0; o < outputLayer.Count; o++)
                {
                    var hiddenUnit = hiddenLayer.ElementAt(h);
                    var outputUnit = outputLayer.ElementAt(o);
                    hiddenLayerConnections.Should().Contain(connection => connection.InputUnit == hiddenUnit && connection.OutputUnit == outputUnit);
                }
            }
        }

        public ICollection<ICollection<ITraversableUnit<IUnit, IConnection, IUnitActivationCreatable<IUnit>>>> CreateNetwork(double bias = 0d, double slopeMultiplier = 1d)
        {
            var network = new StandardNetworkBuilder().With.ANewLayerOfInputUnits(NumberOfInputUnits)
                .ConnectedTo.ANewLayerOfHiddenUnits(NumberOfHiddenUnits)
                .With.UnitActivation<IdentityUnitActivation<IUnit>>()
                .ConnectedTo.ANewLayerOfOutputUnits(NumberOfOutputUnits)
                .With.OutputUnitActivation<IdentityUnitActivation<IUnit>>()
                .And.Bias(bias)
                .And.SlopeMultiplier(slopeMultiplier)
                .And.Build()
                .And.GetNetwork();

            return network;
        }
    }
}
