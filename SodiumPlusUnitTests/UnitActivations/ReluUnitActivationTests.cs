using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;
using FluentAssertions;
using NUnit.Framework;

namespace SodiumPlusUnitTests.UnitActivations
{
    public class ReluUnitActivationTests
    {
        private ReluUnitActivation<Unit> _unitActivation;
        private const double NetInput = 0.1d;

        [SetUp]
        public void SetUp()
        {
            _unitActivation = new ReluUnitActivation<Unit>
            {
                Properties = new Unit
                {
                    NetInput = NetInput
                }
            };
        }

        [Test]
        public void ReluUnitActivationIsNormalUnit()
        {
            _unitActivation.UnitType.Should().Be(UnitType.NormalUnit);
        }
    }
}
