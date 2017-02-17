using SodiumPlus.ActivationFunctions;
using FluentAssertions;
using NUnit.Framework;

namespace SodiumPlusUnitTests.ActivationFunctions
{
    public class BipolarActivationFunctionTests
    {
        private BipolarActivationFunction _activationFunction;

        [SetUp]
        public void Setup()
        {
            _activationFunction = new BipolarActivationFunction();
        }

        [Test]
        public void BipolarActivationFunctionReturnsExpectedActivation()
        {
            const double expected = 0.29131261d;
            _activationFunction.Activation(0.6).Should().BeApproximately(expected, 0.00000001d);
        }
    }
}
