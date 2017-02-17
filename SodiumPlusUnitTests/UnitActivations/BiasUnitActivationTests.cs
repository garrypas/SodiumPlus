using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;
using FluentAssertions;
using NUnit.Framework;

namespace SodiumPlusUnitTests.UnitActivations
{
    public class BiasUnitActivationTests
    {
        private BiasUnitActivation<Unit> _biasUnitActivation;
        private const double NetBias = 0.1d;

        [SetUp]
        public void SetUp()
        {
            _biasUnitActivation = new BiasUnitActivation<Unit>
            {
                Properties = new Unit
                {
                    NetInput = NetBias
                }
            };
        }

        [Test]
        public void BiasUnitActivationIsBias()
        {
            _biasUnitActivation.UnitType.Should().Be(UnitType.BiasUnit);
        }
    }
}
