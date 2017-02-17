using SodiumPlus.ActivationFunctions;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace SodiumPlusUnitTests.ActivationFunctions
{
    public class SoftmaxActivationFunctionTests
    {
        private SoftmaxActivationFunction _softmaxActivationFunction;

        [SetUp]
        public void SetUp()
        {
            _softmaxActivationFunction = new SoftmaxActivationFunction();
        }

        [Test]
        public void SoftmaxActivationFunctionCalculatesActivationCorrectly()
        {
            const double netInputUnit1 = 0.88d;
            const double netInputUnit2 = 0.77d;

            var activationValue = _softmaxActivationFunction.Activation(netInputUnit1, new[] { netInputUnit1, netInputUnit2 });

            var numerator = Math.Exp(netInputUnit1);
            var denominator = Math.Exp(netInputUnit1) + Math.Exp(netInputUnit2);
            var expected = numerator / denominator;

            activationValue.Should().BeApproximately(expected, 0.00000001d);
        }
    }
}
