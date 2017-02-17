using SodiumPlus.ActivationFunctions;
using FluentAssertions;
using NUnit.Framework;

namespace SodiumPlusUnitTests.ActivationFunctions
{
    public class HyperbolicTangentActivationFunctionTests
    {
        private HyperbolicTangentActivationFunction _activationFunction;

        [SetUp]
        public void Setup()
        {
            _activationFunction = new HyperbolicTangentActivationFunction();
        }

        [Test]
        public void HyperbolicTangentActivationFunctionReturnsExpectedActivation()
        {
            const double expected = 0.5370495669980353d;
            _activationFunction.Activation(0.6d).Should().BeApproximately(expected, 0.00000001d);
        }
    }
}
