using SodiumPlus.ActivationFunctions;
using FluentAssertions;
using NUnit.Framework;

namespace SodiumPlusUnitTests.ActivationFunctions
{
    public class SigmoidActivationFunctionTests
    {
        private SigmoidActivationFunction _activationFunction;

        [SetUp]
        public void SetUp()
        {
            _activationFunction = new SigmoidActivationFunction();
        }

        [Test]
        public void SigmoidActivationFunctionReturnsExpectedActivation()
        {
            const double expected = 0.64565631;
            _activationFunction.Activation(0.6).Should().BeApproximately(expected, 0.00000001d);
        }
    }
}
