using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;
using FluentAssertions;
using NUnit.Framework;

namespace SodiumPlusUnitTests.UnitActivations
{
    public class BipolarUnitActivationTests
    {
        private BipolarUnitActivation<Unit> _bipolarUnitActivation;
        private const double NetInput = 0.1d;

        [SetUp]
        public void SetUp()
        {
            _bipolarUnitActivation = new BipolarUnitActivation<Unit>
            {
                Properties = new Unit
                {
                    NetInput = NetInput
                }
            };
        }

        [Test]
        public void BipolarUnitActivationIsNormalUnit()
        {
            _bipolarUnitActivation.UnitType.Should().Be(UnitType.NormalUnit);
        }
    }
}
