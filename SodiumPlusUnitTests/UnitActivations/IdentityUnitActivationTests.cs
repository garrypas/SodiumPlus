using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;
using FluentAssertions;
using NUnit.Framework;

namespace SodiumPlusUnitTests.UnitActivations
{
    public class IdentityUnitActivationTests
    {
        private IdentityUnitActivation<Unit> _identityUnitActivation;
        private const double NetInput = 0.1d;

        [SetUp]
        public void SetUp()
        {
            _identityUnitActivation = new IdentityUnitActivation<Unit>
            {
                Properties = new Unit
                {
                    NetInput = NetInput
                }
            };
        }

        [Test]
        public void IdentityUnitActivationIsNormalUnit()
        {
            _identityUnitActivation.UnitType.Should().Be(UnitType.NormalUnit);
        }
    }
}
