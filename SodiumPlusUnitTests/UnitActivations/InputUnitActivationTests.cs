using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;
using FluentAssertions;
using NUnit.Framework;

namespace SodiumPlusUnitTests.UnitActivations
{
    public class InputUnitActivationTests
    {
        private InputUnitActivation<Unit> _inputUnitActivation;
        private const double NetInput = 0.1d;

        [SetUp]
        public void SetUp()
        {
            _inputUnitActivation = new InputUnitActivation<Unit>
            {
                Properties = new Unit
                {
                    NetInput = NetInput
                }
            };
        }

        [Test]
        public void InputUnitActivationIsInput()
        {
            _inputUnitActivation.UnitType.Should().Be(UnitType.InputUnit);
        }
    }
}
