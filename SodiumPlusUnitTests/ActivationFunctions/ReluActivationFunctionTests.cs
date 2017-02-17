using SodiumPlus.ActivationFunctions;
using FluentAssertions;
using NUnit.Framework;

namespace SodiumPlusUnitTests.ActivationFunctions
{
    public class ReluActivationFunctionTests
    {
        private ReluActivationFunction _activationFunction;

        [SetUp]
        public void Setup()
        {
            _activationFunction = new ReluActivationFunction();
        }

        [Test]
        public void ReluActivationFunctionReturnsExpectedActivationWhenPositive()
        {
            const double expected = 1d;
            _activationFunction.Activation(1d).Should().BeApproximately(expected, 0.00000001d);
        }

        [Test]
        public void ReluActivationFunctionReturnsExpectedActivationWhenNegative()
        {
            const double expected = 0.0000000001d;
            _activationFunction.Activation(-0.1).Should().BeApproximately(expected, 0.0000000001d);
        }
    }
}
