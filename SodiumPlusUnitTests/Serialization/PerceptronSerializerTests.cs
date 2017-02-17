using System;
using System.Collections.Generic;
using System.Linq;
using SodiumPlus.Perceptrons;
using SodiumPlus.Topology;
using SodiumPlus.Topology.Builder;
using SodiumPlus.UnitActivations;
using SodiumPlusSerialization;
using FluentAssertions;
using NUnit.Framework;

namespace SodiumPlusUnitTests.Serialization
{
    public class PerceptronSerializerTests
    {
        private const double Bias = 0.2d;
        private const double SlopeMultiplier = 0.2d;
        private ICollection<ICollection<ITraversableUnit<IUnit, IConnection, IUnitActivationCreatable<IUnit>>>> _network;
        private PerceptronSerializer _perceptronSerializer;

        [SetUp]
        public void SetUp()
        {
            _network = new StandardNetworkBuilder()
                .With.ANewLayerOfInputUnits(2)
                .ConnectedTo.ANewLayerOfHiddenUnits(3).With.UnitActivationMultiFold<SoftmaxUnitActivation<IUnit, IConnection, IUnitActivationCreatable<IUnit>>>()
                .ConnectedTo.ANewLayerOfOutputUnits(2).With.OutputUnitActivationMultiFold<SoftmaxUnitActivation<IUnit, IConnection, IUnitActivationCreatable<IUnit>>>()
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

            _perceptronSerializer = new PerceptronSerializer();
        }

        [TestCase(typeof(Perceptron))]
        [TestCase(typeof(OneHotPerceptron))]
        public void PerceptronSerializerSerializesPerceptron(Type pType)
        {
            var inputs = _network.First();
            IPerceptron<IUnit, IConnection, IUnitActivation<IUnit>> perceptronBefore;
            if (pType == typeof (Perceptron))
            {
                perceptronBefore = new Perceptron(inputs);
            }
            else
            {
                perceptronBefore = new OneHotPerceptron(inputs);
            }
            var asJson = _perceptronSerializer.SerializeJson(perceptronBefore);
            var perceptronAfter = _perceptronSerializer.DeserializeJson<IUnit, IConnection, IUnitActivation<IUnit>>(asJson);
            perceptronAfter.Should().BeOfType(pType);
        }

        [Test]
        public void PerceptronSerializerPreservesSlopeMultiplier()
        {
            var perceptronBefore = new Perceptron(_network.First());
            var asJson = _perceptronSerializer.SerializeJson(perceptronBefore);
            var perceptronAfter = _perceptronSerializer.DeserializeJson<IUnit, IConnection, IUnitActivation<IUnit>>(asJson);
            var slopes = perceptronAfter.Network.SelectMany(u => u).Where(u => u.UnitActivation.UnitType == UnitType.NormalUnit);
            slopes.Select(u => u.UnitActivation.Properties.SlopeMultiplier).ShouldAllBeEquivalentTo(SlopeMultiplier);
        }

        [Test]
        public void PerceptronSerializerPreservesBias()
        {
            var perceptronBefore = new Perceptron(_network.First());
            var asJson = _perceptronSerializer.SerializeJson(perceptronBefore);
            var perceptronAfter = _perceptronSerializer.DeserializeJson<IUnit, IConnection, IUnitActivation<IUnit>>(asJson);
            var biases = perceptronAfter.Network.SelectMany(u => u).Where(u => u.UnitActivation.UnitType == UnitType.BiasUnit);
            biases.Select(u => u.UnitActivation.Properties.NetInput).ShouldAllBeEquivalentTo(Bias);
        }

        [Test]
        public void PerceptronSerializerPreservesWeights()
        {
            var perceptron = new Perceptron(_network.First());
            var asJson = _perceptronSerializer.SerializeJson(perceptron);
            var perceptronAfter = _perceptronSerializer.DeserializeJson<IUnit, IConnection, IUnitActivation<IUnit>>(asJson);
            var oldConnections = perceptron.Network.Skip(1).SelectMany(u => u).SelectMany(u => u.IncomingConnections);
            var newConnections = perceptronAfter.Network.Skip(1).SelectMany(u => u).SelectMany(u => u.IncomingConnections);
            oldConnections.EnumerateWithAdjacent(newConnections, (cOld, cNew) => cOld.Properties.Weight.Should().Be(cNew.Properties.Weight));
        }
    }
}
