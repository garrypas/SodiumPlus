using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;
using FluentAssertions;
using NUnit.Framework;

namespace SodiumPlusUnitTests.UnitActivations
{
    public class SigmoidUnitActivationTests
    {
        private SigmoidUnitActivation<Unit> _sigmoidUnitActivation;
        private const double NetInput = 0.1d;

        [SetUp]
        public void SetUp()
        {
            _sigmoidUnitActivation = new SigmoidUnitActivation<Unit>
            {
                Properties = new Unit
                {
                    NetInput = NetInput
                }
            };
        }

        [Test]
        public void SigmoidUnitActivationIsNormalUnit()
        {
            _sigmoidUnitActivation.UnitType.Should().Be(UnitType.NormalUnit);
        }
    }
}
