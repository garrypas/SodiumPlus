using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using SodiumPlus.Topology;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;
using SodiumPlusUnitTests.Mocks;

namespace SodiumPlusUnitTests.Topology
{
    public class NetworkExtensionsTests
    {
        private IEnumerable<IEnumerable<ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>> _network;
        private NetworkBuilder _networkBuilder;

        [SetUp]
        public void SetUp()
        {
            _networkBuilder = new NetworkBuilder().Setup();
            _network = _networkBuilder.GetNetwork();
        }

        [Test]
        public void NetworkExtensionsGetsNamedUnit()
        {
            _network.FindUnit("Hidden1").Should().Be(_networkBuilder.Hidden1);
        }

        [Test]
        public void NetworkExtensionsGetsConnection()
        {
            _network.FindConnection("Hidden1", "Output1").Should().Be(_networkBuilder.ConnectionHidden1Output1);
        }
    }
}
