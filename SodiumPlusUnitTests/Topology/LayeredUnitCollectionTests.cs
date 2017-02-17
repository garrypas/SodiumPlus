using SodiumPlus.Topology;
using SodiumPlus.Topology.Layering;
using SodiumPlus.UnitActivations;
using SodiumPlusUnitTests.Mocks;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace SodiumPlusUnitTests.Topology
{
    public class LayeredUnitsTests
    {
        private NetworkBuilder _network;

        [SetUp]
        public void SetUp()
        {
            _network = new NetworkBuilder().Setup();
            _network.SetupUnorthodoxStructure();
        }

        [Test]
        public void LayeredUnitsSeparatesUnitsIntoLayersCorrectly()
        {
            var inputs = _network.GetInputs();
            var layeredUnitCollection = new LayeredUnitCollection<IUnit, IConnection, IUnitActivation<IUnit>>(inputs).GetLayeredUnits();

            layeredUnitCollection.ElementAt(0).Should().ContainInOrder(new List<object>
            {
                _network.Input1,
                _network.Input2,
                _network.InputUnorthodox,
            });
            layeredUnitCollection.ElementAt(1).Should().ContainInOrder(new List<object>
            {
                _network.Hidden1,
                _network.Hidden2,
            });
            layeredUnitCollection.ElementAt(2).Should().ContainInOrder(new List<object>
            {
                _network.Output1,
                _network.Output2,
            });
        }
    }
}
