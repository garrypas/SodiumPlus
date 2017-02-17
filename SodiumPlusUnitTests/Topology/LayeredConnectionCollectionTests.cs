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
    public class LayeredConnectionCollectionTests
    {
        private NetworkBuilder _network;

        [SetUp]
        public void SetUp()
        {
            _network = new NetworkBuilder().Setup();
            _network.SetupUnorthodoxStructure();
        }
        
        [Test]
        public void LayeredConnectionsSeparatesUnitsIntoLayersCorrectly()
        {
            var inputs = _network.GetInputs();
            _network.AddBias();

            var layeredUnitCollection = new LayeredConnectionCollection<IUnit, IConnection, IUnitActivation<IUnit>>(inputs);
            var layeredConnections = layeredUnitCollection.GetLayeredConnection().ToList();

            layeredConnections.Should().HaveCount(2);

            var inputToHiddenConnections = layeredConnections.ElementAt(0);
            var hiddenToOutputConnections = layeredConnections.ElementAt(1);

            inputToHiddenConnections.Should().Contain(new List<object>
            {
                _network.ConnectionInput1Hidden1,
                _network.ConnectionInput1Hidden2,
                _network.ConnectionInput2Hidden1,
                _network.ConnectionInput2Hidden2,
                _network.InputUnorthodoxToHidden1,
                _network.HiddenBiasToHidden1,
                _network.HiddenBiasToHidden2,
            }).And.HaveCount(7);

            hiddenToOutputConnections.Should().Contain(new List<object>
            {
                _network.ConnectionHidden1Output1,
                _network.ConnectionHidden1Output2,
                _network.ConnectionHidden2Output1,
                _network.ConnectionHidden2Output2,
                _network.InputUnorthodoxToOutput1,
                _network.OutputBiasToOutput1,
                _network.OutputBiasToOutput2,
            }).And.HaveCount(7);
        }
    }
}
